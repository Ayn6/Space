using UnityEngine;

public class EnemyUFO : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public float detectionRange = 15f; // Радиус обнаружения игрока
    public float chaseSpeed = 5f; // Скорость преследования
    public float wanderSpeed = 2f; // Скорость блуждания
    public float captureDistance = 1.5f; // Дистанция, на которой НЛО захватывает игрока
    public float wanderChangeTime = 3f; // Время смены направления блуждания

    private Vector3 wanderDirection; // Текущее направление блуждания
    private float wanderTimer; // Таймер для смены направления
    private bool isChasing = false; // Флаг, преследует ли НЛО игрока
    private bool hasCapturedPlayer = false; // Флаг, захвачен ли игрок
    public Rigidbody playerRigidbody; // Rigidbody игрока для управления его движением

    void Start()
    {
        // Инициализация направления блуждания
        wanderDirection = GetRandomDirection();
        wanderTimer = wanderChangeTime;
    }

    void Update()
    {
        if (!hasCapturedPlayer)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Игрок в зоне обнаружения — начинаем преследование
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                // Игрок вне зоны обнаружения — блуждаем
                isChasing = false;
                Wander();
            }
        }
    }

    void Wander()
    {
        // Двигаемся в случайном направлении
        transform.position += wanderDirection * wanderSpeed * Time.deltaTime;

        // Таймер для смены направления
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f)
        {
            wanderDirection = GetRandomDirection();
            wanderTimer = wanderChangeTime;
        }
    }

    void ChasePlayer()
    {
        // Вычисляем направление к игроку
        Vector3 direction = (player.position - transform.position).normalized;

        // Двигаемся к игроку
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // Проверяем расстояние до игрока
        if (Vector3.Distance(transform.position, player.position) <= captureDistance)
        {
            CapturePlayer();
        }
    }

    void CapturePlayer()
    {
        hasCapturedPlayer = true; // Устанавливаем флаг, что игрок захвачен
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector3.zero; // Останавливаем движение игрока
            playerRigidbody.isKinematic = true; // Блокируем физику игрока
        }
        Time.timeScale = 0;
        Debug.Log("Игрок захвачен НЛО!");
    }

    Vector3 GetRandomDirection()
    {
        // Возвращает случайное направление на плоскости XZ
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        return new Vector3(randomX, 0, randomZ).normalized;
    }

    public void ReleasePlayer()
    {
        // Метод для освобождения игрока (если нужно)
        hasCapturedPlayer = false;
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false; // Восстанавливаем физику игрока
        }

        Debug.Log("Игрок освобожден!");
    }
}
