using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }

    void Update()
    {
        MoveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }
}
