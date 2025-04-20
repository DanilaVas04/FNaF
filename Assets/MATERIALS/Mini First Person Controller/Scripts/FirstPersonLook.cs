using UnityEngine;
using YG;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity = Vector2.zero;
    Vector2 frameVelocity = Vector2.zero;
    Vector2 previousTouchPosition;
    bool isTouching = false;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Ensure initial rotations are set correctly
        transform.localRotation = Quaternion.identity;
        character.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (Joystick.IsJoystickActive)
        {
            // Do not process camera input if the joystick is active
            return;
        }

        // Always handle mouse input
        HandleMouseInput();

        // Handle touch input if available
        if (Input.touchCount > 0)
        {
            HandleTouchInput();
        }
    }

    void HandleMouseInput()
    {
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.Euler(-velocity.y, 0, 0);
        character.localRotation = Quaternion.Euler(0, velocity.x, 0);
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0 && !Joystick.IsJoystickActive)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 touchDelta = touch.position - previousTouchPosition;
                previousTouchPosition = touch.position;

                // Get smooth velocity.
                Vector2 rawFrameVelocity = Vector2.Scale(touchDelta, Vector2.one * sensitivity * Time.deltaTime);
                frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
                velocity += frameVelocity;
                velocity.y = Mathf.Clamp(velocity.y, -90, 90);

                // Rotate camera up-down and controller left-right from velocity.
                transform.localRotation = Quaternion.Euler(-velocity.y, 0, 0);
                character.localRotation = Quaternion.Euler(0, velocity.x, 0);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }
}

