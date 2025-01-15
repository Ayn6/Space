using System.Collections;
using UnityEngine;

public class JoysticController : MonoBehaviour
{
    public JoysticMove leftJoystick;  // Левый джойстик для передвижения
    public JoysticMove rightJoystick; // Правый джойстик для вертикального движения и поворота камеры

    public float moveSpeed = 5f;           // Скорость движения
    public float verticalSpeed = 3f;      // Скорость вертикального движения
    public float rotationSpeed = 100f;    // Скорость поворота
 
    public float fuelConsumptionRate = 10f; // Расход топлива

    private bool clic = false;

    public float pushForce = 10f; // Сила отталкивания от метеорита

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
            Debug.Log("Топливо закончилось!");
            return;
        }
    }

    void HandleMovement()
    {
        // Получение данных с джойстиков
        float moveHorizontal = leftJoystick.Horizontal();
        float moveVertical = leftJoystick.Vertical();
        float moveUpDown = rightJoystick.Vertical();

        // Вычисление скорости и расхода топлива
        float currentMoveSpeed = clic ? moveSpeed * 2 : moveSpeed;
        float currentFuelConsumption = clic ? fuelConsumptionRate * 3 : fuelConsumptionRate;

        // Перемещение
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical);
        if (moveDirection.magnitude > 0)
        {
            Player.FLUE -= currentFuelConsumption * Time.deltaTime; // Расход топлива
        }

        transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime, Space.Self);

        // Вертикальное перемещение
        if (moveUpDown != 0)
        {
            Player.FLUE -= currentFuelConsumption * Time.deltaTime; // Расход топлива
        }

        transform.Translate(Vector3.up * moveUpDown * verticalSpeed * Time.deltaTime, Space.World);
    }

    void HandleCameraRotation()
    {
        // Получение данных с правого джойстика для поворота
        float rotateHorizontal = rightJoystick.Horizontal();
        if (rotateHorizontal != 0)
        {
            transform.Rotate(0f, rotateHorizontal * rotationSpeed * Time.deltaTime, 0f);
        }
    }

    public void ToggleBoost()
    {
        // Переключение ускорения
        clic = !clic;

        if (clic)
        {
            Debug.Log("Ускорение включено!");
        }
        else
        {
            Debug.Log("Ускорение выключено!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, что столкновение произошло с метеоритом
            // Вычисляем направление отталкивания
            Vector3 pushDirection = transform.position - collision.transform.position;
            pushDirection = pushDirection.normalized;
        clic = !clic;
        // Применяем силу отталкивания
        rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);

            Debug.Log("Столкновение с метеоритом! Отталкивание...");

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
