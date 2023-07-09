//using UnityEngine.EventSystems;
using UnityEngine;


public class MovementJoystick : MonoBehaviour
{
    [SerializeField] private GameObject joystickBG, joystick;
    private Vector2 joystickVector2 = Vector2.zero, joystickOriginalPos = Vector2.zero, joystickTouchPos = Vector2.zero;
    private float joystickRadius = 0, joystickReach = 80;


    public Vector2 GetJoystickVector2(){return joystickVector2;}

    public Vector2 GetJoystickOriginalPos() { return joystickOriginalPos; }

    public Vector2 GetJoystickTouchPos() { return joystickTouchPos; }



    void Start()
    {
        joystickOriginalPos = joystick.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 2.3f;
    }
    
    public void PointerDown()
    {
        joystickTouchPos = Input.mousePosition;
        if (Vector2.Distance(joystickTouchPos, joystickOriginalPos) < joystickReach) 
        {
            joystick.transform.position = Input.mousePosition;
            joystickBG.transform.position = Input.mousePosition;
        }         
    }

    public void Drag(UnityEngine.EventSystems.BaseEventData baseEventData)
    {
        if (Vector2.Distance(joystickTouchPos, joystickOriginalPos) < joystickReach)
        {
            UnityEngine.EventSystems.PointerEventData pointerEventData = baseEventData as UnityEngine.EventSystems.PointerEventData;
            Vector2 dragPos = pointerEventData.position;
            joystickVector2 = (dragPos - joystickTouchPos).normalized;

            float joystickDistance = Vector2.Distance(dragPos, joystickTouchPos);

            if (joystickDistance < joystickRadius)
            {
                joystick.transform.position = joystickTouchPos + joystickVector2 * joystickDistance;
            }

            else
            {
                joystick.transform.position = joystickTouchPos + joystickVector2 * joystickRadius;
            }
        }
    }

    public void PointerUp()
    {
        joystickVector2 = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }
   
}
