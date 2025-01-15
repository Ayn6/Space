using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public Transform linkedBlackHole; // Ссылка на другую "черную дыру"


    private void OnTriggerEnter(Collider other)
    {
        if (linkedBlackHole != null)
        {
            TeleportObject(other.gameObject);
        }
    }

    // Вспомогательный метод для телепортации объекта
    public void TeleportObject(GameObject obj)
    {
        // Проверяем наличие другой черной дыры
        if (linkedBlackHole == null) return;

        // Телепортируем объект
        Vector3 offset = new Vector3(2, 0, 0);
        obj.transform.position = linkedBlackHole.position + offset;

    }


}

