using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalShortcuts : MonoBehaviour
{
    private void Update()
    {
        // Salir del juego
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        // Volver al menú
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("MainMenu");
        }

        // Cheats para saltar niveles
        if (Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("Level1");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Level2");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("Level3");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Level4");
        }
    }
}
