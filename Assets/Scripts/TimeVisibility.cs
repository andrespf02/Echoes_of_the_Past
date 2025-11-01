using UnityEngine;

public class TimeVisibility : MonoBehaviour
{
    public enum TimeState { Past, Present }
    [Header("Visible only in...")]
    public TimeState visibleIn = TimeState.Present;

    private SpriteRenderer spriteRenderer;
    private Collider2D objectCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<Collider2D>();

        UpdateVisibility();

        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent += UpdateVisibility;
        else
            Debug.LogError("TimeVisibility: No se encontró TimeManager en la escena");
    }

    // Destruye objetos de la escena al cambiar tiempo
    void OnDestroy()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent -= UpdateVisibility;
    }

    // Actualiza la visibilidad del objeto según el estado del tiempo
    public void UpdateVisibility()
    {
        if (spriteRenderer == null || objectCollider == null) return;

        bool isPast = TimeManager.Instance != null && TimeManager.Instance.isPast;
        bool visible = (visibleIn == TimeState.Past && isPast) || (visibleIn == TimeState.Present && !isPast);

        spriteRenderer.enabled = visible;
        objectCollider.enabled = visible;
    }
}