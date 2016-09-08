﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player movement + input.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    // TODO: Only for testing
    private bool touch = false;

    private Vector3 leftLane = new Vector3(-2f, 0.5f, -8f);
    private Vector3 middleLane = new Vector3(0f, 0.5f, -8f);
    private Vector3 rightLane = new Vector3(2f, 0.5f, -8f);

    private Vector2 touchOrigin = -Vector2.one;

    // Current lane (0 - left, 1 - middle, 2 - right)
    private int lane;

    // Use this for initialization
    void Start()
    {
        lane = 1;
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.

        // Movement
        // Using touch
        if (touch)
        {
            //Check if Input has registered more than zero touches
            if (Input.touchCount > 0)
            {
                //Store the first touch detected.
                Touch myTouch = Input.touches[0];

                //Check if the phase of that touch equals Began
                if (myTouch.phase == TouchPhase.Began)
                {
                    //If so, set touchOrigin to the position of that touch
                    touchOrigin = myTouch.position;
                }

                //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
                else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
                {
                    //Set touchEnd to equal the position of this touch
                    Vector2 touchEnd = myTouch.position;

                    //Calculate the difference between the beginning and end of the touch on the x axis.
                    float x = touchEnd.x - touchOrigin.x;

                    //Calculate the difference between the beginning and end of the touch on the y axis.
                    float y = touchEnd.y - touchOrigin.y;

                    //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                    touchOrigin.x = -1;

                    //Check if the difference along the x axis is greater than the difference along the y axis.
                    if (Mathf.Abs(x) > Mathf.Abs(y))
                        //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                        horizontal = x > 0 ? 1 : -1;
                    else
                        //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                        vertical = y > 0 ? 1 : -1;
                }
            }
        }
        // Using keyboard
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                horizontal = -1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                horizontal = 1;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                vertical = 1;
            }
        }

        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove(horizontal, vertical);
        }
    }

    void AttemptMove(int horizontal, int vertical)
    {
        Transform transform = GetComponent<Transform>();
        // Attempt move to left
        if (horizontal < 0)
        {
            // Not in leftmost lane
            if (lane != 0)
            {
                if (lane == 1)
                {
                    transform.position = leftLane;
                    lane = 0;
                }
                else
                {
                    transform.position = middleLane;
                    lane = 1;
                }
            }
        }
        // Attempt move to right
        else if (horizontal > 0)
        {
            // Not in rightmost lane
            if (lane != 2)
            {
                if (lane == 1)
                {
                    transform.position = rightLane;
                    lane = 2;
                }
                else
                {
                    transform.position = middleLane;
                    lane = 1;
                }
            }
        }
    }
}