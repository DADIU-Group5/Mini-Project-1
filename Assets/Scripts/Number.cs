using UnityEngine;
using System.Collections;

/// <summary>
/// Used as the object for the number, so it knows which number it is, when it collides with the player.
/// </summary>

public class Number : MonoBehaviour {

    public Texture numberTexture;
    public int thisNumber = 20;

    //alternatively, change number from spawn script by public variable.

    /* public int ThisNumber
     {
         get { return thisNumber; }
         set { thisNumber = value; }
     }*/

    // Use this for initialization
    void Start () {
        //ThisNumber = 20;
        string Digits = thisNumber.ToString();
        GetComponent<MeshRenderer>().material = (Material)Resources.Load("number" + Digits[0]);
        gameObject.transform.Find("NumberText").GetComponent<TextMesh>().text = Digits;

        //In the real prototype, we attach the correct model instead...

        //Tried to flip texture
        //GetComponent<MeshRenderer>().material.SetTextureScale("_MainTex", new Vector2(1, -1));
    }
}
