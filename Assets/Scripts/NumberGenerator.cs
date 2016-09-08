using UnityEngine;
using System.Collections;

/// <summary>
/// Generates a number for the spawner. So the spawner know which number to spawn.
/// </summary>

public class NumberGenerator : MonoBehaviour {

    public static NumberGenerator _instance;

    // Use this for initialization
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("There should not be 2 NumberGenerators, destroys the newly created NumberGenerator");
            Destroy(gameObject);
        }
    }

    public int GetNumber()
    {
        //should contain logic to generate the correct number.
        return 0;
    }
}