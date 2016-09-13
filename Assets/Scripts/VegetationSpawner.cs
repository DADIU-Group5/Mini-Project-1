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
        GameObject vegetation;
        if (Random.Range(0,4) < 1)
        {
           vegetation = bushes[0];
        } else
           vegetation = bushes[1]; //favours bushes[1] by request

        float xCoord = Random.Range(7, 16);
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
    {
       // int x = (int)Mathf.Round(wheel.transform.position.y + 93.37-gameObject.transform.position.z * Mathf.Cos(360 )
        int numberOfPoints = 10;
        Vector3[] points = new Vector3[10];
        int i = 0;

        Vector3[] meshVertices = GameObject.Find("grund").gameObject.transform.GetComponent<MeshFilter>().mesh.vertices;

        while (i < numberOfPoints)
        {
            Vector3 test = meshVertices[Random.Range(0, meshVertices.Length)];
            if (test.z < 85 && test.z > -10 && test.y < 216 && test.y > 210)
            {
                points[i] = test;
                i++;
            }
        }

        for (int k = 0; k < numberOfPoints; k++)
        {

            GameObject vegetation = bushes[0];
            vegetation = (GameObject)Instantiate(vegetation, transform.position + points[k] + (transform.right), Quaternion.Euler(24.3f, 0, 0));

            // make a child of the road
            vegetation.transform.SetParent(wheel.gameObject.transform);
        }
        
        //Vector3 test = wheel.GetComponent<Mesh>().vertices[0];

    }

    public void Vegetation()
    {
        spawnTime = spawnTime / numberSpeed;

        //clamping spawntime between 0.1f and 5.0f
        spawnTime = Mathf.Clamp(spawnTime, 0.1f, 5.0f);

        InvokeRepeating("SpawnObject", 0f, spawnTime);
    }
}
