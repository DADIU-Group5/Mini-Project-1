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
        Vegetation();
    }

    public void SpawnObject()
    {
        GameObject vegetation;
        if (Random.Range(0,4) < 1)
        {
           vegetation = bushes[0];
        } else
            vegetation = bushes[1];

        float xCoord = Random.Range(7, 16);
        if (Random.Range(0,2) == 1)
        {
            xCoord = -xCoord;
        }
        // spawn
        vegetation = (GameObject)Instantiate(vegetation, transform.position + new Vector3(xCoord, -21.74f, 93.37f) + (transform.right), Quaternion.Euler(24.3f,0,0 ));

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

    public void Vegetation()
    {
        spawnTime = spawnTime / numberSpeed;
        if (spawnTime < 0.5f)
        {
            spawnTime = 0.5f;
        }
        Debug.Log("spawntime: " + spawnTime);
        InvokeRepeating("SpawnObject", 0f, spawnTime);
    }
}
