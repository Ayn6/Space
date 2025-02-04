using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{

    public Slider timeProgressBar; // Ññûëêà íà UI-ýëåìåíò Slider
    public Slider movementProgressBar; // Ññûëêà íà UI-ýëåìåíò Slider
    public float totalTime = Player.OXYGEN; // Îáùåå âðåìÿ äëÿ ïðîãðåññà
    private float remainingTime; // Îñòàâøååñÿ âðåìÿ

    public float movementTotal = 100f; // Ìàêñèìàëüíîå çíà÷åíèå øêàëû äâèæåíèÿ
    private float remainingMovement;

    public Transform player; // Ññûëêà íà îáúåêò èãðîêà
    private Vector3 lastPosition; // Ïîñëåäíÿÿ ïîçèöèÿ èãðîêà
    public float movementCost = 0.5f; // Ñòîèìîñòü äâèæåíèÿ (óìåíüøåíèå øêàëû)


    void Start()
    {
        // Èíèöèàëèçàöèÿ øêàëû âðåìåíè
        remainingTime = totalTime;
        timeProgressBar.maxValue = totalTime;
        timeProgressBar.value = totalTime;

        // Èíèöèàëèçàöèÿ øêàëû äâèæåíèÿ
        remainingMovement = movementTotal;
        movementProgressBar.maxValue = movementTotal;
        movementProgressBar.value = movementTotal;

        // Ñîõðàíÿåì ñòàðòîâóþ ïîçèöèþ èãðîêà
        lastPosition = player.position;
    }

    void Update()
    {
        // Îáíîâëåíèå øêàëû âðåìåíè
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timeProgressBar.value = remainingTime;
        }
        else
        {
            OnTimeEnd();
        }

        // Îáíîâëåíèå øêàëû äâèæåíèÿ
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
        // Ïðîâåðÿåì èçìåíåíèå ïîçèöèè èãðîêà
        if (player.position != lastPosition)
        {
            lastPosition = player.position;
            return true; // Èãðîê äâèãàëñÿ
        }

        return false; // Èãðîê ñòîÿë íà ìåñòå
    }

    void OnMovementEnd()
    {
        Debug.Log("Øêàëà äâèæåíèÿ çàêîí÷èëàñü!");
        Time.timeScale = 0;
        // Äîáàâüòå íóæíûå äåéñòâèÿ, íàïðèìåð, âûâîä ñîîáùåíèÿ
    }

    void OnTimeEnd()
    {
        Debug.Log("Âðåìÿ çàêîí÷èëîñü!");
        Time.timeScale = 0;
    }

    public void RefillMovementBar()
    {       
        remainingTime = movementTotal; // Çàïîëíÿåì øêàëó äâèæåíèÿ
        movementProgressBar.value = movementTotal; // Îáíîâëÿåì çíà÷åíèå øêàëû

        remainingMovement = totalTime; // Çàïîëíÿåì øêàëó äâèæåíèÿ
        timeProgressBar.value = totalTime; // Îáíîâëÿåì çíà÷åíèå øêàëû
    }
}
