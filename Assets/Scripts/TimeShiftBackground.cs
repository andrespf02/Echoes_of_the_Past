using UnityEngine;

public class TimeShiftBackground : MonoBehaviour
{
    public Sprite pastBackground;
    public Sprite presentBackground;

    private SpriteRenderer sr;
    private bool isPast = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = presentBackground; // empieza en el presente
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPast = !isPast;
            sr.sprite = isPast ? pastBackground : presentBackground;
        }
    }
}
