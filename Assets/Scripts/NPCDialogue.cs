using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC Dialogue Settings")]
    [TextArea] public string dialogueText = "Me alegra verte! Tú eres el elegido y eres la última esperanza de nuestro pueblo, por eso te otorgo el poder del cambio temporal. Usa este poder sabiamente para superar los desafíos que te esperan. Presiona 'P' para viajar cuando lo necesites. Buena suerte!";

    [Header("Visual Text")]
    public GameObject dialoguePrefab;
    private GameObject dialogueInstance;

    private bool playerInRange = false;
    private bool dialogueActive = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideDialogue();
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.H))
        {
            if (dialogueActive)
                HideDialogue();
            else
                ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        if (dialoguePrefab != null && dialogueInstance == null)
        {
            dialogueInstance = Instantiate(dialoguePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            dialogueInstance.GetComponentInChildren<TextMeshPro>().text = dialogueText;
            dialogueInstance.transform.SetParent(transform);
            dialogueActive = true;
        }
    }

    void HideDialogue()
    {
        if (dialogueInstance != null)
        {
            Destroy(dialogueInstance);
            dialogueActive = false;
        }
    }
}
