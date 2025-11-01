using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [Header("Lives Settings")]
    public int maxLives = 7;
    public int initialLives = 3;
    public int currentLives = 3;

    [Header("Heart Visuals")]
    public SpriteRenderer[] hearts;

    [Header("Checkpoint")]
    private Vector3 respawnPosition;

    [Header("Flag Settings")]
    public Sprite activeFlagSprite;

    void Start()
    {
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);
        UpdateHearts();
        respawnPosition = transform.position;
    }

    // Perder una vida al colisionar
    public void TakeLife(int amount = 1)
    {
        currentLives -= amount;
        if (currentLives < 0)
            currentLives = 0;

        UpdateHearts();

        if (currentLives == 0)
            RespawnAtCheckpoint();
    }

    // Ganar una vida al colisionar
    public void AddLife(int amount = 1)
    {
        currentLives += amount;
        if (currentLives > maxLives)
            currentLives = maxLives;

        UpdateHearts();
    }

    // Actualizar corazones
    void UpdateHearts()
    {
        if (hearts == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null) continue;
            hearts[i].enabled = (i < currentLives);
        }
    }

    // Checkpoint: resetar al número de vidas iniciales
    public void SetCheckpoint(Vector3 newPosition, SpriteRenderer flagRenderer = null)
    {
        respawnPosition = newPosition;
        Debug.Log("Checkpoint activado!");

        if (flagRenderer != null && activeFlagSprite != null)
            flagRenderer.sprite = activeFlagSprite;
    }

    private void RespawnAtCheckpoint()
    {
        Debug.Log("Player murió! Reapareciendo en el checkpoint...");
        transform.position = respawnPosition;

        currentLives = initialLives;
        UpdateHearts();
    }

    // Colisiones: perder y ganar vida según el objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            TakeLife(1);
            Debug.Log("Player touched fire! Lost one life.");
        }
        else if (other.CompareTag("Heart"))
        {
            AddLife(1);
            Debug.Log("Player picked up a heart!");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Checkpoint"))
        {
            SpriteRenderer flagSprite = other.GetComponent<SpriteRenderer>();
            SetCheckpoint(other.transform.position, flagSprite);
        }
    }
}
