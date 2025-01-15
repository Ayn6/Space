using TMPro;
using UnityEngine;

public class GamerTime : MonoBehaviour
{
        private float elapsedTime = 0f; // ��������� �����
        public TextMeshProUGUI timerText;

        void Update()
        {
            Player.OXYGEN -= Time.deltaTime;
            elapsedTime += Time.deltaTime; // ����������� �����
            UpdateTimerDisplay();
        }

        void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60); // ������
            int seconds = Mathf.FloorToInt(elapsedTime % 60); // �������
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
}
