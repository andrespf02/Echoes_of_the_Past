using UnityEngine;

public class SeasonPuzzle : PuzzleBase
{
    [Header("Puzzle Settings")]
    [TextArea]
    public string puzzleQuestion = "Ordena las estaciones:";

    public string correctWord = "VOIP";
    private string playerInput = "";

    protected override void ShowPuzzle()
    {
        base.ShowPuzzle();
        
        if (dialogueText != null)
            dialogueText.text = puzzleQuestion + "\n\n>";
    }

    protected override void ProcessInput()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                playerInput += char.ToUpper(c);

                if (dialogueText != null)
                    dialogueText.text = puzzleQuestion + "\n\n> " + playerInput;

                if (playerInput.Length >= correctWord.Length)
                    CheckAnswer();
            }

            // Permitir borrar
            if (c == '\b' && playerInput.Length > 0)
            {
                playerInput = playerInput.Substring(0, playerInput.Length - 1);

                if (dialogueText != null)
                    dialogueText.text = puzzleQuestion + "\n\n> " + playerInput;
            }
        }
    }

    protected override void CheckAnswer()
    {
        if (playerInput == correctWord)
        {
            SolvePuzzle();
        }
        else
        {
            FailPuzzle();
            playerInput = "";
        }
    }
}
