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

    private double hornSoundTimer = 2.0;
    private double nextHorn;
    private double animalsPartySoundTimer = 3.0;
    private double nextCheer;

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
    private GameObject[] numbers;
    private GameObject cartNumber;
    private int lastNumber = -1;

    // Cart jumping animation
    private Animator cartAnimator;
    [Range(0.1f, 1.0f)]
    public float jumpDuration = 0.17f;
    [Range(0.1f, 3.0f)]
    public float jumpHeight = 1.0f;
    private float jumpEndTime;
    private float jumpEndPos;
    private Vector2 jumpStartPos;

    void Start()
    {
        cartAnimator = gameObject.GetComponent<Animator>();
        nextMove = cartMoveTimer;

        spawner = GetComponent<Spawner>();
        LaneWidth = GameState._instance.GetLaneWidth();

        AkSoundEngine.PostEvent("jungleAmbience", this.gameObject);
        AkSoundEngine.PostEvent("truckEngine", this.gameObject);

        System.Random random = new System.Random();
        double rnd = random.NextDouble();

        nextHorn = GameState._instance.GetTimeSinceGameStarted() + rnd;

        rnd = random.NextDouble();

        nextCheer = GameState._instance.GetTimeSinceGameStarted() + rnd;

        cartNumber = gameObject.transform.Find("CartNumber").gameObject;
        numbers = new GameObject[1];
    }

    void Update()
    {
        if (nextHorn <= GameState._instance.GetTimeSinceGameStarted())
        {
            System.Random random = new System.Random();
            double rnd = random.NextDouble() * 16;

            nextHorn = GameState._instance.GetTimeSinceGameStarted() + rnd;

            AkSoundEngine.PostEvent("truckHonk", this.gameObject);
        }

        if (nextCheer <= GameState._instance.GetTimeSinceGameStarted())
        {
            System.Random random = new System.Random();
            double rnd = random.NextDouble() * 15;

            nextCheer = GameState._instance.GetTimeSinceGameStarted() + rnd;

            AkSoundEngine.PostEvent("partyAnimals", this.gameObject);
        }
        
        currentPosition = gameObject.transform.position;
        if (nextMove <= GameState._instance.GetTimeSinceGameStarted())
        {
            MoveCart();
        }
        //If moving on Z axis, towards or away from player.
        if (movingZ == true)
        {
            timer += Time.deltaTime;
            if (timer >= movementTime)
            {
                cartAnimator.SetBool("Brake", false);
                cartAnimator.SetBool("Accelerate", false);
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

        //update the next number.
        if (GameState._instance.lastNumber != lastNumber)
        {
         //   Debug.Log()
            lastNumber = GameState._instance.lastNumber;
            foreach (GameObject gm in numbers)
            {
                DestroyImmediate(gm, true);
            }

            string Digits = (lastNumber + 1).ToString();

            numbers = new GameObject[Digits.Length];

            float distance = -0.75f;

            for (int i = 0; i < Digits.Length; i++)
            {
                float addValue = 0;
                if (Digits.Length > 1 && i == 0)
                {
                    addValue = distance/2;
                }
                else
                    addValue = -distance/2;

                GameObject digit = (GameObject)Instantiate(Resources.Load("Textures/numbers_" + Digits[i]), cartNumber.transform.position + new Vector3(.0f, 0, -0.5f) + (cartNumber.transform.right * addValue), cartNumber.transform.rotation);
                digit.transform.SetParent(cartNumber.transform);
                numbers[i] = digit;
            }

        }

        if (GameState._instance.GetTimeSinceGameStarted() < jumpEndTime)
        {
            float fraction = (jumpEndTime - GameState._instance.GetTimeSinceGameStarted()) / (jumpEndTime - (jumpEndTime - jumpDuration));

            float newX = Mathf.Lerp(jumpStartPos.x, jumpEndPos, 1 - fraction);

            float newY = (fraction - fraction * fraction) * 4 * jumpHeight + spots[GameState._instance.getPlayerLives()].y;

            transform.position = new Vector3(newX, newY, transform.position.z);
        }
    }


    public void MoveCartAway(int slot, bool movingAway)
    {
        //Moving between spots.
        if(slot >= 1)
        {
            if (movingAway)
            {
                cartAnimator.SetBool("Accelerate", true);
            }
            else
            {
                cartAnimator.SetBool("Brake", true);
            }
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
        jumpEndTime = GameState._instance.GetTimeSinceGameStarted() + jumpDuration;
        jumpStartPos = new Vector2(transform.position.x, transform.position.y);

        int laneJumps = _newLane - _currentLane;
        Vector3 tempPosition = new Vector3(laneJumps * LaneWidth, 0, 0);

        jumpEndPos = transform.position.x + tempPosition.x;
    }
}
