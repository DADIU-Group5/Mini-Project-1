﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    // Singleton.
    public static GameState _instance;

    // Editable fields used for balancing game.
    // Speed.
    [Header("Speed")]
    [Range(0, 50)]
    public float initialSpeed = 10;
    [Range(1, 20)]
    public int numbersPerSpeedIncrease = 10;
    [Range(0, 1)]
    public float speedMultiplierIncrease = 0.1f;

    // Score.
    [Header("Score")]
    [Range(1, 1000)]
    public int scorePerNumber = 10;
    [Range(1, 20)]
    public int numbersPerScoreIncrease = 10;
    [Range(0, 1)]
    public float scoreMultiplierIncrease = 0.1f;

    // Other.
    [Header("Other")]
    public bool unbeatable;
    [Range(1, 10)]
    public int maxLifes = 3;
    [Range(1, 5)]
    public float laneWidth = 1.5f;
    [Range(1f, 3f)]
    public int missedNumbersThreshold = 5;
    public int numberToGiveLife = 6;
    public int lastNumber;

    public Cart cart;

    // For changing the player animation.
    public Animator playerAnimator;

    // Private fields.
    private bool gameOver = false;
    private float score;
    private int missedNumbers;
    private int numberStreak;
    private float currentNumberSpeed;
    private int playerLives;
    private float currentSpeedMultiplier = 1;
    private float currentScoreMultiplier = 0;
    private int numberStreakWithoutMiss = 0;

    private float timeSinceGameStarted = 0;

    // Use this for initialization.
    void Awake () {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
        else
        {
            Debug.LogError("There should not be 2 GameStates, destroys the newly created GameState");
            Destroy(gameObject);
        }
	}

    private void Init()
    {
        ResetTimeSinceGameStarted();
        gameOver = true;
        currentNumberSpeed = initialSpeed;
        RotatingWheel._instance.ChangeWheelSpeed(currentNumberSpeed);
        playerLives = maxLifes;
    }

    private void Update()
    {
        // Update score every tick by the number-speed.
        if (!gameOver)
        {
            score += Time.deltaTime * currentNumberSpeed;
            UIController._instance.UpdateScore((int)score);
        }
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

    public float GetMultiplier()
    {
        return 1 + currentScoreMultiplier * scoreMultiplierIncrease;
    }

    public void PlayerGotNumber(int newNum)
    {
        // Determine if the right number was caught.
        if (newNum == GetNextNumber())
        {
            lastNumber = newNum;
            numberStreak++;
            // Reset the amount of missed correct numbers.
            missedNumbers = 0;

            if (numberStreak >= numberToGiveLife && playerLives < maxLifes)
            {
                GiveLife();
                numberStreak = 0;
            }

            // Play pick up sound.
            AkSoundEngine.PostEvent("correctNumberPickup", this.gameObject);
            
            // Update speed.
            int speedLevel = (lastNumber / numbersPerSpeedIncrease);
            currentNumberSpeed = initialSpeed + initialSpeed * (speedLevel * speedMultiplierIncrease);

            // Update wheel-speed.
            RotatingWheel._instance.ChangeWheelSpeed(currentNumberSpeed);

            // Update score.
            score += scorePerNumber * GetMultiplier();
            currentScoreMultiplier += 1;
            UIController._instance.UpdateScore((int)score);
            UIController._instance.UpdateMultiplierUp(GetMultiplier());

            // pump music
            SoundEngine._instance.MoveMusicToNextLevel(speedLevel + 1);
        }
        else
        {
            numberStreak = 0;
            currentScoreMultiplier = 0;

            // Play pick up sound.
            AkSoundEngine.PostEvent("wrongNumberPickup", this.gameObject);

            UIController._instance.UpdateMultiplierDown(GetMultiplier());
            LoseLife();
        }
        UIController._instance.UpdateNextNumber(GetNextNumber());
    }

    // Makes the player lose a life. Initiates game-over if all lives are lost.
    public void LoseLife()
    {
        playerLives--;

        // Animate stumble.
        playerAnimator.SetTrigger("Stumble");

        // Moves the cart further away from the player.
        cart.MoveCartAway(playerLives, true);
        UIController._instance.UpdateLives(playerLives);
       // Move cart away as player lose lives.
        if (playerLives <= 0 && !unbeatable)
        {
            // Game over.
            gameOver = true;

            // Animate fall.
            playerAnimator.SetBool("Fall", true);

            // Update highscore if we beat it.
            if (!PlayerPrefs.HasKey("HighScore") || PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", (int)score);
                PlayerPrefs.Save();
            }

            // Load main menu.
            //SceneManager.LoadScene(0);
            UIController._instance.DisplayLossScreen();
        }
    }

    public int getPlayerLives()
    {
        return playerLives;
    }

    public void GiveLife()
    {
        // Animate player speeding up to go nearer the cart.
        GameObject.Find("Hugo").GetComponent<PlayerMovement>().startSprint();
        playerLives++;
        cart.MoveCartAway(playerLives, false);
        UIController._instance.UpdateLives(playerLives);
    }

    public void NumberMissed(int numberMissed)
    {
        if (numberMissed == GetNextNumber())
        {
            numberStreak = 0;
            missedNumbers++;
            // Reduce the score multiplier.
            currentScoreMultiplier -= 1;
            if (currentScoreMultiplier < 0)
                currentScoreMultiplier = 0;
            UIController._instance.UpdateMultiplierDown(GetMultiplier());


            if (missedNumbers >= missedNumbersThreshold)
            {
                missedNumbers = 0;
                LoseLife();
            }
        }
    }

    public float GetTimeSinceGameStarted()
    {
        if (gameOver)
        {
            return 0;
        }
        return Time.timeSinceLevelLoad - timeSinceGameStarted;
    }

    public void SetGameOver(bool b)
    {
        gameOver = b;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void ResetTimeSinceGameStarted()
    {
        timeSinceGameStarted = Time.timeSinceLevelLoad;
    }
}
