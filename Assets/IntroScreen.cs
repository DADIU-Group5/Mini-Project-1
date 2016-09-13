using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroScreen : MonoBehaviour {

    public Image title;
    public Image buttonImage;
    public Button button;
    public GameObject swipe;

    public float opacityTime = 2f;
    float timer;
    bool started = true;

    public void StartTheGame()
    {
        button.interactable = false;
        started = false;
        UIController._instance.InitialStart();
        if(PlayerPrefs.GetInt("HighScore") == 0)
        {
            swipe.SetActive(true);
        }
        else
        {
            PlayerPrefs.DeleteAll();
        }
    }

    void Update()
    {
        if (started)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer >= opacityTime)
        {
            started = true;
        }
        title.color = Color.Lerp(Color.white, Color.clear, (timer / opacityTime));
        buttonImage.color = Color.Lerp(Color.white, Color.clear, (timer / opacityTime));

    }
}
