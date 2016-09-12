using UnityEngine;
using System.Collections;

public class Despawner : MonoBehaviour {

    Vector3 startPos;

    void Start()
    {
        startPos = gameObject.transform.position;
    }

    //spawn rate based on size
    //lasttimespawned

    void Update()
    {
        if (gameObject.transform.position.z <= -13f)
        {
            Destroy(gameObject);
        }
    }
}
