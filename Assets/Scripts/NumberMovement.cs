﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Handles Number movement.
/// </summary>

public class NumberMovement : MonoBehaviour {

    public Rigidbody rigidBody;
    
    //numberspeed is set in the GameState.
    //Every new number will be instantiated with the correct speed,
    //but number from before the change will not have the correct speed.
    public float numberSpeed = 1f;

    /* //for when we add the ground
    public Transform road;
    float gravityForce = 10;

    void FixedUpdate()
    {
        Vector3 gravityDirection = (transform.position - road.position).normalized;// normalize;
        rigidBody.AddForce(gravityDirection* gravityForce);
    }*/


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        numberSpeed = GameState._instance.GetNumberSpeed();
	}
	
	// Update is called once per frame
	void Update () {
        //set a constant velocity in the z-direction
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -1f * numberSpeed);
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
        this.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Despawner")
        {
            //send number info to GameState
            GameState._instance.NumberMissed(gameObject.GetComponent<Number>().thisNumber);
            Destroy(gameObject);
        }
    }
}
