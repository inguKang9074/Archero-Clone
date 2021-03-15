using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickMove : MonoBehaviour
{
    public enum TouchState
    {
        IDLE,
        DOWN,
        DRAG,
        UP
    }
    public TouchState touchState;

    private Animator playerAnimator;

    [SerializeField] private Image joystickBackGround;
    [SerializeField] private Image stick;
    [SerializeField] private Canvas canvas;
    private Vector2 firstDownPos;
    private float backgroundRadius;
    private float stickRadius;

    private Vector3 joyDir;
    private Vector3 joystickPos;

    // Start is called before the first frame update
    void Start()
    {
        touchState = TouchState.IDLE;
        backgroundRadius = joystickBackGround.GetComponent<RectTransform>().sizeDelta.y / 2f * canvas.scaleFactor;
        stickRadius = stick.GetComponent<RectTransform>().sizeDelta.y / 2f * canvas.scaleFactor;
        joystickPos = joystickBackGround.transform.position;
    }

    public void PointerDown(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        firstDownPos = pointerEventData.position;
        joystickBackGround.transform.position = firstDownPos;
        stick.transform.position = firstDownPos;
        touchState = TouchState.IDLE;
    }

    public void PointerDrag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dir = (pointerEventData.position - firstDownPos).normalized;
        joyDir = new Vector3(dir.x, 0, dir.y);

        float stickDistance = Vector3.Distance(pointerEventData.position, firstDownPos);

        if (stickDistance < backgroundRadius - stickRadius)
            stick.transform.position = firstDownPos + dir * stickDistance;
        else
            stick.transform.position = firstDownPos + dir * (backgroundRadius - stickRadius);
        touchState = TouchState.DRAG;
    }

    public void PointerUp(BaseEventData baseEventData)
    {
        joystickBackGround.transform.position = joystickPos;
        stick.transform.position = joystickPos;
        joyDir = Vector3.zero;
        touchState = TouchState.UP;
    }

    public Vector3 GetJoystickDir()
    {
        return joyDir;
    }
}
