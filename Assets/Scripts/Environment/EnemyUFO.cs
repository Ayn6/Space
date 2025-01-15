using UnityEngine;

public class EnemyUFO : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public float detectionRange = 15f; // ������ ����������� ������
    public float chaseSpeed = 5f; // �������� �������������
    public float wanderSpeed = 2f; // �������� ���������
    public float captureDistance = 1.5f; // ���������, �� ������� ��� ����������� ������
    public float wanderChangeTime = 3f; // ����� ����� ����������� ���������

    private Vector3 wanderDirection; // ������� ����������� ���������
    private float wanderTimer; // ������ ��� ����� �����������
    private bool isChasing = false; // ����, ���������� �� ��� ������
    private bool hasCapturedPlayer = false; // ����, �������� �� �����
    public Rigidbody playerRigidbody; // Rigidbody ������ ��� ���������� ��� ���������

    void Start()
    {
        // ������������� ����������� ���������
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
                // ����� � ���� ����������� � �������� �������������
                isChasing = true;
                ChasePlayer();
            }
            else
            {
                // ����� ��� ���� ����������� � ��������
                isChasing = false;
                Wander();
            }
        }
    }

    void Wander()
    {
        // ��������� � ��������� �����������
        transform.position += wanderDirection * wanderSpeed * Time.deltaTime;

        // ������ ��� ����� �����������
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f)
        {
            wanderDirection = GetRandomDirection();
            wanderTimer = wanderChangeTime;
        }
    }

    void ChasePlayer()
    {
        // ��������� ����������� � ������
        Vector3 direction = (player.position - transform.position).normalized;

        // ��������� � ������
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // ��������� ���������� �� ������
        if (Vector3.Distance(transform.position, player.position) <= captureDistance)
        {
            CapturePlayer();
        }
    }

    void CapturePlayer()
    {
        hasCapturedPlayer = true; // ������������� ����, ��� ����� ��������
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector3.zero; // ������������� �������� ������
            playerRigidbody.isKinematic = true; // ��������� ������ ������
        }
        Time.timeScale = 0;
        Debug.Log("����� �������� ���!");
    }

    Vector3 GetRandomDirection()
    {
        // ���������� ��������� ����������� �� ��������� XZ
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        return new Vector3(randomX, 0, randomZ).normalized;
    }

    public void ReleasePlayer()
    {
        // ����� ��� ������������ ������ (���� �����)
        hasCapturedPlayer = false;
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false; // ��������������� ������ ������
        }

        Debug.Log("����� ����������!");
    }
}
