﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

    /// <summary>
    /// Know the score, which number has to be accessed next etc.
    /// </summary>

    public static GameState _instance;

    private int lastNumber = 0;
    private int missedNumbers = 0;
    [Range(0,10)]
    float numberSpeed = 3;

    public int playerLives = 3;

    // Determines the width between the incoming numbers.
    // This is also used for determining how much the player and car moves.
    [Range(1f,3f)]
    public float laneWidth = 1.5f;
    public int missedNumbersThreshold = 5;


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

    public int GetNextNumber()
    {
        return lastNumber + 1;
    }

    public float GetNumberSpeed()
    {
        return numberSpeed;
    }

    public float GetLaneWidth()
    {
        return laneWidth;
    }

    public void PlayerGotNumber(int newNum)
    {
        //Determine if the right number was caught.
        if (newNum == GetNextNumber())
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
            LoseLife();
        }
        UIController._instance.UpdateNextNumber(GetNextNumber());
    }

    // Makes the player lose a life. Initiates game-over if all lives are lost.
    public void LoseLife()
    {
        playerLives--;
        UIController._instance.UpdateLives(playerLives);
        // TODO Move cart away as player lose lives.
        if (playerLives <= 0)
        {
            // Game-Over, reload the game.
            SceneManager.LoadScene(0);
        }
    }

    public void NumberMissed(int numberMissed)
    {
        if (numberMissed == GetNextNumber())
        {
            missedNumbers++;
            if (missedNumbers >= missedNumbersThreshold)
            {
                missedNumbers = 0;
                LoseLife();
            }
        }
    }
}
