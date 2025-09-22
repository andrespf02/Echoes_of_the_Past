using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Sprite doorClosed;
    public Sprite doorOpen;

    private SpriteRenderer sr;
    private BoxCollider2D col;
    private bool isOpen = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        UpdateDoorVisual();
    }

    // Alterna el estado de la puerta
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        UpdateDoorVisual();
    }

    private void UpdateDoorVisual()
    {
        sr.sprite = isOpen ? doorOpen : doorClosed;
        col.enabled = !isOpen; // bloquea solo si está cerrada
    }
}
