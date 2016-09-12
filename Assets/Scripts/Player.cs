using UnityEngine;
using System.Collections;

/// <summary>
/// The player should chandle collision data.
/// </summary>

[RequireComponent(typeof(AkBank))]
public class Player : MonoBehaviour {

    GameState gameState;
	// Use this for initialization
	void Start () {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Number")
        {
            int caughtNumber = collision.gameObject.GetComponent<Number>().thisNumber;
            //send number info to GameState
            gameState.PlayerGotNumber(caughtNumber);
            AkSoundEngine.PostEvent("correctNumberPickup", this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
