using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

    public UnityEngine.UI.Image panel;
    public Color start;
    public Color end;
    public float fadeTime = 3;

    public GameObject holder;

    Color col;
    float timer;

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer < fadeTime) {
            col = Color.Lerp(start, end, (timer / fadeTime));
            panel.color = col;
        }
        else
        {
            holder.SetActive(true);
            this.enabled = false;
        }
    }
}
