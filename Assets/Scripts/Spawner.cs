using UnityEngine;
using System.Collections;

/// <summary>
/// Should spawn the number in a lane.
/// </summary>

public class Spawner : MonoBehaviour {

    [Range(0, 2)]
    public int minDistance = 0;

    [Range(2,10)]
    public int maxDistance = 2;

    private float spawnDistance = 0.0f;

    public void Spawn(Vector3 pos)
    {
        int newNumber = NumberGenerator._instance.GetNumber();
        int randomDistance = Random.Range(minDistance, (maxDistance + 1));
        Vector3 tempPosition = new Vector3(0, randomDistance, 0);
        pos += tempPosition;
        //instantiate + set number
    }
}
