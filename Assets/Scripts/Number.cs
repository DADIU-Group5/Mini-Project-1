using UnityEngine;
using System.Collections;

/// <summary>
/// Used as the object for the number, so it knows which number it is, when it collides with the player.
/// </summary>

public class Number : MonoBehaviour {

    public Texture numberTexture;
    public int thisNumber = 20;
    public string Digits;
    float distance = -0.75f;

    //alternatively, change number from spawn script by public variable.

    public int ThisNumber
     {
         get { return thisNumber; }
         set { thisNumber = value;
            SetNumber();
        }
     }

    void Start()
    {
        SetNumber();
        Debug.Log(Digits.Length);
        // instantiate the correct number-prefabs
        for (int i = 0; i < Digits.Length; i++)
        {
            GameObject digit = (GameObject)Instantiate(Resources.Load("models/MikeSnakeV001"), transform.position + new Vector3(.25f, 0, 0) + (transform.right * distance * i),transform.rotation);
            //GameObject digit = (GameObject) Instantiate(Resources.Load("models/Number"+Digits[i]), transform.position + new Vector3(.25f, 0, 0) + (transform.right * distance * i),transform.rotation);

            //Assign the correct positions (how do I take object width into account here?)
            digit.transform.SetParent(gameObject.transform);
            //distance = digit.GetComponent<MeshFilter>().mesh.bounds.size.x;
        }
    } 

    // Use this for initialization
   public void SetNumber () {
        //ThisNumber = 20;
        Digits = thisNumber.ToString();
        //GetComponent<MeshRenderer>().material = (Material)Resources.Load("number" + Digits[0]);
        gameObject.transform.Find("NumberText").GetComponent<TextMesh>().text = Digits;
    }
}
