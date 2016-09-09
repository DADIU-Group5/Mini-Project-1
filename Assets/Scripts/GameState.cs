using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

    /// <summary>
    /// Know the score, which number has to be accessed next etc.
    /// </summary>

    public static GameState _instance;

    int lastNumber = 20;

    List<int> missedNumbers;
    public int missedCorrectNumberTimes = 0;

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

        missedNumbers = new List<int>();
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
        if (newNum == lastNumber + 1)
        {
            //you got the right number! Profit!
            lastNumber += 1;

        } else if (newNum != lastNumber + 1)
        {
            //you caught the wrong number! Penalty!

        }
    }

    public void NumberMissed(int numberMissed)
    {
        missedNumbers.Add(numberMissed); //add to list

        if (numberMissed == lastNumber + 1) //if it was the correct number...
            missedCorrectNumberTimes += 1; //missed count + 1
    }
}
