using UnityEngine;

public class SphereBoundary : MonoBehaviour
{
    public float radius = 5000f; // Радиус сферы
    public Vector3 center = Vector3.zero; // Центр сферы (по умолчанию в (0,0,0))

    private void Update()
    {
        // Проверяем расстояние от игрока до центра сферы
        float distanceFromCenter = Vector3.Distance(transform.position, center);

        if (distanceFromCenter > radius)
        {
            // Если игрок выходит за пределы, возвращаем его на границу сферы
            Vector3 direction = (transform.position - center).normalized;
            transform.position = center + direction * radius;
        }
    }
}
