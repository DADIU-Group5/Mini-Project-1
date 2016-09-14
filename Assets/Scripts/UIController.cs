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
    public Text loseScreenStats;
    public Disco rightLight;
    public Disco leftLight;
    public Image pauseButtonImage;

    public PlayerMovement playerMove;

    private bool countingDown = false;
    private float countdown = 3;
    private DateTime countTo;
    private Animator multiplierAnim;
    private bool fadingIn = false;
    public float fadeInTime = 2f;
    float timer;

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
        multiplierAnim = multiplier.GetComponent<Animator>();
    }

    /// <summary>
    /// Updates the score shown on the UI.
    /// </summary>
    /// <param name="newScore"></param>
    public void UpdateScore(int newScore)
    {
        score.text = "Score: " + newScore;
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
        Debug.Log("should not happen");
        multiplier.gameObject.SetActive(true);
        multiplierAnim.SetTrigger("Bigger");
        multiplier.text = "X " + newMultiplier;
    }

    public void UpdateMultiplierUp(float newMultiplier)
    {
        multiplierAnim.SetTrigger("Bigger");
        multiplier.text = "X " + newMultiplier;
        if(Math.Truncate(newMultiplier)==newMultiplier)
        {
            rightLight.QuickAnim();
            leftLight.QuickAnim();
        }
    }

    public void UpdateMultiplierDown(float newMultiplier)
    {
        multiplierAnim.SetTrigger("Smaller");
        multiplier.text = "X " + newMultiplier;
    }

    /// <summary>
    /// Pauses the game, disables the playermovement script.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
        playerMove.enabled = false;
        AkSoundEngine.Suspend(true);
        leftLight.Stop();
        rightLight.Stop();
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

    public void InitialStart()
    {
        Unpuase();
        timer = -1;
        fadingIn = true;
        playerMove.enabled = true;
    }

    public void DisplayLossScreen()
    {
        // stop all sounds
        //AkSoundEngine.StopAll();
        AkSoundEngine.PostEvent("gameOver", GameObject.Find("MusicEmitter"));
        //SoundEngine._instance.MoveMusicToNextLevel(0);


        losePanel.gameObject.SetActive(true);
        playerMove.enabled = false;
        RotatingWheel._instance.StopRotate();
        GameObject[] numbers = GameObject.FindGameObjectsWithTag("Number");
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i].GetComponent<NumberMovement>().Stop();
        }
        loseScreenStats.text = "Highscore: " + PlayerPrefs.GetInt("HighScore") + "\n" + score.text + "\nHighest number: " + GameState._instance.GetNumber();
        leftLight.Stop();
        rightLight.Stop();
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
            unpauseCountdown.text = (int)((countTo - now).TotalSeconds+1)+"";
            //No longer puased
            if (countTo.CompareTo(now) < 0)
            {
                //Unpause functionality.
                Time.timeScale = 1;
                countingDown = false;
                unpauseCountdown.gameObject.SetActive(false);
                playerMove.enabled = true;

                // resume sound
                AkSoundEngine.WakeupFromSuspend();

                leftLight.Resume();
                rightLight.Resume();
            }
        }
        if (fadingIn)
        {
            timer += Time.deltaTime;
            Color temp = Color.Lerp(Color.clear, Color.white, timer / fadeInTime);
            multiplier.color = temp;
            score.color = temp;
            pauseButtonImage.color = temp;
            if(timer >= fadeInTime)
            {
                fadingIn = false;
                GameState._instance.ResetTimeSinceGameStarted();
                GameState._instance.SetGameOver(false);
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(0);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        AkSoundEngine.StopAll();
        SceneManager.LoadScene(0);
    }
}
