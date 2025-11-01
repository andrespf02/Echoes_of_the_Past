using UnityEngine;

public class DoorScript : MonoBehaviour
{
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

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        UpdateDoorVisual();
    }

    // Actualiza la apariencia visual de la puerta según su estado
    private void UpdateDoorVisual()
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = isOpen ? doorOpen : doorClosed;

        if (boxCollider != null)
            boxCollider.enabled = !isOpen;
    }
}
