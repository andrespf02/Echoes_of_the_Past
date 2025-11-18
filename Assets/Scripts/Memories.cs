using UnityEngine;
using TMPro;

public class Memories : MonoBehaviour
{
    [Header("3D Number Counter")]
    public TextMeshPro text3D;

    private void Start()
    {
        if (text3D != null)
            text3D.text = GameManager.Instance.totalMemories.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Suma global
            GameManager.Instance.totalMemories++;

            // Actualiza el texto 3D
            if (text3D != null)
                text3D.text = GameManager.Instance.totalMemories.ToString();

            // Destruye la memoria recogida
            Destroy(gameObject);
        }
    }
}
