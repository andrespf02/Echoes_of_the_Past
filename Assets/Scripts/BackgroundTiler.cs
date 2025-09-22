using UnityEngine;

public class BackgroundTiler : MonoBehaviour
{
    public Transform target; // normalmente la cámara
    private float width;
    private float height;

    void Start()
    {
        // Obtener el tamaño del sprite en Unity units
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        width = sr.bounds.size.x;
        height = sr.bounds.size.y;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = target.position;

        // Si la cámara se mueve más allá del borde, reposicionar el fondo
        if (targetPos.x - pos.x >= width) pos.x += width;
        else if (pos.x - targetPos.x >= width) pos.x -= width;

        if (targetPos.y - pos.y >= height) pos.y += height;
        else if (pos.y - targetPos.y >= height) pos.y -= height;

        transform.position = pos;
    }
}
