using UnityEngine;
using TMPro;

public class Memories : MonoBehaviour
{
    [Header("3D Number Counter")]
    public TextMeshPro text3D;

    private void Start()
    {
        if (text3D != null)
            text3D.text = "0";
    }

    // Al recoger memoria incrementa el contador de memorias y destruye el objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (text3D != null)
            {
                int currentCount = int.Parse(text3D.text);
                currentCount++;
                text3D.text = currentCount.ToString();
            }

            Destroy(gameObject);
        }
    }
}