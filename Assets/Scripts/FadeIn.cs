using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {

    public Image panel;
    public Color start;
    public Color end;
    public float fadeTime = 3;
    public Image buttonImg;
    public Image discoOne;
    public Image discoTwo;
    public Text mult;
    public Text score;


    public GameObject holder;

    Color col;
    Color invCol;
    float timer;

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer < fadeTime) {
            col = Color.Lerp(start, end, (timer / fadeTime));
            invCol = Color.Lerp(Color.white, Color.clear, (timer / fadeTime));
            panel.color = col;
            buttonImg.color = invCol;
            mult.color = invCol;
            score.color = invCol;
            //discoOne.color = invCol;
            //discoTwo.color = invCol;
        }
        else
        {
            holder.SetActive(true);
            this.enabled = false;
        }
    }
}
