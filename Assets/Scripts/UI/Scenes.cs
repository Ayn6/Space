using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Вы вышли");
    }
}
