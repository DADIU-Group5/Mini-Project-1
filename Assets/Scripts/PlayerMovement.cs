using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player movement + input.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    enum Lane { Left, Middle, Right };
    private Vector3 leftLane = new Vector3(-2f, 0.5f, -14.5f);
    private Vector3 middleLane = new Vector3(0f, 0.5f, -14.5f);
    private Vector3 rightLane = new Vector3(2f, 0.5f, -14.5f);

    private Vector2 touchOrigin = -Vector2.one;

    private Animator hugoAnimator;

    // Current lane (0 - left, 1 - middle, 2 - right)
    private Lane lane;

    // Jumping animation
    [Range(0.1f,1.0f)]
    public float jumpTime = 1.0f;
    [Range(0.1f, 3.0f)]
    public float jumpHeight = 1.0f;
    private float jumpEndTime;
    Vector3 jumpStartPos, jumpEndPos;
    private bool canMove = true;

    // Use this for initialization
    void Start()
    {
        Transform transform = GetComponent<Transform>();
        transform.position = middleLane;
        lane = Lane.Middle;
        leftLane = new Vector3(0f - GameState._instance.laneWidth, 0.5f, -14.5f);
        rightLane = new Vector3(0f + GameState._instance.laneWidth, 0.5f, -14.5f);
        hugoAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.

        // TODO: Remove in final version.
        // Keyboard input - only for testing
        #if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            AttemptMove(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AttemptMove(1, 0);
        }

        #endif

        // Touch input
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
            
        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            //Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
            //Pass in horizontal and vertical as parameters to specify the direction to move Player in.
            AttemptMove(horizontal, vertical);
        }


        // Check if we are doing the jumping animation.
        if (Time.timeSinceLevelLoad < jumpEndTime)
        {
            float fraction = (jumpEndTime - Time.timeSinceLevelLoad) / (jumpEndTime - (jumpEndTime - jumpTime));

            float newX = Mathf.Lerp(jumpStartPos.x, jumpEndPos.x, 1 - fraction);

            float newZ = Mathf.Lerp(jumpStartPos.z, jumpEndPos.z, 1 - fraction);

            float newY = (fraction - fraction * fraction) * 4 * jumpHeight + jumpEndPos.y;

            if ((1 - fraction) >= 0.9f && hugoAnimator.GetBool("JumpFalling"))
            {
                hugoAnimator.SetBool("JumpFalling", false);
            }

            transform.position = new Vector3(newX, newY, newZ);
        }
        else
        {
            canMove = true;
        }
    }

    void AttemptMove(int horizontal, int vertical)
    {
        if (!canMove)
        {
            return;
        }
        Transform transform = GetComponent<Transform>();
        // Attempt move to left
        if (horizontal < 0)
        {
            // Not in leftmost lane
            if (lane != Lane.Left)
            {
                if (lane == Lane.Middle)
                {
                    lane = Lane.Left;
                    jumpEndTime = Time.timeSinceLevelLoad + jumpTime;
                    jumpStartPos = middleLane;
                    jumpEndPos = leftLane;
                    hugoAnimator.SetTrigger("Jump");
                    hugoAnimator.SetBool("JumpFalling", true);
                    canMove = false;
                }
                else
                {
                    lane = Lane.Middle;
                    jumpEndTime = Time.timeSinceLevelLoad + jumpTime;
                    jumpStartPos = rightLane;
                    jumpEndPos = middleLane;
                    hugoAnimator.SetTrigger("Jump");
                    hugoAnimator.SetBool("JumpFalling", true);
                    canMove = false;
                }
            }
        }
        else if (horizontal > 0)
        {
            // Not in rightmost lane
            if (lane != Lane.Right)
            {
                if (lane == Lane.Middle)
                {
                    lane = Lane.Right;
                    jumpEndTime = Time.timeSinceLevelLoad + jumpTime;
                    jumpStartPos = middleLane;
                    jumpEndPos = rightLane;
                    hugoAnimator.SetTrigger("Jump");
                    hugoAnimator.SetBool("JumpFalling", true);
                    canMove = false;
                }
                else
                {
                    lane = Lane.Middle;
                    jumpEndTime = Time.timeSinceLevelLoad + jumpTime;
                    jumpStartPos = leftLane;
                    jumpEndPos = middleLane;
                    hugoAnimator.SetTrigger("Jump");
                    hugoAnimator.SetBool("JumpFalling", true);
                    canMove = false;
                }
            }
        }
    }
}
