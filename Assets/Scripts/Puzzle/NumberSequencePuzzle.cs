using UnityEngine;

public class NumberSequencePuzzle : PuzzleBase
{
    public int correctAnswer = 8;

    protected override void ShowPuzzle()
    {
        base.ShowPuzzle();
        if (dialogueText != null)
            dialogueText.text = "Completa la secuencia: 2, 4, 6, ?";
    }

    protected override void ProcessInput()
    {
        for (int i = 0; i <= 9; i++)
            if (Input.GetKeyDown(i.ToString()))
            {
                if (i == correctAnswer)
                    SolvePuzzle();
                else
                    FailPuzzle();
            }
    }

    protected override void CheckAnswer() {}
}
