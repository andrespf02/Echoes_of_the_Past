using UnityEngine;
using TMPro;

public class HintScript : MonoBehaviour
{
    [Header("Hint Settings")]
    [TextArea] public string hintText = "Hint: Todo tiempo pasado fue mejor";
    public GameObject dialoguePrefab;

    private GameObject dialogueInstance;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            ShowHint();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            HideHint();
    }

    void ShowHint()
    {
        if (dialoguePrefab != null && dialogueInstance == null)
        {
            dialogueInstance = Instantiate(dialoguePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            dialogueInstance.GetComponentInChildren<TextMeshPro>().text = hintText;
            dialogueInstance.transform.SetParent(transform);
        }
    }

    void HideHint()
    {
        if (dialogueInstance != null)
            Destroy(dialogueInstance);
    }
}
