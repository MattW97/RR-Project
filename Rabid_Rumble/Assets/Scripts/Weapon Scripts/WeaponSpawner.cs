using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private float initRespawnTime;
    private int weaponIndex;
    private float respawnTimer;
    private Transform thisTransform;
    private Vector3 spawnOffset;
    private Quaternion spawnTilt;

    public List<GameObject> weapons;
    public ParticleSystem confetti;

    void Start()
    {
        respawnTimer = initRespawnTime;
  
        thisTransform = GetComponent<Transform>();

        spawnOffset = this.transform.position + new Vector3(0, 0.5f, 0);
        spawnTilt = weapons[weaponIndex].transform.rotation * Quaternion.Euler(10, 0, 0);

        weaponIndex = Random.Range(0, weapons.Count);
        Quaternion weaponRotation = spawnTilt;
        Instantiate(weapons[weaponIndex], spawnOffset, weaponRotation, gameObject.transform);

        confetti.Clear();
        confetti.Play();
    }

    void Update()
    {
        if(thisTransform.childCount == 0)
        {
            RespawnWeapon();
        }
    }

    void RespawnWeapon()
    {
        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0)
        {
            weaponIndex = Random.Range(0, weapons.Count);
            Quaternion weaponRotation = weapons[weaponIndex].transform.rotation * Quaternion.Euler(10, 0, 0);
            Instantiate(weapons[weaponIndex], this.transform.position + new Vector3(0, 0.5f, 0), weaponRotation, gameObject.transform);

            respawnTimer = initRespawnTime;

            confetti.Clear();
            confetti.Play();

            GetComponent<Animator>().Play("Anim_WeaponSpawn");
        }
    }
}

