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

    public GameObject numberPrefab;

    public void Spawn(Vector3 pos)
    {
        int newNumber = NumberGenerator._instance.GetNumber();
        int randomDistance = Random.Range(minDistance, (maxDistance + 1));
        Vector3 tempPosition = new Vector3(0, randomDistance, 0);
        Vector3 numberPos = pos + tempPosition;
        //instantiate + set number
        GameObject number = Instantiate(numberPrefab, numberPos, Quaternion.identity) as GameObject;
        number.GetComponent<Number>().ThisNumber = newNumber;
    }
}
