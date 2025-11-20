using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalMemories = 0;

    private PlayerLives playerLives; // referencia al sistema de vidas del jugador

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

    private void Update()
    {
        HandleCheats();

        // Mantener referencia al PlayerLives si cambiamos de escena
        if (playerLives == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerLives = player.GetComponent<PlayerLives>();
        }
    }

    // Cheat para sumar/restar vidas
    private void HandleCheats()
    {
        if (playerLives == null) return;

        // Sumar vida (C)
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerLives.AddLife(1);
            Debug.Log($"CHEAT: +1 vida → {LivesManager.Instance.currentLives}");
        }

        // Restar vida (B)
        if (Input.GetKeyDown(KeyCode.B))
        {
            playerLives.TakeLife(1);
            Debug.Log($"CHEAT: -1 vida → {LivesManager.Instance.currentLives}");
        }
    }
}
