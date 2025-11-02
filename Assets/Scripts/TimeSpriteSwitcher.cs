using UnityEngine;

public class TimeSpriteSwitcher : MonoBehaviour
{
    [Header("Sprites según el tiempo")]
    public Sprite presentSprite;
    public Sprite pastSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("TimeSpriteSwitcher: no se encontró un SpriteRenderer en este objeto.");
            return;
        }

        // Suscribirse al evento del cambio temporal
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChangedEvent += UpdateSprite;
            UpdateSprite();
        }
    }

    void OnDestroy()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent -= UpdateSprite;
    }

    private void UpdateSprite()
    {
        if (spriteRenderer == null) return;

        // Cambiar sprite según el tiempo
        if (TimeManager.Instance.isPast)
            spriteRenderer.sprite = pastSprite;
        else
            spriteRenderer.sprite = presentSprite;
    }
}