using UnityEngine;

public class MirrorPuzzle : PuzzleBase
{
    [Header("Puzzle Settings")]
    [TextArea]
    public string puzzleQuestion = "Espejito espejito...\nClave de 4 dígitos:";

    public string correctCode = "2314";
    private string input = "";

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
            if (char.IsDigit(c))
            {
                input += c;

                if (dialogueText != null)
                    dialogueText.text = puzzleQuestion + "\n\n> " + input;

                if (input.Length >= correctCode.Length)
                    CheckAnswer();
            }

            // Borrar con Backspace
            if (c == '\b' && input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);

                if (dialogueText != null)
                    dialogueText.text = puzzleQuestion + "\n\n> " + input;
            }
        }
    }

    protected override void CheckAnswer()
    {
        if (input == correctCode)
        {
            SolvePuzzle();
        }
        else
        {
            FailPuzzle();
            input = "";
        }
    }
}
