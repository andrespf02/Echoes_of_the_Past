using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Door Sprites")]
    public Sprite doorClosed;
    public Sprite doorOpen;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    [SerializeField, Tooltip("No cambiar en runtime. Siempre arranca cerrada.")]
    private bool isOpen = false; // Siempre arranca cerrada

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Forzar estado cerrado antes del inicio
        isOpen = false;
        if (boxCollider != null)
        {
            boxCollider.isTrigger = false; // bloquea paso
            boxCollider.enabled = true;
        }

        UpdateDoorVisual();
    }

    void Start()
    {
        // Reforzar por si algún otro script intenta tocarlo
        UpdateDoorVisual();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        UpdateDoorVisual();
    }

    private void UpdateDoorVisual()
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = isOpen ? doorOpen : doorClosed;

        if (boxCollider != null)
            boxCollider.isTrigger = isOpen; // abierto = trigger, cerrado = sólido
    }

    public void OpenDoor()
    {
        isOpen = true;
        UpdateDoorVisual();
    }

    public void CloseDoor()
    {
        isOpen = false;
        UpdateDoorVisual();
    }
}
