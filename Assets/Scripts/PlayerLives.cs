using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [Header("Heart Visuals")]
    public SpriteRenderer[] hearts;

    [Header("Checkpoint")]
    private Vector3 respawnPosition;

    [Header("Flag Settings")]
    public Sprite activeFlagSprite;

    void Start()
    {
        // Cargar vidas desde el LivesManager
        LivesManager.Instance.currentLives = Mathf.Clamp(LivesManager.Instance.currentLives, 0, LivesManager.Instance.maxLives);
        UpdateHearts();
        respawnPosition = transform.position;
    }

    // Perder una vida
    public void TakeLife(int amount = 1)
    {
        LivesManager.Instance.currentLives -= amount;

        if (LivesManager.Instance.currentLives < 0)
            LivesManager.Instance.currentLives = 0;

        UpdateHearts();

        if (LivesManager.Instance.currentLives == 0)
            RespawnAtCheckpoint();
    }

    // Ganar una vida
    public void AddLife(int amount = 1)
    {
        LivesManager.Instance.currentLives += amount;

        if (LivesManager.Instance.currentLives > LivesManager.Instance.maxLives)
            LivesManager.Instance.currentLives = LivesManager.Instance.maxLives;

        UpdateHearts();
    }

    // Actualizar corazones visuales
    void UpdateHearts()
    {
        int currentLives = LivesManager.Instance.currentLives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
                hearts[i].enabled = (i < currentLives);
        }
    }

    // Checkpoint
    public void SetCheckpoint(Vector3 newPosition, SpriteRenderer flagRenderer = null)
    {
        respawnPosition = newPosition;

        if (flagRenderer != null && activeFlagSprite != null)
            flagRenderer.sprite = activeFlagSprite;
    }

    private void RespawnAtCheckpoint()
    {
        transform.position = respawnPosition;

        // Se restablecen las vidas al reaparecer
        LivesManager.Instance.currentLives = 3;

        UpdateHearts();
    }

    // Colisiones
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            TakeLife(1);
        }
        else if (other.CompareTag("Heart"))
        {
            AddLife(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Checkpoint"))
        {
            SpriteRenderer flagSprite = other.GetComponent<SpriteRenderer>();
            SetCheckpoint(other.transform.position, flagSprite);
        }
    }
}
