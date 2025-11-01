using UnityEngine;
using TMPro;

public class LeverScript : MonoBehaviour
{
    [Header("Linked Door")]
    public DoorScript linkedDoor;

    [Header("Lever Dialogue Settings")]
    [TextArea] public string dialogueText = "Actívame!";
    public GameObject dialoguePrefab;

    private bool playerNearby = false;
    private GameObject dialogueInstance;

    private SpriteRenderer spriteRenderer;
    private Collider2D boxCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<Collider2D>();

        // Actualiza la visibilidad inicial según el estado global del tiempo.
        UpdateVisibility();

        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent += UpdateVisibility;
        else
            Debug.LogError("LeverScript: No se encuentra TimeManager en la escena");
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Space) && TimeManager.Instance != null && TimeManager.Instance.isPast)
        {
            if (linkedDoor != null)
            {
                linkedDoor.ToggleDoor();
                Debug.Log("Palanca accionada. Puerta cambiada de estado.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            ShowDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            HideDialogue();
        }
    }

    public void UpdateVisibility()
    {
        if (spriteRenderer == null || boxCollider == null) return;

        bool visible = TimeManager.Instance != null && TimeManager.Instance.isPast;
        spriteRenderer.enabled = visible;
        boxCollider.enabled = visible;
    }

    // Pista de diálogo para el jugador
    void ShowDialogue()
    {
        if (dialoguePrefab != null && dialogueInstance == null)
        {
            dialogueInstance = Instantiate(dialoguePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            dialogueInstance.GetComponentInChildren<TextMeshPro>().text = dialogueText;
            dialogueInstance.transform.SetParent(transform);
        }
    }

    void HideDialogue()
    {
        if (dialogueInstance != null)
            Destroy(dialogueInstance);
    }
}
