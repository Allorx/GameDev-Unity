using UnityEngine;

public class TouchController : MonoBehaviour {
    public float magnitudeForSwipe = 35;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public static bool SwipeLeft { get { return swipeLeft; } }
    public static bool SwipeRight { get { return swipeRight; } }
    public static bool SwipeUp { get { return swipeUp; } }
    public static bool SwipeDown { get { return swipeDown; } }
    public static bool Tapped { get { return tap; } }
    public static bool TappedLeft { get { return tapLeft; } }
    public static bool TappedRight { get { return tapRight; } }
    public static bool touchControllerActive = false;

    public bool isPressed = false;
    static bool tap = false;
    static bool tapLeft;
    static bool tapRight;
    bool tapReset = true;
    static bool swipeLeft, swipeRight, swipeUp, swipeDown;
    Vector2 startTouch, swipeDelta;
    float screenWidth;

    void Start () {
        screenWidth = Screen.width;
    }

    void Update () {
        if (touchControllerActive) {
            InputCheck ();
            TapCheck ();
            //SwipeCheck();
        } else {
            tapLeft = false;
            tapRight = false;
            tap = false;
        }
    }

    void InputCheck () {
        // Editor Input
        if (Input.GetMouseButtonDown (0)) {
            isPressed = true;
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp (0)) {
            Reset ();
        }

        // Mobile Input
        if (Input.touchCount > 0) {
            if (Input.touches[0].phase == TouchPhase.Began) {
                isPressed = true;
                startTouch = Input.touches[0].position;
            } else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) {
                Reset ();
            }
        }
    }

    void TapCheck () {
        tapLeft = false;
        tapRight = false;
        tap = false;
        if (isPressed && tapReset) {
            tap = true;
            if (startTouch.x < screenWidth / 2) {
                tapLeft = true;
            } else if (startTouch.x > screenWidth / 2) {
                tapRight = true;
            }
            tapReset = false;
        }
    }

    void SwipeCheck () {
        swipeLeft = swipeDown = swipeRight = swipeUp = false;
        // Calculate Swipe Distance
        swipeDelta = Vector2.zero;
        if (isPressed) {
            if (Input.touchCount > 0) {
                swipeDelta = Input.touches[0].position - startTouch;
            } else if (Input.GetMouseButton (0)) {
                swipeDelta = (Vector2) Input.mousePosition - startTouch;
            }
        }
        // Swipe Direction
        if (swipeDelta.magnitude > magnitudeForSwipe) {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs (x) > Mathf.Abs (y)) {
                // Left or Right
                if (x < 0) {
                    swipeLeft = true;
                } else {
                    swipeRight = true;
                }
            } else {
                // Up or Down
                if (y < 0) {
                    swipeDown = true;
                } else {
                    swipeUp = true;
                }
            }

        }
    }

    void Reset () {
        startTouch = swipeDelta = Vector2.zero;
        isPressed = false;
        tapReset = true;
    }
}