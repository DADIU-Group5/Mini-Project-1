using UnityEngine;
using System.Collections;

/// <summary>
/// Should be used to move the cart. The spawner should use that position to know in which lane to spawn the number in.
/// </summary>

public class Cart : MonoBehaviour {

    public float moveTimer = 2.0f;
    private float nextMove;
    private Spawner spawner;
    private Vector3 currentPosition;
    private Vector3 newPosition;
    private int currentLane = 2;

    void Update () {
        if (nextMove == Time.timeSinceLevelLoad) {
            MoveCart();
        }
	}

    public void MoveCart() {

        spawner.Spawn(currentPosition);
        nextMove = Time.timeSinceLevelLoad + moveTimer;
        //TODO: move cart to a random lane 

    }
}
