using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("Estado inicial del tiempo")]
    public bool isPast = false;

    [Header("Bloqueo de control temporal")]
    public bool isTimeBlocked = false;

    public delegate void TimeChangeHandler();
    public event TimeChangeHandler OnTimeChangedEvent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Permite cambiar el tiempo al tocar tecla P si es no está bloqueado (se bloquea cuando esté resoleviendo puzzles)
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isTimeBlocked)
        {
            isPast = !isPast;
            Debug.Log("Cambio de tiempo: " + (isPast ? "Pasado" : "Presente"));
            OnTimeChangedEvent?.Invoke();
        }
    }
}
