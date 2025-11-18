using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    public int maxLives = 7;
    public int currentLives = 3;  // valor inicial del juego

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
