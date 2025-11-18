using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private PlayerLives playerLives;

    void Start()
    {
        playerLives = GetComponent<PlayerLives>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerLives == null) return;

        // Fuego
        if (other.CompareTag("Fire"))
        {
            playerLives.TakeLife(1);
        }

        // Corazón
        else if (other.CompareTag("Heart"))
        {
            playerLives.AddLife(1);
            Destroy(other.gameObject);
        }

        // Checkpoint
        else if (other.CompareTag("Checkpoint"))
        {
            SpriteRenderer flagSprite = other.GetComponent<SpriteRenderer>();
            playerLives.SetCheckpoint(other.transform.position, flagSprite);
        }
    }
}
