using UnityEngine;
using TMPro;

public class SeasonPuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [TextArea] public string puzzleQuestion = "Ordena:";
    public string correctWord = "VOIP"; // Verano, Otoño, Invierno, Primavera

    [Header("Linked Door & Hidden Object")]
    public DoorScript linkedDoor;
    public GameObject hiddenObject;
    public SpriteRenderer chestRenderer;
    public Sprite openChestSprite;

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
        if (hiddenObject != null) hiddenObject.SetActive(false);
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
        if (!playerNearby || !puzzleActive || puzzleSolved) return;

        foreach (char c in Input.inputString)
        {
            // Solo letras
            if (char.IsLetter(c))
            {
                playerInput += char.ToUpper(c);
                UpdateDialogue();

                if (playerInput.Length >= correctWord.Length)
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
        if (playerInput == correctWord)
        {
            puzzleSolved = true;
            if (dialogueText != null)
                dialogueText.text = "¡Correcto! La puerta se ha abierto.";

            if (linkedDoor != null)
                linkedDoor.ToggleDoor();

            if (hiddenObject != null)
                hiddenObject.SetActive(true);

            if (chestRenderer != null && openChestSprite != null)
                chestRenderer.sprite = openChestSprite;

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
