using UnityEngine;
using TMPro;

public class PuzzleScript : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [TextArea] public string puzzleQuestion = "Completa la secuencia: 2, 4, 6, ?";
    public int correctAnswer = 8;

    [Header("Linked Door")]
    public DoorScript linkedDoor;

    [Header("Chest Settings")]
    public SpriteRenderer chestRenderer;
    public Sprite openChestSprite;

    [Header("Hidden Object")]
    public GameObject hiddenObject;

    [Header("Dialogue Prefab")]
    public GameObject dialoguePrefab;
    private GameObject dialogueInstance;

    private bool playerNearby = false;
    private bool puzzleActive = false;
    private bool puzzleSolved = false;

    private PlayerLives playerLives;

    void Start()
    {
        playerLives = FindFirstObjectByType<PlayerLives>();
        if (playerLives == null)
            Debug.LogWarning("No se encontró PlayerLives en la escena.");

        if (hiddenObject != null)
            hiddenObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !puzzleSolved)
        {
            playerNearby = true;
            ShowPuzzle();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            HidePuzzle();
        }
    }

    void Update()
    {
        if (playerNearby && puzzleActive && !puzzleSolved)
        {
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    CheckAnswer(i);
                }
            }
        }
    }

    void ShowPuzzle()
    {
        if (dialoguePrefab != null && dialogueInstance == null)
        {
            dialogueInstance = Instantiate(dialoguePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            dialogueInstance.GetComponentInChildren<TextMeshPro>().text = puzzleQuestion + "\nPresiona el número correcto.";
            dialogueInstance.transform.SetParent(transform);
            puzzleActive = true;
        }
    }

    void HidePuzzle()
    {
        if (dialogueInstance != null)
            Destroy(dialogueInstance);

        puzzleActive = false;
    }

    void CheckAnswer(int answer)
    {
        if (answer == correctAnswer)
        {
            puzzleSolved = true;
            if (dialogueInstance != null)
                dialogueInstance.GetComponentInChildren<TextMeshPro>().text = "¡Correcto! La puerta se ha abierto.";

            if (linkedDoor != null)
                linkedDoor.ToggleDoor();

            if (chestRenderer != null && openChestSprite != null)
                chestRenderer.sprite = openChestSprite;

            if (hiddenObject != null)
                hiddenObject.SetActive(true);

            Invoke(nameof(DestroyPuzzleObject), 2f);
        }
        else
        {
            if (dialogueInstance != null)
                dialogueInstance.GetComponentInChildren<TextMeshPro>().text = "Incorrecto. Has perdido una vida.";

            if (playerLives != null)
                playerLives.TakeLife(1);
        }

        Invoke(nameof(HidePuzzle), 2f);
    }

    void DestroyPuzzleObject()
    {
        if (dialogueInstance != null)
            Destroy(dialogueInstance);

        Destroy(gameObject);
    }
}
