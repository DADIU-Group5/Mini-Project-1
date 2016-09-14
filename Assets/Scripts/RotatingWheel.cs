using UnityEngine;
using System.Collections;

public class RotatingWheel : MonoBehaviour {

    public static RotatingWheel _instance;

    public float radius = 217.5f; // Approximate radius of the wheel.
    public float speed = 4.5f; // 3.5f seems to fit a speed of 12, 4.5 seems to fit a speed of 17.
    public float slowdownTime = 1.6f; // Time it takes to stop.

    private float slowStartTime = 0f; // Time when stop was initiated.
    private bool stop = false;

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
        if (stop)
        {
            float factor = (slowStartTime + slowdownTime - Time.timeSinceLevelLoad) / slowdownTime;
            transform.Rotate(-Time.deltaTime * Mathf.Lerp(0f, speed, factor), 0, 0);
        }
        else
        {
            transform.Rotate(-Time.deltaTime * speed, 0, 0);
        }
    }

    // The wheel matches its rotational speed to a given surface-velocity
    public void ChangeWheelSpeed(float newSpeed)
    {
        //speed = newSpeed;

        // Circumference
        float c = 2 * Mathf.PI * radius;

        speed = newSpeed / (c / 360);
    }

    public void StopRotate()
    {
        stop = true;
        slowStartTime = Time.timeSinceLevelLoad;
        //speed = 0;
    }
}