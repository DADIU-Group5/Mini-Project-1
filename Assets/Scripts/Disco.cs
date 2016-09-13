using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Disco : MonoBehaviour {

    public Color[] colors;
    public Image imageCol;
    Color startCol;
    int nextCol;
    float timer;
    public float changeTime = 2;
    public float timeDiff = 1;
    float actTime;
    bool stopped = false;

	// Use this for initialization
	void Start () {
        stopped = true;
        startCol = imageCol.color;
	}
	
	// Update is called once per frame
	void Update () {
        if (stopped)
        {
            return;
        }
        timer += Time.deltaTime*GameState._instance.GetMultiplier();
        if(timer >= actTime)
        {
            ChooseNewColor();
        }
        else
        {
            imageCol.color = Color.Lerp(startCol, colors[nextCol], timer / changeTime);
        }
	}

    void ChooseNewColor()
    {
        startCol = imageCol.color;
        nextCol = Random.Range(0, colors.Length);
        actTime = changeTime + Random.Range(-timeDiff, timeDiff);
        timer = 0;
    }

    public void Stop()
    {
        stopped = true;
        GetComponent<Animator>().enabled = false;
    }

    public void Resume()
    {
        stopped = false;
        GetComponent<Animator>().enabled = true;
    }
}
