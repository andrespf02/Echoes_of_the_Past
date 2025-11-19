using UnityEngine;
using TMPro;

public abstract class PuzzleBase : MonoBehaviour
{
    [Header("Puzzle UI")]
    public GameObject dialoguePrefab;
    protected GameObject dialogueInstance;
    protected TextMeshPro dialogueText;

    [Header("Door / Chest / Hidden Object")]
    public DoorScript linkedDoor;
    public GameObject hiddenObject;
    public SpriteRenderer chestRenderer;
    public Sprite openChestSprite;

    protected PlayerLives playerLives;

    protected bool playerNearby = false;
    protected bool puzzleActive = false;
    protected bool puzzleSolved = false;

    protected virtual void Start()
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

    protected virtual void ShowPuzzle()
    {
        if (dialoguePrefab != null && dialogueInstance == null)
        {
            dialogueInstance = Instantiate(dialoguePrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            dialogueText = dialogueInstance.GetComponentInChildren<TextMeshPro>();
            dialogueInstance.transform.SetParent(transform);

            puzzleActive = true;

            if (TimeManager.Instance != null)
                TimeManager.Instance.isTimeBlocked = true;
        }
    }

    protected virtual void HidePuzzle()
    {
        if (dialogueInstance != null)
            Destroy(dialogueInstance);

        puzzleActive = false;

        if (TimeManager.Instance != null)
            TimeManager.Instance.isTimeBlocked = false;
    }

    // Cada puzzle define cómo procesa input y verifica respuesta
    protected abstract void ProcessInput();
    protected abstract void CheckAnswer();

    protected void SolvePuzzle()
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

    protected void FailPuzzle()
    {
        if (dialogueText != null)
            dialogueText.text = "Incorrecto. Has perdido una vida.";

        if (playerLives != null)
            playerLives.TakeLife(1);
    }

    protected void DestroyPuzzleObject()
    {
        HidePuzzle();
        Destroy(gameObject);
    }

    protected virtual void Update()
    {
        if (playerNearby && puzzleActive && !puzzleSolved)
            ProcessInput();
    }
}
