using UnityEngine;
using System.Collections;

/// <summary>
/// Should spawn the number in a lane.
/// </summary>

public class Spawner : MonoBehaviour {

    public GameObject numberPrefab;

    private NumberGenerator numberGenerator;

    void Start()
    {
        Debug.Log("Spawning");
        Spawn(gameObject.transform.position);
    }

    public void Spawn(Vector3 pos)
    {
        numberGenerator.GetNumber();
        Debug.Log("instantiating");
        GameObject number = (GameObject)Instantiate(numberPrefab) as GameObject;
    }

}
