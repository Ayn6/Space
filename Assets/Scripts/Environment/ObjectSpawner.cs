using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectsToSpawn;  // ������ �������� ��� ���������
    public int numberOfObjects = 4;      // ���������� �������� ��� ���������
    public float radius = 5000f;         // ������ �����

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // ��������� ��������� � �������� �����
            Vector3 randomPosition = Random.insideUnitSphere * radius;

            // ��������� ������� �� ��������� �������
            Instantiate(objectsToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
