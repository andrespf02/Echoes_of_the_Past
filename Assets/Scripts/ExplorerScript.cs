using UnityEngine;

public class ExplorerScript : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public float moveSpeed = 3f;

    void Start()
    {
        // Asegurar que existe un Rigidbody2D
        if (playerRigidbody == null)
            playerRigidbody = GetComponent<Rigidbody2D>();

        if (playerRigidbody == null)
            Debug.LogWarning("ExplorerScript requires a Rigidbody2D on the same GameObject.");
    }

    void FixedUpdate()
    {
        if (playerRigidbody == null) return;

        // Leer input
        Vector2 move = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) move += Vector2.up;
        if (Input.GetKey(KeyCode.S)) move += Vector2.down;
        if (Input.GetKey(KeyCode.A)) move += Vector2.left;
        if (Input.GetKey(KeyCode.D)) move += Vector2.right;

        // Aplicar movimiento
        playerRigidbody.linearVelocity = move.normalized * moveSpeed;
    }
}
