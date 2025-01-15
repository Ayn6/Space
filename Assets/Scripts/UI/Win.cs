using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject window;
    private void OnTriggerEnter(Collider other)
    {
        window.SetActive(true);
    }
}