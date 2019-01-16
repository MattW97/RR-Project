using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGoreSpawner : MonoBehaviour {
    public GameObject gorePackage;
    private int isSpawned;

    public void SpawnGore()
    {
        if (isSpawned == 0)
        {
            Instantiate(gorePackage, transform.position, transform.rotation);
            isSpawned++;
            Invoke("ResetTimer", 3);
        }
    }

    public void ResetTimer()
    {
        isSpawned = 0;
    }
}
