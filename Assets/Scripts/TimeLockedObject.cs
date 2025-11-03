using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TimeLockedObject : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    [Header("Bloquear movimiento en el presente")]
    public bool lockInPresent = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChangedEvent += UpdateLockState;
            UpdateLockState();
        }
    }

    void OnDestroy()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChangedEvent -= UpdateLockState;
    }

    private void UpdateLockState()
    {
        if (rigidBody == null) return;

        if (TimeManager.Instance.isPast)
        {
            // En el pasado desbloqueada
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            //En el presente bloqueada
            if (lockInPresent)
                rigidBody.bodyType = RigidbodyType2D.Static;
        }
    }
}
