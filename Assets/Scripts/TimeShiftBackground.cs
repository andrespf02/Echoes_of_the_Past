using UnityEngine;

public class TimeShiftBackground : MonoBehaviour
{
    public Sprite pastBackground;
    public Sprite presentBackground;

    // Sprite del fondo
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("TimeShiftBackground requires a SpriteRenderer on the same GameObject.");
            return;
        }

        // Inicializar sprite según el TimeManager global si está disponible.
        if (TimeManager.Instance != null)
        {
            UpdateBackgroundSprite();
            TimeManager.Instance.OnTimeChangedEvent += UpdateBackgroundSprite;
        }
        else
        {
            // Si no se encuentra TimeManager, se usará el fondo presente por defecto.
            spriteRenderer.sprite = presentBackground;
            Debug.LogWarning("TimeManager not found: TimeShiftBackground will not react to time changes automatically.");
        }
    }

    void OnDestroy()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent -= UpdateBackgroundSprite;
    }

    private void UpdateBackgroundSprite()
    {
        if (spriteRenderer == null) return;
        bool isPast = TimeManager.Instance != null && TimeManager.Instance.isPast;
        spriteRenderer.sprite = isPast ? pastBackground : presentBackground;
    }
}
