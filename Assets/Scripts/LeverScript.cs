using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public DoorScript linkedDoor;
    private bool playerNearby = false;

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
            Debug.LogError("No se encuentra TimeManager en la escena");
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Space) && TimeManager.Instance.isPast)
        {
            linkedDoor.ToggleDoor(); // alterna abrir/cerrar
            Debug.Log("Palanca accionada. Estado de la puerta: " + (linkedDoor != null && linkedDoor != null));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }

    public void UpdateVisibility()
    {
        if (sr == null || col == null) return;
        bool visible = TimeManager.Instance != null && TimeManager.Instance.isPast;
        sr.enabled = visible;
        col.enabled = visible;
    }
}
