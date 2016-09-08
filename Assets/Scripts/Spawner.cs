using UnityEngine;
using System.Collections;

/// <summary>
/// Should spawn the number in a lane.
/// </summary>

public class Spawner : MonoBehaviour {

    private NumberGenerator numberGenerator;

    public void Spawn(Vector3 pos) {
        numberGenerator.GetNumber();
    }
}
