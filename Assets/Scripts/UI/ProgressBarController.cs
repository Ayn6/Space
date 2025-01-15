using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider timeProgressBar; // Ссылка на UI-элемент Slider
    public Slider movementProgressBar; // Ссылка на UI-элемент Slider
    public float totalTime = Player.OXYGEN; // Общее время для прогресса
    private float remainingTime; // Оставшееся время

    public float movementTotal = 100f; // Максимальное значение шкалы движения
    private float remainingMovement;

    public Transform player; // Ссылка на объект игрока
    private Vector3 lastPosition; // Последняя позиция игрока
    public float movementCost = 0.5f; // Стоимость движения (уменьшение шкалы)


    void Start()
    {
        // Инициализация шкалы времени
        remainingTime = totalTime;
        timeProgressBar.maxValue = totalTime;
        timeProgressBar.value = totalTime;

        // Инициализация шкалы движения
        remainingMovement = movementTotal;
        movementProgressBar.maxValue = movementTotal;
        movementProgressBar.value = movementTotal;

        // Сохраняем стартовую позицию игрока
        lastPosition = player.position;
    }

    void Update()
    {
        // Обновление шкалы времени
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timeProgressBar.value = remainingTime;
        }
        else
        {
            OnTimeEnd();
        }

        // Обновление шкалы движения
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
        // Проверяем изменение позиции игрока
        if (player.position != lastPosition)
        {
            lastPosition = player.position;
            return true; // Игрок двигался
        }

        return false; // Игрок стоял на месте
    }

    void OnMovementEnd()
    {
        Debug.Log("Шкала движения закончилась!");
        Time.timeScale = 0;
        // Добавьте нужные действия, например, вывод сообщения
    }

    void OnTimeEnd()
    {
        Debug.Log("Время закончилось!");
        Time.timeScale = 0;
    }

    public void RefillMovementBar()
    {       
        remainingTime = movementTotal; // Заполняем шкалу движения
        movementProgressBar.value = movementTotal; // Обновляем значение шкалы

        remainingMovement = totalTime; // Заполняем шкалу движения
        timeProgressBar.value = totalTime; // Обновляем значение шкалы
    }
}
