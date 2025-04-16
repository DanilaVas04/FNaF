using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform joystickKnob;
    public float knobRange = 50f;
    public Vector2 inputVector = Vector2.zero;

    private Vector2 knobStartPosition;

    void Start()
    {
        knobStartPosition = joystickKnob.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickKnob.localPosition = knobStartPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out joystickPosition))
        {
            joystickPosition.x = Mathf.Clamp(joystickPosition.x, -knobRange, knobRange);
            joystickPosition.y = Mathf.Clamp(joystickPosition.y, -knobRange, knobRange);

            inputVector = joystickPosition / knobRange;
            joystickKnob.localPosition = joystickPosition;
        }
    }

    public float Horizontal
    {
        get { return inputVector.x; }
    }

    public float Vertical
    {
        get { return inputVector.y; }
    }
}
