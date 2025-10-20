using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [Header("Lives Settings")]
    public int maxLives = 5;
    public int currentLives = 3;

    [Header("Heart Visuals")]
    public SpriteRenderer[] hearts;

    void Start()
    {
        currentLives = Mathf.Clamp(currentLives, 0, maxLives);
        UpdateHearts();
    }

    public void TakeLife(int amount = 1)
    {
        currentLives -= amount;
        if (currentLives < 0)
            currentLives = 0;

        UpdateHearts();

        if (currentLives == 0)
            GameOver();
    }

    public void AddLife(int amount = 1)
    {
        currentLives += amount;
        if (currentLives > maxLives)
            currentLives = maxLives;

        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentLives);
        }
    }

    void GameOver()
    {
        Debug.Log("Player ran out of lives!");
    }

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
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //         TakeLife();

    //     if (Input.GetKeyDown(KeyCode.A))
    //         AddLife();
    // }
}
