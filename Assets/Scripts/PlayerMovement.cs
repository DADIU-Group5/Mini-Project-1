using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player movement + input.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    enum Lane { Left, Middle, Right };

    // editable fields
    [Range(0, 10000)]
    public int pixelForSwipe = 400;


    private Vector3 leftLane = new Vector3(-2f, 0f, -14.5f);
    private Vector3 middleLane = new Vector3(0f, 0f, -14.5f);
    private Vector3 rightLane = new Vector3(2f, 0f, -14.5f);

    private Vector2 touchOrigin = -Vector2.one;

    private Animator hugoAnimator;
    private bool canSwipe;

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
        leftLane = new Vector3(0f - GameState._instance.laneWidth, 0f, -14.5f);
        rightLane = new Vector3(0f + GameState._instance.laneWidth, 0f, -14.5f);
        hugoAnimator = GetComponentInChildren<Animator>();
        canSwipe = true;
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
            AttemptMove(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AttemptMove(1);
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
                canSwipe = true;
            }
                
            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Moved && canSwipe)
            {
                // right
                if (myTouch.position.x - touchOrigin.x > pixelForSwipe) {
                    AttemptMove(1);
                    canSwipe = false;
                }

                //left
                if (myTouch.position.x - touchOrigin.x < -pixelForSwipe)
                {
                    AttemptMove(-1);
                    canSwipe = false;
                }
            }
        }
        
        // Check if we are doing the jumping animation.
        if (Time.timeSinceLevelLoad < jumpEndTime)
        {
            float fraction = (jumpEndTime - Time.timeSinceLevelLoad) / (jumpEndTime - (jumpEndTime - jumpTime));

            float newX = Mathf.Lerp(jumpStartPos.x, jumpEndPos.x, 1 - fraction);

            float newZ = Mathf.Lerp(jumpStartPos.z, jumpEndPos.z, 1 - fraction);

            float newY = (fraction - fraction * fraction) * 4 * jumpHeight + jumpEndPos.y;

            transform.position = new Vector3(newX, newY, newZ);
        }
        else
        {
            canMove = true;
        }
    }

    void AttemptMove(int horizontal)
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
                    canMove = false;
                }
                else
                {
                    lane = Lane.Middle;
                    jumpEndTime = Time.timeSinceLevelLoad + jumpTime;
                    jumpStartPos = rightLane;
                    jumpEndPos = middleLane;
                    hugoAnimator.SetTrigger("Jump");
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
                    canMove = false;
                }
                else
                {
                    lane = Lane.Middle;
                    jumpEndTime = Time.timeSinceLevelLoad + jumpTime;
                    jumpStartPos = leftLane;
                    jumpEndPos = middleLane;
                    hugoAnimator.SetTrigger("Jump");
                    canMove = false;
                }
            }
        }
    }
}
