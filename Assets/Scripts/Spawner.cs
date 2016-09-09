using UnityEngine;
using System.Collections;

/// <summary>
/// Should spawn the number in a lane.
/// </summary>

public class Spawner : MonoBehaviour {

    public void Spawn(Vector3 pos)
    {
        NumberGenerator._instance.GetNumber();
    }

}
