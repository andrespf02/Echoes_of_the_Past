using UnityEngine;
using TMPro;

public class MirrorPuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [TextArea] public string puzzleQuestion = "Espejito espejito dime una clave de 4 dígitos:";
    public string correctCode = "2314"; // Respuesta correcta

    [Header("Linked Door")]
    public DoorScript linkedDoor;

    [Header("Dialogue Prefab")]
    public GameObject dialoguePrefab;
    private GameObject dialogueInstance;
    private TextMeshPro dialogueText;

    [Header("Player Settings")]
    private PlayerLives playerLives;

    private bool playerNearby = false;
    private bool puzzleActive = false;
    private bool puzzleSolved = false;
    private string playerInput = "";

    void Start()
    {
        playerLives = FindFirstObjectByType<PlayerLives>();
    }

    // Detectar cercanía del jugador para mostrar el puzzle
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !puzzleSolved)
        {
            playerNearby = true;
            ShowPuzzle();
        }
    }

    // Detectar cercanía del jugador para desactivar el puzzle
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
        if (!playerNearby || !puzzleActive || puzzleSolved) return;

        foreach (char c in Input.inputString)
        {
            // Solo números válidos
            if (char.IsDigit(c))
            {
                playerInput += c;
                UpdateDialogue();

                if (playerInput.Length >= correctCode.Length)
                    CheckPuzzle();
            }

            // Borrar con Backspace
            if (c == '\b' && playerInput.Length > 0)
            {
                playerInput = playerInput.Substring(0, playerInput.Length - 1);
                UpdateDialogue();
            }
        }
    }

    // Mostrar puzzle
    void ShowPuzzle()
    {
        if (dialoguePrefab != null && dialogueInstance == null)
        {
            dialogueInstance = Instantiate(dialoguePrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            dialogueText = dialogueInstance.GetComponentInChildren<TextMeshPro>();
            if (dialogueText != null)
                dialogueText.text = puzzleQuestion + "\n\n> ";
            dialogueInstance.transform.SetParent(transform);
            puzzleActive = true;

            if (TimeManager.Instance != null)
                TimeManager.Instance.isTimeBlocked = true;
        }
    }

    void UpdateDialogue()
    {
        if (dialogueText != null)
            dialogueText.text = puzzleQuestion + "\n\n> " + playerInput;
    }

    void HidePuzzle()
    {
        if (dialogueInstance != null)
            Destroy(dialogueInstance);

        puzzleActive = false;
        playerInput = "";

        if (TimeManager.Instance != null)
            TimeManager.Instance.isTimeBlocked = false;
    }

    // Chequear respuesta del jugador: correcta abre la puerta y hace aparecer una memoria, incorrecta quita una vida
    void CheckPuzzle()
    {
        if (playerInput == correctCode)
        {
            puzzleSolved = true;
            if (dialogueText != null)
                dialogueText.text = "¡Correcto! La puerta se ha abierto.";

            if (linkedDoor != null)
                linkedDoor.ToggleDoor();

            Invoke(nameof(DestroyPuzzleObject), 2f);
        }
        else
        {
            if (dialogueText != null)
                dialogueText.text = "Incorrecto. Has perdido una vida.";

            if (playerLives != null)
                playerLives.TakeLife(1);

            playerInput = ""; // Reinicia input para volver a intentar
        }
    }

    void DestroyPuzzleObject()
    {
        HidePuzzle();
        Destroy(gameObject);
    }
}
