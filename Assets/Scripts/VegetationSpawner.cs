using UnityEngine;
using System.Collections;

public class VegetationSpawner : MonoBehaviour {

    GameObject wheel;
    public GameObject[] bushes;
    public float spawnTime = 0.5f;
    
	// Use this for initialization
	void Start () {
        wheel = GameObject.Find("Wheel");
        /*int randnum = Random.Range(0, wheel.GetComponent<Mesh>().vertexCount);
        Vector3 randPos = wheel.GetComponent<Mesh>().vertices[randnum];
        randPos = transform.TransformPoint(randPos);
        Instantiate(bushes[0], randPos, wheel.GetComponent<Mesh>().normals[randnum]);*/
        Vegetation();
    }

    public void SpawnObject()
    {
        GameObject vegetation = bushes[Random.Range(0, bushes.Length)];

        float xCoord = Random.Range(7, 45);
        if (Random.Range(0,2) == 1)
        {
            xCoord = -xCoord;
        }
        // spawn
        vegetation = (GameObject)Instantiate(vegetation, transform.position + new Vector3(xCoord, -21.74f, 93.37f) + (transform.right), Quaternion.Euler(24.3f,0,0 ));

        // make as child
        vegetation.transform.SetParent(wheel.gameObject.transform);
    }

    public void Vegetation()
    {
        InvokeRepeating("SpawnObject", 0f, spawnTime);
    }
}
