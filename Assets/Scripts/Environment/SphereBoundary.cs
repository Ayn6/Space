using UnityEngine;

public class SphereBoundary : MonoBehaviour
{
    public float radius = 5000f; // ������ �����
    public Vector3 center = Vector3.zero; // ����� ����� (�� ��������� � (0,0,0))

    private void Update()
    {
        // ��������� ���������� �� ������ �� ������ �����
        float distanceFromCenter = Vector3.Distance(transform.position, center);

        if (distanceFromCenter > radius)
        {
            // ���� ����� ������� �� �������, ���������� ��� �� ������� �����
            Vector3 direction = (transform.position - center).normalized;
            transform.position = center + direction * radius;
        }
    }
}
