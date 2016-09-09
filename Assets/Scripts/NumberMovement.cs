using UnityEngine;
using System.Collections;

/// <summary>
/// Handles Number movement.
/// </summary>

public class NumberMovement : MonoBehaviour {

    GameState gameState;
    public Rigidbody rigidBody;
    public float numberSpeed = 1f;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
        //set a constant velocity in the z-direction
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -1f * numberSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Despawner")
        {
            //send number info to GameState
            gameState.NumberMissed(gameObject.GetComponent<Number>().thisNumber);
            Destroy(gameObject);
        }
    }
}
