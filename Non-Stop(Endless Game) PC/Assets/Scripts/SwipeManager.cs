// This script is used to manage swipe inputs in the game.
// It detects whether the player has swiped left, right, up, or down or has tapped on the screen.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    // Variables to track the swipe inputs.
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    private void Update()
    {
        // Reset the variables for every update.
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;
        #region Standalone Inputs
        // Check if the player is using a mouse or touchpad.
        if (Input.GetMouseButtonDown(0))
        {
            // If the player pressed the mouse button, it means they tapped the screen.
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // If the player released the mouse button, it means they have stopped swiping.
            isDraging = false;
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            // Check if the player is using a mobile device.
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                // If the player lifted their finger from the screen, it means they have stopped swiping.
                isDraging = false;
                Reset();
            }
        }
        #endregion

        // Calculate the distance of the swipe.
        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the distance?
        // Check if the player has swiped a minimum distance.
        if (swipeDelta.magnitude > 100)
        {
            //Which direction?
            // Determine the direction of the swipe.    
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                // If the player swiped horizontally, determine whether it was left or right.
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                // If the player swiped vertically, determine whether it was up or down.    
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            // Reset the variables.
            Reset();
        }

    }

    // Reset the variables.
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
