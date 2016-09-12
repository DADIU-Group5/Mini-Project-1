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

    // Throw animation
    private float spawnTime;
    public float throwEndTime, throwHeight;
    public Vector3 throwStartPos, throwEndPos;

    //alternatively, change number from spawn script by public variable.

    public int ThisNumber
     {
         get { return thisNumber; }
         set { thisNumber = value;
            SetNumber();
        }
     }

    private void Awake()
    {
        spawnTime = Time.timeSinceLevelLoad;
        Rigidbody body = GetComponent<Rigidbody>();
        body.isKinematic = true;
    }

    void Start()
    {
        SetNumber();
        // instantiate the correct number-prefabs
        for (int i = 0; i < Digits.Length; i++)
        {
            float addValue = 0;
            if (Digits.Length > 1 && i == 0)
            {
                addValue = distance;
            }

            GameObject digit = (GameObject)Instantiate(Resources.Load("Textures/numbers_" + Digits[i]), transform.position + new Vector3(.25f, 0, 0) +/* (-transform.right * distance/2 * i) +*/ (transform.right * addValue), transform.rotation);
            //GameObject digit = (GameObject) Instantiate(Resources.Load("models/Number"+Digits[i]), transform.position + new Vector3(.25f, 0, 0) + (transform.right * distance * i),transform.rotation);

            digit.transform.SetParent(gameObject.transform);

            //Assign the correct positions (how do I take object width into account here?)
            //RectTransform rt = (RectTransform)digit.transform;
            //distance = rt.rect.width;//digit.GetComponent<MeshFilter>().mesh.bounds.size.x;

            //number1.transform.localScale = new Vector3(2, 2, 2); //considering as animation...
        }
    } 

    // Use this for initialization
    public void SetNumber ()
    {
        Digits = thisNumber.ToString();
    }

    public void Update ()
    {
        // Check if we are doing the throwing animation.
        if (Time.timeSinceLevelLoad < throwEndTime)
        {
            float fraction = (throwEndTime - Time.timeSinceLevelLoad) / (throwEndTime - spawnTime);

            float newX = Mathf.Lerp(throwStartPos.x, throwEndPos.x, 1 - fraction);

            float newZ = Mathf.Lerp(throwStartPos.z, throwEndPos.z, 1 - fraction);

            float newY = (fraction - fraction * fraction) * 4 * throwHeight + throwEndPos.y;

            transform.position = new Vector3(newX, newY, newZ);

            transform.localScale = new Vector3(1 - fraction, 1 - fraction, 1 - fraction);
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
