using UnityEngine;

public class TimeVisibility : MonoBehaviour
{
    public enum TimeState { Past, Present }
    [Header("Visible only in...")]
    public TimeState visibleIn = TimeState.Present;

    private SpriteRenderer sr;
    private Collider2D col;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        UpdateVisibility();

        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent += UpdateVisibility;
        else
            Debug.LogError("No se encontró TimeManager en la escena");
    }

    void OnDestroy()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent -= UpdateVisibility;
    }

    public void UpdateVisibility()
    {
        if (sr == null || col == null) return;

        bool isPast = TimeManager.Instance != null && TimeManager.Instance.isPast;
        bool visible = (visibleIn == TimeState.Past && isPast) || (visibleIn == TimeState.Present && !isPast);

        sr.enabled = visible;
        col.enabled = visible;
    }
}