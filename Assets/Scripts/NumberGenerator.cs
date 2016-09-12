using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generates a number for the spawner. So the spawner know which number to spawn.
/// </summary>

public class NumberGenerator : MonoBehaviour {

    public static NumberGenerator _instance;

    [Range(0, 1)]
    public float highestChance = 0.4f;
    [Range(0, 1)]
    public float lowestChance = 0.1f;
    public float lastTier = 90;
    [Range(0,1)]
    public float probability = 0.5f;
    public int lowerRange = 5;
    public int upperRange = 3;

    List<int> range = new List<int>();
    int multipleOfTen = 1;

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
        for (int i = -lowerRange; i <= upperRange; i++)
        {
            range.Add(i);
        }
        range.Remove(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(GetNumber());
        }
    }

    /// <summary>
    /// Used to get the next number to spawn.
    /// </summary>
    /// <returns>Number to spawn</returns>
    public int GetNumber()
    {
        if(GameState._instance.GetNextNumber() == 1)
        {
            return 1;
        }
        if(Random.Range(0f, 1f) <= GetProbability())
        {
            return GameState._instance.GetNextNumber();
        }
        else
        {
            //Makes sure it cannot generate numbers below 1.
            int minNum = GameState._instance.GetNextNumber() - lowerRange;
            if (minNum < 1)
            {
                //Needs to fix so it cannot spawn 0.
                int temp = range[Random.Range(-minNum, range.Count)] + GameState._instance.GetNextNumber();
                if(temp == 0)
                {
                    temp = 1;
                }
                return temp;
                //return range[Random.Range(-minNum, range.Count)] + GameState._instance.GetNextNumber();
            }
            else
            {
                return range[Random.Range(0, range.Count)] + GameState._instance.GetNextNumber();
            }
        }
    }

    public float GetProbability()
    {
        int multipleOfTen = GameState._instance.GetNextNumber();
        multipleOfTen -= multipleOfTen % 10;
        return highestChance - ((multipleOfTen / lastTier) * (highestChance - lowestChance));
    }
}