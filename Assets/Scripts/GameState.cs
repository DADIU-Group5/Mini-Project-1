﻿using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

    /// <summary>
    /// Know the score, which number has to be accessed next etc.
    /// </summary>

    public static GameState _instance;

    int lastNumber = 20;

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
        if(lastNumber == 100)
        {
            return 1;
        }
        else
        {
            return lastNumber + 1;
        }
    }

    public void PlayerGotNumber(int newNum)
    {
        //Code to handle the number the player recieved.
    }
}
