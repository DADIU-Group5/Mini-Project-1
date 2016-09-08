using UnityEngine;
using System.Collections;

/// <summary>
/// Used as the object for the number, so it knows which number it is, when it collides with the player.
/// </summary>

public class Number : MonoBehaviour {

    public Texture numberTexture;
    public int thisNumber;

    public int ThisNumber//alternatively, change number from spawn script by public variable.
    {
        get { return thisNumber; }
        set { thisNumber = value; }
    }

	// Use this for initialization
	void Start () {
        //thisNumber = 50;
        string Digits = thisNumber.ToString();
        //GetComponent<MeshRenderer>().material = (Material)Resources.Load("number" + Digits[0]);
        TextMesh childText = transform.Find("FloatingText").GetComponent<TextMesh>();
        childText.text = Digits;
    }
}
