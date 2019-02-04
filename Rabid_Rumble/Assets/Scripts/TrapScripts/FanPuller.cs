using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPuller : MonoBehaviour
{
    public float thrust;
    public float upThrust;
    public bool runFanSuck;
    public GameObject ragZone;

    private void Start()
    {
        ragZone.SetActive(false);
    }

    private void Update()
    {
        if (runFanSuck)
        {
            ragZone.SetActive(true);
        }
        else
        {
            ragZone.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (runFanSuck)
        {
            other.gameObject.GetComponentInParent<Rigidbody>().AddForce(transform.forward * thrust);
            other.gameObject.GetComponentInParent<Rigidbody>().AddForce(transform.up * upThrust);

            ragZone.SetActive(true);
        }
        else
        {
            ragZone.SetActive(false);
        }
    }
}
