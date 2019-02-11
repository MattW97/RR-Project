using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorePackageScript : MonoBehaviour {

    private float despawnTimer = 15;
    private float timer;

	void Start () {
        timer = despawnTimer;
	}
	
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
