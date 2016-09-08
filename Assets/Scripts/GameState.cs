﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameState : MonoBehaviour {

    /// <summary>
    /// Know the score, which number has to be accessed next etc.
    /// </summary>

    private int lastNumber = 0;

    public static GameState _instance;
    public int playerLives;

	// Use this for initialization
	void Awake () {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("There should not be 2 GameStates, destroys the newly created GameState");
            Destroy(gameObject);
        }
	}

    public int GetNumber()
    {
        return lastNumber;
    }

    // Receive a new number the player collected.
    public void PlayerGotNumber(int newNum)
    {
        //Determine if the right number was caught.
        if (newNum == lastNumber++)
        {
            lastNumber++;
            if (lastNumber >= 100)
            {
                // Loop number-counter back to zero.
                lastNumber = 0;
            }
        }
        else
        {
            playerLives--;
            if (playerLives <= 0)
            {
                // Game-Over, reload the game.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}