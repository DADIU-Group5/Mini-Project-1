using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    // singleton
    public static GameState _instance;

    // editable fields used for balancing game
    // speed
    [Header("Speed")]
    [Range(0, 50)]
    public float initialSpeed = 10;
    [Range(1, 20)]
    public int numbersPerSpeedIncrease = 10;
    [Range(0, 1)]
    public float speedMultiplierIncrease = 0.1f;
    // score
    [Header("Score")]
    [Range(1, 1000)]
    public int scorePerNumber = 10;
    [Range(1, 20)]
    public int numbersPerScoreIncrease = 10;
    [Range(0, 1)]
    public float scoreMultiplierIncrease = 0.1f;
    // number
    [Header("Number")]
    [Range(0, 99)]
    public int lastNumber = 0;
    [Range(1, 10)]
    public int numberToLoseLife = 5;
    [Range(1, 10)]
    public int numberToGiveLife = 6;
    // other
    [Header("Other")]
    public bool unbeatable;
    [Range(1, 10)]
    public int maxLifes = 3;
    [Range(1, 5)]
    public float laneWidth = 1.5f;

    public Cart cart;

    // For changing the player animation
    public Animator playerAnimator;

    // private fields
    private float score;
    private int missedNumbers;
    private int numberStreak;
    private float currentNumberSpeed;
    private int playerLives;
    private float currentSpeedMultiplier = 1;
    private float currentScoreMultiplier = 1;
    private int numberStreakWithoutMiss = 0;
    private bool completedNumberCycle;

    // Use this for initialization
    void Awake () {
        if (_instance == null)
        {
            _instance = this;
            Init();

            //Play footstep sound
           // AkSoundEngine.PostEvent("footstep", this.gameObject);
        }
        else
        {
            Debug.LogError("There should not be 2 GameStates, destroys the newly created GameState");
            Destroy(gameObject);
        }
	}

    private void Init()
    {
        currentNumberSpeed = initialSpeed;
        playerLives = maxLifes;
        
        int speedLevel = (lastNumber / numbersPerSpeedIncrease);
        currentNumberSpeed = initialSpeed + initialSpeed * (speedLevel * speedMultiplierIncrease);
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
            

            //Play pick up sound
           // AkSoundEngine.PostEvent("correctNumberPickup", this.gameObject);
            
            lastNumber = newNum;
            numberStreak++;
            if (lastNumber == 99)
                completedNumberCycle = true;

            // give extra life
            if (numberStreak >= numberToGiveLife && playerLives < maxLifes)
            {
                GiveLife();
                numberStreak = 0;
            }

            //Reset the amount of missed correct numbers.
            missedNumbers = 0;

            // update speed for first number cycle 0-99
            if (!completedNumberCycle)
            {
                int speedLevel = (lastNumber / numbersPerSpeedIncrease);
                currentNumberSpeed = initialSpeed + initialSpeed * (speedLevel * speedMultiplierIncrease);
            }

            //update score
            currentScoreMultiplier = 1 + (numberStreak / numbersPerScoreIncrease) * scoreMultiplierIncrease;
            score += scorePerNumber * currentScoreMultiplier;
            UIController._instance.UpdateScore((int)score);
            UIController._instance.UpdateMultiplier(currentScoreMultiplier);
        }
        else
        {
            numberStreak = 0;
            currentScoreMultiplier = 1;

            //Play pick up sound
          //  AkSoundEngine.PostEvent("wrongNumberPickup", this.gameObject);

            UIController._instance.UpdateMultiplier(currentScoreMultiplier);
            LoseLife();
        }
        UIController._instance.UpdateNextNumber(GetNextNumber());
    }

    // Makes the player lose a life. Initiates game-over if all lives are lost.
    public void LoseLife()
    {
        playerLives--;

        // Animate stumble
        playerAnimator.SetTrigger("Stumble");

        //Moves the cart further away from the player.
        cart.MoveCartAway(playerLives);
        UIController._instance.UpdateLives(playerLives);
       // Move cart away as player lose lives.
        if (playerLives <= 0 && !unbeatable)
        {
            // game over

            // Animate fall
            playerAnimator.SetBool("Fall", true);

            // update highscore if we beat it
            if (!PlayerPrefs.HasKey("HighScore") || PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", (int)score);
                PlayerPrefs.Save();
            }

            // load main menu
            //SceneManager.LoadScene(0);
            UIController._instance.DisplayLossScreen();
        }
    }

    public void GiveLife()
    {
        // Animate player speeding up to go nearer the cart
        playerAnimator.SetBool("Sprint", true);
        playerLives++;
        cart.MoveCartAway(playerLives);
        UIController._instance.UpdateLives(playerLives);
    }

    public void NumberMissed(int numberMissed)
    {
        if (numberMissed == GetNextNumber())
        {
            numberStreak = 0;
            missedNumbers++;
            // reduce the score multiplier
            currentScoreMultiplier -= scoreMultiplierIncrease;
            if (currentScoreMultiplier < 1)
                currentScoreMultiplier = 1;
            UIController._instance.UpdateMultiplier(currentScoreMultiplier);


            if (missedNumbers >= numberToLoseLife)
            {
                missedNumbers = 0;
                LoseLife();
            }
        }
    }
}
