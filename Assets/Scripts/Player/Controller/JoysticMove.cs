using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoysticMove : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform joysticBase;
    [SerializeField] private RectTransform joysticHadle;
    private Vector2 input;

    public void OnDrag(PointerEventData pointerEvent)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joysticBase, pointerEvent.position, pointerEvent.pressEventCamera, out position);
        position = Vector2.ClampMagnitude(position, joysticBase.sizeDelta.x / 2);
        joysticHadle.anchoredPosition = position;
        input = position / (joysticBase.sizeDelta.x / 2);
    }

    public void OnPointerDown(PointerEventData pointerEvent)
    {
        OnDrag(pointerEvent);
    }

    public void OnPointerUp(PointerEventData pointerEvent)
    {
        joysticHadle.anchoredPosition = Vector2.zero;
        input = Vector2.zero;
    }

    public float Horizontal()
    {
        return input.x;
    }

    public float Vertical()
    {
        return input.y;
    }
}
