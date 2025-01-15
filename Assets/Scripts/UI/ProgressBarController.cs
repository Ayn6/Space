using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider timeProgressBar; // ������ �� UI-������� Slider
    public Slider movementProgressBar; // ������ �� UI-������� Slider
    public float totalTime = Player.OXYGEN; // ����� ����� ��� ���������
    private float remainingTime; // ���������� �����

    public float movementTotal = 100f; // ������������ �������� ����� ��������
    private float remainingMovement;

    public Transform player; // ������ �� ������ ������
    private Vector3 lastPosition; // ��������� ������� ������
    public float movementCost = 0.5f; // ��������� �������� (���������� �����)


    void Start()
    {
        // ������������� ����� �������
        remainingTime = totalTime;
        timeProgressBar.maxValue = totalTime;
        timeProgressBar.value = totalTime;

        // ������������� ����� ��������
        remainingMovement = movementTotal;
        movementProgressBar.maxValue = movementTotal;
        movementProgressBar.value = movementTotal;

        // ��������� ��������� ������� ������
        lastPosition = player.position;
    }

    void Update()
    {
        // ���������� ����� �������
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timeProgressBar.value = remainingTime;
        }
        else
        {
            OnTimeEnd();
        }

        // ���������� ����� ��������
        if (remainingMovement > 0 && HasPlayerMoved())
        {
            remainingMovement -= movementCost * Time.deltaTime;
            movementProgressBar.value = remainingMovement;
        }
        else if (remainingMovement <= 0)
        {
            OnMovementEnd();
        }
    }

    private bool HasPlayerMoved()
    {
        // ��������� ��������� ������� ������
        if (player.position != lastPosition)
        {
            lastPosition = player.position;
            return true; // ����� ��������
        }

        return false; // ����� ����� �� �����
    }

    void OnMovementEnd()
    {
        Debug.Log("����� �������� �����������!");
        Time.timeScale = 0;
        // �������� ������ ��������, ��������, ����� ���������
    }

    void OnTimeEnd()
    {
        Debug.Log("����� �����������!");
        Time.timeScale = 0;
    }

    public void RefillMovementBar()
    {       
        remainingTime = movementTotal; // ��������� ����� ��������
        movementProgressBar.value = movementTotal; // ��������� �������� �����

        remainingMovement = totalTime; // ��������� ����� ��������
        timeProgressBar.value = totalTime; // ��������� �������� �����
    }
}
