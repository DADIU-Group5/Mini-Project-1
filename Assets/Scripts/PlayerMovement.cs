using UnityEngine;
using System.Collections;

/// <summary>
/// Handles player movement + input.
/// </summary>
namespace Blub
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector3 leftLane = new Vector3(-3f, 0.5f, -8f);
        private Vector3 middleLane = new Vector3(0f, 0.5f, -8f);
        private Vector3 rightLane = new Vector3(3f, 0.5f, -8f);

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
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Transform transform = GetComponent<Transform>();

            // Move left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Not in leftmost lane
                if (lane != 0)
                {
                    if (lane == 1)
                    {
                        transform.position = leftLane;
                    }
                    else
                    {
                        transform.position = middleLane;
                    }
                }
            }

            // Move right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Not in leftmost lane
                if (lane != 2)
                {
                    if (lane == 1)
                    {
                        transform.position = rightLane;
                    }
                    else
                    {
                        transform.position = middleLane;
                    }
                }
            }
        }
    }
}
