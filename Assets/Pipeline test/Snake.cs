using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Snaking", true);
            print("Snaking");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Snaking", false);
        }
    }
}
