using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public Transform linkedBlackHole; // ������ �� ������ "������ ����"


    private void OnTriggerEnter(Collider other)
    {
        if (linkedBlackHole != null)
        {
            TeleportObject(other.gameObject);
        }
    }

    // ��������������� ����� ��� ������������ �������
    public void TeleportObject(GameObject obj)
    {
        // ��������� ������� ������ ������ ����
        if (linkedBlackHole == null) return;

        // ������������� ������
        Vector3 offset = new Vector3(2, 0, 0);
        obj.transform.position = linkedBlackHole.position + offset;

    }


}

