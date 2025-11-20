using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneRedirect : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(ReturnToMenu), 3f);
    }

    private void ReturnToMenu()
    {
        // Reiniciar estado del juego si quieres
        if (GameManager.Instance != null)
            GameManager.Instance.totalMemories = 0;

        if (LivesManager.Instance != null)
            LivesManager.Instance.ResetLives();

        SceneManager.LoadScene("MainMenu");
    }
}
