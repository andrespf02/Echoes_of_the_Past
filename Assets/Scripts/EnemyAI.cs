using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    public float detectionRange = 5f;
    public float loseSightTime = 2f;

    private Transform player;
    private int currentPoint = 0;
    private float loseSightCounter = 0f;
    private bool isChasing = false;

    private float patrolMinX;
    private float patrolMaxX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Calcula límites de patrulla
        patrolMinX = Mathf.Min(patrolPoints[0].position.x, patrolPoints[1].position.x);
        patrolMaxX = Mathf.Max(patrolPoints[0].position.x, patrolPoints[1].position.x);
    }

    void Update()
    {
        bool playerInSight = PlayerInSight() && PlayerInsidePatrolArea();

        if (playerInSight)
        {
            isChasing = true;
            loseSightCounter = loseSightTime;
        }
        else if (isChasing)
        {
            loseSightCounter -= Time.deltaTime;
            if (loseSightCounter <= 0)
                isChasing = false;
        }

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    // Solo detecta si está en rango
    private bool PlayerInSight()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionRange;
    }

    // NO persigue fuera de la zona marcada por los puntos
    private bool PlayerInsidePatrolArea()
    {
        return player.position.x >= patrolMinX && player.position.x <= patrolMaxX;
    }

    private void Patrol()
    {
        Transform targetPoint = patrolPoints[currentPoint];

        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPoint.position,
            patrolSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    private void ChasePlayer()
    {
        // Limitar persecución dentro del área
        if (player.position.x < patrolMinX || player.position.x > patrolMaxX)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerLives lives = collision.GetComponent<PlayerLives>();
            if (lives != null)
                lives.TakeLife(1);
        }
    }
}
