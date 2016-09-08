using UnityEngine;
using System.Collections;

/// <summary>
/// Handles Number movement.
/// Moves towards player (one dimension)
/// </summary>

public class NumberMovement : MonoBehaviour {

    public Rigidbody rigidBody;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //set a constant velocity in the z-direction
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -1f * speed);
    }
}
