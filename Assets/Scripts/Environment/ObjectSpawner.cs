using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectsToSpawn;  // Массив объектов для генерации
    public int numberOfObjects = 4;      // Количество объектов для генерации
    public float radius = 5000f;         // Радиус сферы

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Рандомное положение в пределах сферы
            Vector3 randomPosition = Random.insideUnitSphere * radius;

            // Генерация объекта на случайной позиции
            Instantiate(objectsToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
