﻿using UnityEngine;
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
        string Digits = thisNumber.ToString();
        GetComponent<MeshRenderer>().material = (Material)Resources.Load("number" + Digits[0]);

        //In the real prototype, we attach the correct model instead...

        //Tried to flip texture
        //GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(1, -1));
    }
}
