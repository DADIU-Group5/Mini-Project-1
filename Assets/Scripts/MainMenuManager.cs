using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public Text highScoreLabel;

    void Start()
    {
        // get high score
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreLabel.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            highScoreLabel.text = "---";
        }  
    }

	public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}
