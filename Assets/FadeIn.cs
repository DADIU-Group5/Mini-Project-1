using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

    UnityEngine.UI.ColorBlock buttonCol;
    Color col;
    public UnityEngine.UI.Button button;
    public Color start;
    public Color end;
    public float fadeTime = 3;
    float timer;

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer < fadeTime) {
            col = Color.Lerp(start, end, (timer / fadeTime));
            buttonCol.normalColor = col;
            buttonCol.highlightedColor = col;
            buttonCol.pressedColor = col;
            button.colors = buttonCol;
        }
    }
}
