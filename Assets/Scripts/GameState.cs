using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    // singleton
    public static GameState _instance;

    // editable fields used for balancing game
    [Range(0, 10)]
    public float initialSpeed = 5;
    [Range(0, 1)]
    public float speedMultiplierIncrease = 0.1f;
    [Range(0, 1)]
    public float scoreMultiplierIncrease = 0.1f;
    [Range(1, 10)]
    public int initialLifes = 3;
    [Range(1, 5)]
    public float laneWidth = 1.5f;
    // Determines the width between the incoming numbers.
    // This is also used for determining how much the player and car moves.
    [Range(1f, 3f)]
    public int missedNumbersThreshold = 5;

    // private fields
    private float score;
    private int lastNumber;
    private int missedNumbers;
    private int numberStreak;
    private float currentNumberSpeed;
    private int playerLives;
    private float currentSpeedMultiplier = 1;
    private float currentScoreMultiplier = 1;

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

    void Start()
    {
        currentNumberSpeed = initialSpeed;
        playerLives = initialLifes;
    }

    public int GetNumber()
    {
        return lastNumber;
    }

    public int GetNextNumber()
    {
        return lastNumber == 99 ? 0 : lastNumber + 1;
    }

    public float GetNumberSpeed()
    {
        return currentNumberSpeed;
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
            lastNumber = newNum;
            // update speed
            currentNumberSpeed = initialSpeed + (lastNumber / 10) * speedMultiplierIncrease;
            currentScoreMultiplier = (numberStreak / 10) * scoreMultiplierIncrease;
            score += currentScoreMultiplier;
            UIController._instance.UpdateScore((int)score);
        }
        else
        {
            currentScoreMultiplier = 1;
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
            // game over

            // update highscore if we beat it
            if (!PlayerPrefs.HasKey("HighScore") || PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", (int)score);
                PlayerPrefs.Save();
            }

            // load main menu
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
