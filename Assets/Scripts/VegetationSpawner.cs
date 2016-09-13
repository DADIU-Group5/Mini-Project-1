using UnityEngine;
using System.Collections;

public class VegetationSpawner : MonoBehaviour {

    GameObject wheel;
    public GameObject[] bushes;
    public float spawnTime = 0.5f;
    private float numberSpeed = 1f;

	// Use this for initialization
	void Start () {
        wheel = GameObject.Find("Wheel");
        numberSpeed = GameState._instance.GetNumberSpeed();
        StartVegetation();
        Vegetation();
    }

    public void SpawnObject()
    {
        float playTime = GameState._instance.GetTimeSinceGameStarted();
        playTime = Time.time;
        //int spawnChance = (int)( 8 / (spawnTime/2));

        // TODO: Change 3 with variable.
        //float chance = playTime / (playTime + 0.001f);

        GameObject vegetation;

        if (Random.Range(0, 8) < 1 || 
            playTime > 30f && Random.Range(0, 8) <= 5 ||
            playTime > 60f && Random.Range(0, 10) <= 9)
        {
            vegetation = bushes[0]; //Tree
        } else 
        {
            vegetation = bushes[1]; //favours bushes[1] by request
        }

        if (playTime == 30 || playTime == 60)
            Debug.Log("playtime is: " + playTime);

        float xCoord = Random.Range(7, 16); // less than 16?
        if (Random.Range(0,2) == 1)
        {
            xCoord = -xCoord;
        }
        // spawn
        vegetation = (GameObject)Instantiate(vegetation, transform.position + new Vector3(xCoord, -21.74f, 93.37f) + (transform.right), Quaternion.Euler(24.3f,0,0 ));

        if (xCoord > 0)
        {
            Quaternion theRot = vegetation.transform.rotation;
            theRot.y = 180;
            vegetation.transform.rotation = theRot;
        }

        // make a child of the road
        vegetation.transform.SetParent(wheel.gameObject.transform);  
    }

    void Update()
    {
        if (GameState._instance.GetNumberSpeed() == 0f)
        {
            CancelInvoke();
        }
        else if (numberSpeed != GameState._instance.GetNumberSpeed())
        {
            CancelInvoke();
            numberSpeed = GameState._instance.GetNumberSpeed();
            Vegetation();
        }
    }

    public void StartVegetation()
    {/*
       // int x = (int)Mathf.Round(wheel.transform.position.y + 93.37-gameObject.transform.position.z * Mathf.Cos(360 )
        int numberOfPoints = 10;
        Vector3[] points = new Vector3[10];
        int i = 0;

        Vector3[] meshVertices = GameObject.Find("grund").gameObject.transform.GetComponent<MeshFilter>().mesh.vertices;
       
        int t = 0;
        while (i < numberOfPoints && t < 100)
        {
            t++;
            Vector3 test = meshVertices[Random.Range(0, meshVertices.Length)];

            if (test.y > 210)
            {
                Debug.Log("meshVertices[0]: " + test.x + ", " + test.y + ", " + test.z);
            }
            if (test.z < 85 && test.z > -10 && test.y < 216 && test.y > 210)
            {
                points[i] = test;
                i++;
            }
        }
        Debug.Log("i is: " + i);
        for (int k = 0; k < i; k++)
        {

            GameObject vegetation = bushes[0];
            vegetation = (GameObject)Instantiate(vegetation, transform.position + points[k] + (transform.right), Quaternion.Euler(24.3f, 0, 0));

            // make a child of the road
            vegetation.transform.SetParent(wheel.gameObject.transform);
        }
        
        //Vector3 test = wheel.GetComponent<Mesh>().vertices[0];
        */
    }

    public void Vegetation()
    {
        spawnTime = spawnTime / numberSpeed;

        //clamping spawntime between 0.1f and 5.0f
        spawnTime = Mathf.Clamp(spawnTime, 0.1f, 5.0f);

        InvokeRepeating("SpawnObject", 0f, spawnTime);
    }
}
