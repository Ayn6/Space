using System.Collections;
using UnityEngine;

public class JoysticController : MonoBehaviour
{
    public JoysticMove leftJoystick;  // ����� �������� ��� ������������
    public JoysticMove rightJoystick; // ������ �������� ��� ������������� �������� � �������� ������

    public float moveSpeed = 5f;           // �������� ��������
    public float verticalSpeed = 3f;      // �������� ������������� ��������
    public float rotationSpeed = 100f;    // �������� ��������
 
    public float fuelConsumptionRate = 10f; // ������ �������

    private bool clic = false;

    public float pushForce = 10f; // ���� ������������ �� ���������

    public Rigidbody rb;

    void Update()
    {
        if (Player.FLUE > 0)
        {
            HandleMovement();

            HandleCameraRotation();
        }
        else
        {
            Debug.Log("������� �����������!");
            return;
        }
    }

    void HandleMovement()
    {
        // ��������� ������ � ����������
        float moveHorizontal = leftJoystick.Horizontal();
        float moveVertical = leftJoystick.Vertical();
        float moveUpDown = rightJoystick.Vertical();

        // ���������� �������� � ������� �������
        float currentMoveSpeed = clic ? moveSpeed * 2 : moveSpeed;
        float currentFuelConsumption = clic ? fuelConsumptionRate * 3 : fuelConsumptionRate;

        // �����������
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical);
        if (moveDirection.magnitude > 0)
        {
            Player.FLUE -= currentFuelConsumption * Time.deltaTime; // ������ �������
        }

        transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime, Space.Self);

        // ������������ �����������
        if (moveUpDown != 0)
        {
            Player.FLUE -= currentFuelConsumption * Time.deltaTime; // ������ �������
        }

        transform.Translate(Vector3.up * moveUpDown * verticalSpeed * Time.deltaTime, Space.World);
    }

    void HandleCameraRotation()
    {
        // ��������� ������ � ������� ��������� ��� ��������
        float rotateHorizontal = rightJoystick.Horizontal();
        if (rotateHorizontal != 0)
        {
            transform.Rotate(0f, rotateHorizontal * rotationSpeed * Time.deltaTime, 0f);
        }
    }

    public void ToggleBoost()
    {
        // ������������ ���������
        clic = !clic;

        if (clic)
        {
            Debug.Log("��������� ��������!");
        }
        else
        {
            Debug.Log("��������� ���������!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ��� ������������ ��������� � ����������
            // ��������� ����������� ������������
            Vector3 pushDirection = transform.position - collision.transform.position;
            pushDirection = pushDirection.normalized;
        clic = !clic;
        // ��������� ���� ������������
        rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);

            Debug.Log("������������ � ����������! ������������...");

    }

    private void OnCollisionExit(Collision collision)
    {
        StartCoroutine(Stop());
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.5f);
        rb.linearVelocity = Vector3.zero;
    }
}
