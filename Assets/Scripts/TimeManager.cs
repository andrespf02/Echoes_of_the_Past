using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("Estado inicial del tiempo")]
    public bool isPast = false;

    public delegate void TimeChangeHandler();
    public event TimeChangeHandler OnTimeChangedEvent;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // cambiar tiempo
        {
            isPast = !isPast;
            Debug.Log("Cambio de tiempo: " + (isPast ? "Pasado" : "Presente"));
            if (OnTimeChangedEvent != null)
                OnTimeChangedEvent.Invoke();
        }
    }
}
