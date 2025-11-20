using UnityEngine;
using TMPro;

public class MemoryCheats : MonoBehaviour
{
    public TextMeshPro text3D;

    void Start()
    {
        UpdateMemoryText();
    }

    void Update()
    {
        // CHEAT: Agregar memoria (M)
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.Instance.totalMemories++;
            UpdateMemoryText();
            Debug.Log("CHEAT: +1 Memoria");
        }

        // CHEAT: Restar memoria (N)
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameManager.Instance.totalMemories = Mathf.Max(0, GameManager.Instance.totalMemories - 1);
            UpdateMemoryText();
            Debug.Log("CHEAT: -1 Memoria");
        }
    }

    public void UpdateMemoryText()
    {
        if (text3D != null)
            text3D.text = GameManager.Instance.totalMemories.ToString();
    }
}
