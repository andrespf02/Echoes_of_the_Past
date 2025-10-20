using UnityEngine;
using TMPro;

public class MathPuzzleUI : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public string question = "2, 4, 6, ?";
    public int correctAnswer = 8;

    [Header("Door")]
    public GameObject doorToOpen;

    [Header("UI")]
    public TextMeshProUGUI puzzleText;
    public TextMeshProUGUI answerText;

    private string playerInput = "";
    private bool isPlayerNear = false;
    private PlayerLives playerLives;

    void Start()
    {
        playerLives = FindObjectOfType<PlayerLives>();
    }

    void Update()
    {
        if (isPlayerNear)
        {
            foreach (char c in Input.inputString)
            {
                if (char.IsDigit(c))
                {
                    playerInput += c;
                    answerText.text = playerInput;
                }
                else if (c == '\b' && playerInput.Length > 0) // backspace
                {
                    playerInput = playerInput.Substring(0, playerInput.Length - 1);
                    answerText.text = playerInput;
                }
                else if (c == '\n' || c == '\r') // enter
                {
                    CheckAnswer();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            puzzleText.text = question;
            answerText.text = "";
            playerInput = "";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            puzzleText.text = "";
            answerText.text = "";
            playerInput = "";
        }
    }

    void CheckAnswer()
    {
        int playerAnswer;
        if (int.TryParse(playerInput, out playerAnswer))
        {
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("Correct! Door opens.");
                if (doorToOpen != null)
                    doorToOpen.SetActive(false); // abre la puerta
            }
            else
            {
                Debug.Log("Wrong! Lose a life.");
                if (playerLives != null)
                    playerLives.TakeLife(1);
            }
        }
        else
        {
            Debug.Log("Invalid input");
        }

        // Reset input after checking
        playerInput = "";
        answerText.text = "";
    }
}
