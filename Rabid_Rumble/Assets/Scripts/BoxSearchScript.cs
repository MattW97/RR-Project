using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSearchScript : MonoBehaviour {

    public GameObject boxWeapon;
    private Transform boxWeaponSpawnPoint;

    void Start()
    {
        boxWeaponSpawnPoint = gameObject.transform.Find("WeaponSpawnPoint");
    }

	public void SpawnWeapon()
    {
        Instantiate(boxWeapon, boxWeaponSpawnPoint.position, boxWeaponSpawnPoint.rotation);
    }
}
