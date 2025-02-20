using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField] private Transform player; // ������ �� ������ ������

    private void Start()
    {
        LoadPlayerPosition(); // ��������� ����������� ������ ��� ������� ����
    }

    public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.position.z);
        PlayerPrefs.Save(); // ��������� ���������
        Debug.Log("������� ������ ���������!");
    }

    public void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX")) // ���������, ���� �� ����������� ������
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            player.position = new Vector3(x, y, z);
            Debug.Log("������� ������ ���������!");
        }
        else
        {
            Debug.Log("����������� ������ �����������.");
        }
    }
}
