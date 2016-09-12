using UnityEngine;
using System.Collections;

/// <summary>
/// Should be used to move the cart. The spawner should use that position to know in which lane to spawn the number in.
/// </summary>

[RequireComponent(typeof(AkBank))]
public class Cart : MonoBehaviour {

    [Range(0.0f, 10.0f)]
    public float cartMoveTimer = 2.0f;
    //Spots the cart can move to.
    public Vector3[] spots;
    //Curve the cart follows when moving in the z-axis.
    public AnimationCurve curve;
    //Time it takes to move between the spots.
    public float movementTime = 1f;

    float LaneWidth= 4.0f;

    private float nextMove;

    private Spawner spawner;
    private Vector3 currentPosition;
    private int currentLane = 1;
    private int newLane = 1;

    //Used to move the cart.
    private bool movingZ = false;
    private Vector3 targetZ;
    private Vector3 startZ;
    private float timer;
    private bool finalMove = false;

    void Start()
    {
        spawner = GetComponent<Spawner>();
        LaneWidth = GameState._instance.GetLaneWidth();
    }

    void Update()
    {
        currentPosition = gameObject.transform.position;
        if (nextMove <= Time.timeSinceLevelLoad)
        {
            MoveCart();
        }
        //If moving on Z axis, towards or away from player.
        if (movingZ == true)
        {
            timer += Time.deltaTime;
            if (timer >= movementTime)
            {
                movingZ = false;
            }
            else
            {
                Vector3 temp = Vector3.Lerp(startZ, targetZ, curve.Evaluate(timer / movementTime));
                temp.x = transform.position.x;
                transform.position = temp;
            }
        }
        //The final move, the cart moves into the horizon.
        if(finalMove == true)
        {
            timer += Time.deltaTime;
            if (timer >= movementTime)
            {
                finalMove = false;
                this.enabled = false;
            }
            else
            {
                Vector3 temp = Vector3.Lerp(startZ, targetZ, curve.Evaluate(timer / movementTime));
                temp.x = transform.position.x;
                transform.position = temp;
            }
        }
    }


    public void MoveCartAway(int slot)
    {
        //Moving between spots.
        if(slot >= 1)
        {
            targetZ = spots[slot];
            startZ = transform.position;
            timer = 0;
            movingZ = true;
        }
        //Player died.
        else
        {
            targetZ = spots[slot];
            startZ = transform.position;
            timer = 0;
            finalMove = true;
            //double the movement time, for better animation, as it is longer.
            movementTime *= 2;
        }
    }

    public void MoveCart()
    {
        if(finalMove == true)
        {
            return;
        }

        spawner.Spawn(currentPosition);
        AkSoundEngine.PostEvent("numberDrop", this.gameObject);

        nextMove += cartMoveTimer;
        if (currentLane == newLane)
        {
            int random = Random.Range(1, 3);
            newLane = (currentLane + random) % 3;
        }
        SetPosition(currentLane, newLane);
        currentLane = newLane;
    }

    private void SetPosition(int _currentLane, int _newLane)
    {
        int laneJumps = _newLane - _currentLane;
        Vector3 tempPosition = new Vector3(laneJumps * LaneWidth, 0, 0);
        gameObject.transform.position += tempPosition;
    }
}
