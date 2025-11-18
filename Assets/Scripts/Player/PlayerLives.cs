using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [Header("Heart Visuals")]
    public SpriteRenderer[] hearts;

    [Header("Checkpoint")]
    private Vector3 respawnPosition;

    [Header("Flag Settings")]
    public Sprite activeFlagSprite;

    private SpriteRenderer playerRenderer;
    private bool isBlinking = false;

    void Start()
    {
        playerRenderer = GetComponent<SpriteRenderer>();

        respawnPosition = transform.position;
        UpdateHearts();
    }

    // Perder vida
    public void TakeLife(int amount = 1)
    {
        LivesManager.Instance.currentLives -= amount;

        // Parpadeo rojo
        StartCoroutine(DamageBlink());

        if (LivesManager.Instance.currentLives <= 0)
        {
            LivesManager.Instance.currentLives = 0;
            RespawnAtCheckpoint();
            return;
        }

        UpdateHearts();
    }

    // Ganar vida
    public void AddLife(int amount = 1)
    {
        LivesManager.Instance.currentLives += amount;

        if (LivesManager.Instance.currentLives > LivesManager.Instance.maxLives)
            LivesManager.Instance.currentLives = LivesManager.Instance.maxLives;

        UpdateHearts();

        // Parpadeo verde
        StartCoroutine(HealBlink());
    }

    // Guardar checkpoint
    public void SetCheckpoint(Vector3 newPosition, SpriteRenderer flag)
    {
        respawnPosition = newPosition;

        if (flag != null && activeFlagSprite != null)
            flag.sprite = activeFlagSprite;
    }

    // Reaparecer en checkpoint
    private void RespawnAtCheckpoint()
    {
        transform.position = respawnPosition;
        LivesManager.Instance.currentLives = LivesManager.Instance.initialLives;
        UpdateHearts();
    }

    // Corazones visuales
    private void UpdateHearts()
    {
        int lives = LivesManager.Instance.currentLives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
                hearts[i].enabled = (i < lives);
        }
    }

    // Parpadeo rojo al recibir daño
    private System.Collections.IEnumerator DamageBlink()
    {
        if (isBlinking || playerRenderer == null)
            yield break;

        isBlinking = true;

        Color originalColor = playerRenderer.color;

        for (int i = 0; i < 3; i++)
        {
            playerRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);

            playerRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }

        isBlinking = false;
    }

    // Parpadeo verde (curación)
    private System.Collections.IEnumerator HealBlink()
    {
        if (isBlinking || playerRenderer == null)
            yield break;

        isBlinking = true;

        Color originalColor = playerRenderer.color;

        for (int i = 0; i < 3; i++)
        {
            playerRenderer.color = Color.green;
            yield return new WaitForSeconds(0.1f);

            playerRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }

        isBlinking = false;
    }
}
