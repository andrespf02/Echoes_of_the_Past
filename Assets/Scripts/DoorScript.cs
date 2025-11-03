using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [Header("Door Sprites")]
    public Sprite doorClosed;
    public Sprite doorOpen;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isOpen = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (spriteRenderer == null)
            Debug.LogWarning("DoorScript: no SpriteRenderer found on the door GameObject.");
        if (boxCollider == null)
            Debug.LogWarning("DoorScript: no BoxCollider2D found on the door GameObject.");

        UpdateDoorVisual();
    }

    // Llama a este método para abrir/cerrar la puerta
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        UpdateDoorVisual();
    }

    // Actualiza el sprite y el collider según si la puerta está abierta o cerrada
    private void UpdateDoorVisual()
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = isOpen ? doorOpen : doorClosed;

        if (boxCollider != null)
            boxCollider.isTrigger = isOpen; // permite atravesar cuando está abierta
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
