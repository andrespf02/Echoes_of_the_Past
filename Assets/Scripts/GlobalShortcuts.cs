using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalShortcuts : MonoBehaviour
{
    public static GlobalShortcuts Instance;
    public bool shortcutsBlocked = false;

    private void Awake()
    {
        // Singleton correcto y único
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);  // Evita duplicados
            return;
        }
    }

    private void Update()
    {
        if (shortcutsBlocked) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetGameState();
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.J))
            SceneManager.LoadScene("Level1");

        if (Input.GetKeyDown(KeyCode.K))
            SceneManager.LoadScene("Level2");

        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene("Level3");

        if (Input.GetKeyDown(KeyCode.Z))
            SceneManager.LoadScene("Level4");

        if (Input.GetKeyDown(KeyCode.X))
            SceneManager.LoadScene("Final");
    }

    public void ResetGameState()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.totalMemories = 0;

        if (LivesManager.Instance != null)
            LivesManager.Instance.ResetLives();
    }
}
