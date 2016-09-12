using UnityEngine;
using System.Collections;

public class RotatingWheel : MonoBehaviour {

    public static RotatingWheel _instance;

    float speed = 3.5f;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("There should not be 2 RotatingWheels, destroys the newly created RotatingWheels");
            Destroy(gameObject);
        }
    }

	void Update () {
        transform.Rotate(-Time.deltaTime * speed, 0, 0);
	}

    public void ChangeWheelSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void StopRotate()
    {
        speed = 0;
    }
}