using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private PlayerInput input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input.MoveInput * moveSpeed;
    }
}
