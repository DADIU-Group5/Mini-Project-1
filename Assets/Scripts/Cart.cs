using UnityEngine;
using System.Collections;

/// <summary>
/// Should be used to move the cart. The spawner should use that position to know in which lane to spawn the number in.
/// </summary>

public class Cart : MonoBehaviour {

    [Range(0.0f, 10.0f)]
    public float cartMoveTimer = 2.0f;

    float LaneWidth= 4.0f;

    private float nextMove;

    private Spawner spawner;
    private Vector3 currentPosition;
    private int currentLane = 1;
    private int newLane = 1;

    void Start()
    {
        spawner = GetComponent<Spawner>();
        LaneWidth = GameState._instance.GetLaneWidth();
    }

    void Update()
    {
        currentPosition = gameObject.transform.position;
        if (nextMove <= Time.time)
        {
            MoveCart();
        }
    }

    public void MoveCart()
    {
        spawner.Spawn(currentPosition);
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
