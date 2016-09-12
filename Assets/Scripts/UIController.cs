using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Text multiplier;
    public GameObject losePanel;

    public PlayerMovement playerMove;

    private bool countingDown = false;
    private float countdown = 3;
    private DateTime countTo;

    /// <summary>
    /// Makes it a singleton.
    /// </summary>
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

    /// <summary>
    /// Updates the multiplier in the UI.
    /// </summary>
    /// <param name="newMultiplier"></param>
    public void UpdateMultiplier(float newMultiplier)
    {
        multiplier.text = "Multiplier: " + newMultiplier;
    }

    /// <summary>
    /// Pauses the game, disables the playermovement script.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
        playerMove.enabled = false;
    }

    /// <summary>
    /// Unpause Button handler.
    /// </summary>
    public void Unpuase()
    {
        countTo = DateTime.Now;
        countTo = countTo.AddSeconds(countdown);
        countingDown = true;
    }

    public void DisplayLossScreen()
    {
        //WIP
        //losePanel.gameObject.SetActive(true);
        playerMove.enabled = false;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Only used when counting down.
    /// </summary>
    void Update()
    {
        if (countingDown)
        {
            //Gets the current time.
            DateTime now = DateTime.Now;
            //Updates the visual text.
            unpauseCountdown.text = "Resuming in:\n"+(int)((countTo - now).TotalSeconds+1) + "!";
            //No longer puased
            if (countTo.CompareTo(now) < 0)
            {
                //Unpause functionality.
                Time.timeScale = 1;
                countingDown = false;
                unpauseCountdown.gameObject.SetActive(false);
                playerMove.enabled = true;
            }
        }
    }
}
