using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField] private Transform player; // Ссылка на объект игрока

    private void Start()
    {
        LoadPlayerPosition(); // Загружаем сохраненные данные при запуске игры
    }

    public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.position.z);
        PlayerPrefs.Save(); // Сохраняем изменения
        Debug.Log("Позиция игрока сохранена!");
    }

    public void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX")) // Проверяем, есть ли сохраненные данные
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            player.position = new Vector3(x, y, z);
            Debug.Log("Позиция игрока загружена!");
        }
        else
        {
            Debug.Log("Сохраненные данные отсутствуют.");
        }
    }
}
