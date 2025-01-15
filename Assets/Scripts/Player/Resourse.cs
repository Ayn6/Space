using UnityEngine;

public class Resourse : MonoBehaviour
{
    public GameObject buttont;
    private GameObject currentResource;
    public ProgressBarController progressBarController;

    private void OnTriggerEnter(Collider other)
    {
        buttont.SetActive(true);
        currentResource = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        buttont.SetActive(false);
        currentResource = null;
    }

    public void GetResourse()
    {
        if(currentResource != null) 
        {
                progressBarController.RefillMovementBar();
                Destroy(currentResource); // Удаляем объект
                buttont.SetActive(false); // Скрываем кнопку
                currentResource = null ;
        }
    }
}
