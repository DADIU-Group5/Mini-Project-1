using UnityEngine;
using System.Collections;

/// <summary>
/// Should spawn the number in a lane.
/// </summary>

public class Spawner : MonoBehaviour {

    public Number numberPrefab;

    [Header("Throwing Animation")]
    [Range(-3, 15)]
    public float spawnZ = 11;

    [Range(0.1f, 5)]
    public float throwTime = 0.2f;

    [Range(0, 20)]
    public float throwHeight = 1;

    //[Range(0, 2)]
    //public int minDistance = 1;

    //[Range(2,10)]
    //public int maxDistance = 2;


    public void Spawn(Vector3 pos)
    {
        int newNumber = NumberGenerator._instance.GetNumber();
        //int randomDistance = Random.Range(minDistance, (maxDistance + 1));
        //Vector3 numberPos = pos;
        //numberPos.y = randomDistance;
        //numberPos.y = 1;
        //numberPos.z = spawnZ;

        //instantiate + set number
        Number number = Instantiate(numberPrefab);
        number.ThisNumber = newNumber;
        //number.throwEndTime = Time.timeSinceLevelLoad + throwTime;
        number.throwEndTime = GameState._instance.GetTimeSinceGameStarted() + throwTime;
        number.throwHeight = throwHeight;
        number.throwStartPos = new Vector3(pos.x, 2.5f, pos.z);
        number.throwEndPos = new Vector3(pos.x, 0.5f, spawnZ);
        number.transform.position = number.throwStartPos;
        number.transform.localScale = new Vector3(0, 0, 0);
    }
}