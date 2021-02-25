using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    static public Vector2 startTouch, swipeDelta;
    private bool isDraging = false;

    private void Update()
    {
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
        }
        if (isDraging)
        {
            swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
        else
        {
            swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
            }
        }
        if (isDraging)
        {
            if (Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
        }
        else
        {
            swipeDelta = Vector2.zero;
        }
        #endregion
    }
}