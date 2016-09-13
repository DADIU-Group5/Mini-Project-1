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

    void Start()
    {
        spawner = GetComponent<Spawner>();
        LaneWidth = GameState._instance.GetLaneWidth();

    //    AkSoundEngine.PostEvent("jungleAmbience", this.gameObject);
      //  AkSoundEngine.PostEvent("truckEngine", this.gameObject);

        System.Random random = new System.Random();
        double rnd = random.NextDouble();

        nextHorn = Time.timeSinceLevelLoad + rnd;

        rnd = random.NextDouble();

        nextCheer = Time.timeSinceLevelLoad + rnd;

        cartNumber = gameObject.transform.Find("CartNumber").gameObject;
        numbers = new GameObject[1];
    }

    void Update()
    {
        if (nextHorn <= Time.timeSinceLevelLoad)
        {
            System.Random random = new System.Random();
            double rnd = random.NextDouble() * 10;

            nextHorn = Time.timeSinceLevelLoad + rnd;

          //  AkSoundEngine.PostEvent("truckHonk", this.gameObject);
        }

        if (nextCheer <= Time.timeSinceLevelLoad)
        {
            System.Random random = new System.Random();
            double rnd = random.NextDouble() * 10;

            nextCheer = Time.timeSinceLevelLoad + rnd;

          //  AkSoundEngine.PostEvent("partyAnimals", this.gameObject);
        }
        
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
        //AkSoundEngine.PostEvent("numberDrop", this.gameObject);

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
