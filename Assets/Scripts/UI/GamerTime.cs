using TMPro;
using UnityEngine;

public class GamerTime : MonoBehaviour
{
        private float elapsedTime = 0f; // Прошедшее время
        public TextMeshProUGUI timerText;

        void Update()
        {
            Player.OXYGEN -= Time.deltaTime;
            elapsedTime += Time.deltaTime; // Увеличиваем время
            UpdateTimerDisplay();
        }

        void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60); // Минуты
            int seconds = Mathf.FloorToInt(elapsedTime % 60); // Секунды
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
}
