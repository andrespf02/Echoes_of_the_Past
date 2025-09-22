using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // tu jugador (Explorer)
    public Vector3 offset;      // por si quieres ajustar la posición
    public float smoothSpeed = 0.125f; // suavizado

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}
