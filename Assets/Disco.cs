using UnityEngine;
using System.Collections;

public class Disco : MonoBehaviour {

    public float maxDistFromStart = 150;
    public float averageTime = 2;
    public float timeDiff = 1;

    private Vector2 startPos;
    private Vector2 lerpTo;
    private Vector2 lerpFrom;
    private bool lerping;
    private float timer;
    private float totalTime;

    void Start()
    {
        startPos = transform.position;
        Debug.Log(startPos);
    }

	// Update is called once per frame
	void Update () {
         timer -= Time.deltaTime;
         transform.position = Vector2.Lerp(lerpFrom, lerpTo, (timer / totalTime));
         if(timer <= 0)
         {
             NewLerpPos();
            this.enabled = false;
         }
        //transform.position += new Vector3(Random.Range(-20, 20), Random.Range(-20, 20),0);
	}

    void NewLerpPos()
    {
        lerpFrom = transform.position;
        lerpTo = new Vector2(startPos.x + Random.Range(-maxDistFromStart, maxDistFromStart), startPos.y + Random.Range(-maxDistFromStart, maxDistFromStart));
        timer = averageTime + Random.Range(-timeDiff, timeDiff);
        totalTime = timer;
    }
}
