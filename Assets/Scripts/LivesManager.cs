using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    [Header("Lives Settings")]
    public int maxLives = 7; // Vidas máximas permitidas
    public int initialLives = 3; // Vidas iniciales al comenzar el juego
    public int currentLives;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Inicializar vidas al comenzar el juego
            currentLives = initialLives;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
