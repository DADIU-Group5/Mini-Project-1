using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/// <summary>
/// Used to update the UI. Is a singleton, access using UIController._instance.METHOD
/// </summary>
public class UIController : MonoBehaviour {

    public static UIController _instance;

    public Text score;
    public Text nextNumber;
    public Text lives;
    public Text unpauseCountdown;

    public PlayerMovement playerMove;

    private bool countingDown = false;
    private float countdown = 3;
    private DateTime countTo;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("There should not be 2 UIControllers, destroys the newly created UIController");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Updates the score shown on the UI.
    /// </summary>
    /// <param name="newScore"></param>
    public void UpdateScore(int newScore)
    {
        score.text = "score: " + newScore;
    }

    /// <summary>
    /// Updates the number the player should hit next on the UI.
    /// </summary>
    /// <param name="nextNum"></param>
    public void UpdateNextNumber(int nextNum)
    {
        nextNumber.text = "Next: " + nextNum;
    }

    /// <summary>
    /// Updates the amount of lives the player has left, in the UI.
    /// </summary>
    /// <param name="newLives"></param>
    public void UpdateLives(int newLives)
    {
        lives.text = "Lives: " + newLives;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        playerMove.enabled = false;
    }

    public void Unpuase()
    {
        countTo = DateTime.Now;
        countTo = countTo.AddSeconds(countdown);
        countingDown = true;
    }

    void Update()
    {
        if (countingDown)
        {
            DateTime now = DateTime.Now;
            unpauseCountdown.text = "Resuming in:\n"+(int)((countTo - now).TotalSeconds+1) + "!";
            if (countTo.CompareTo(now) < 0)
            {
                Time.timeScale = 1;
                countingDown = false;
                unpauseCountdown.gameObject.SetActive(false);
                playerMove.enabled = true;
            }
        }
    }
}
