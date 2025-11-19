using UnityEngine;

public class TimeVisibility : MonoBehaviour
{
    public enum TimeState { Past, Present }

    [Header("Visible only in...")]
    public TimeState visibleIn = TimeState.Present;

    private SpriteRenderer spriteRenderer;
    private MeshRenderer meshRenderer;   // Para TextMeshPro 3D
    private Collider2D objectCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();  // TMP 3D usa MeshRenderer
        objectCollider = GetComponent<Collider2D>();

        UpdateVisibility();

        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent += UpdateVisibility;
        else
            Debug.LogError("TimeVisibility: No se encontró TimeManager en la escena");
    }

    void OnDestroy()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent -= UpdateVisibility;
    }

    public void UpdateVisibility()
    {
        if (TimeManager.Instance == null)
            return;

        bool isPast = TimeManager.Instance.isPast;

        bool visible = (visibleIn == TimeState.Past && isPast) ||
                       (visibleIn == TimeState.Present && !isPast);

        // SpriteRenderer
        if (spriteRenderer != null)
            spriteRenderer.enabled = visible;

        // TextMeshPro 3D
        if (meshRenderer != null)
            meshRenderer.enabled = visible;

        // Collider
        if (objectCollider != null)
            objectCollider.enabled = visible;
    }
}
