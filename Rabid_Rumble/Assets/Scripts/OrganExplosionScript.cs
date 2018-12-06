using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganExplosionScript : MonoBehaviour
{
    private Rigidbody rigidBody;

    private float force;
    private float despawnTimer;
    private float minDespawnTime = 5;
    private float maxDespawnTime = 10;

    void Start()
    {
        force = 50;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddExplosionForce(force, transform.position + Random.insideUnitSphere * 5, 20, 20);

        despawnTimer = Random.Range(minDespawnTime, maxDespawnTime);
    }

    void Update()
    {
        despawnTimer -= Time.deltaTime;

        if(despawnTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void DestroyGorePackage()
    {
        Destroy(gameObject);
    }
}