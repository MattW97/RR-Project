using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class KillZone : MonoBehaviour
{

    public enum ZoneType
    {
        ragdoll,
        killZone,
        both,
        removeCollisions,
        trapDamage
    }

    public ZoneType zoneSelection;
    public int damageFromTrap;
    public bool press;
    private int goreTimer;
    public GameObject goreSpawner;
    private bool goreSpawned;

    private void Start()
    {
        if (zoneSelection != ZoneType.trapDamage && GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }          
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (zoneSelection == ZoneType.ragdoll)
            {
                if (!other.gameObject.GetComponentInParent<PlayerController>().ragdolling)
                {
                    other.gameObject.GetComponentInParent<PlayerController>().Ragdoll(true);
                }
            }
            else if (zoneSelection == ZoneType.killZone)
            {
                other.gameObject.GetComponentInParent<PlayerController>().isDead = true;
                other.gameObject.SetActive(false);

                goreSpawner.GetComponent<TrapGoreSpawner>().SpawnGore();
            }
            else if (zoneSelection == ZoneType.both)
            {
                if (!other.gameObject.GetComponentInParent<PlayerController>().ragdolling)
                {
                    other.gameObject.GetComponentInParent<PlayerController>().Ragdoll(true);
                }

                other.gameObject.GetComponentInParent<PlayerController>().isDead = true;

                if (!press)
                {
                    goreSpawner.GetComponent<TrapGoreSpawner>().SpawnGore();
                }
                else
                {
                    Instantiate(goreSpawner, transform.position, transform.rotation);
                }

                if(other.tag == "Gore")
                {
                    Destroy(other.gameObject);
                }

            }
            else if (zoneSelection == ZoneType.removeCollisions)
            {
                foreach (Collider collider in other.gameObject.GetComponentsInChildren<Collider>())
                {
                    //collider.enabled = false;
                }
            }

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (zoneSelection == ZoneType.trapDamage)
            {
                if (!collision.gameObject.GetComponentInParent<PlayerController>().ragdolling)
                {
                    collision.gameObject.GetComponentInParent<PlayerHealthManager>().DamagePlayer(25);
                }
            }
        }
    }
}