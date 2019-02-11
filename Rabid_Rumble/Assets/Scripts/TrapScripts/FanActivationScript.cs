using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanActivationScript : MonoBehaviour {

    public GameObject fanObject;
    public GameObject fanConveyor;
    public FanPuller fanSucker;
    public GameObject killZone;
    private bool fanActive;
    public int fanSpeed;

    private void Start()
    {
        fanObject.GetComponent<Animator>().speed = 0.2f;
        fanConveyor.SetActive(false);
        killZone.SetActive(false);
    }

    private void Update()
    {
        if (fanActive)
        {
            Invoke("DeactivateFan", 2.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerController>().Player.GetButtonDown("InteractOption1"))
        {
            if (!fanActive)
            {
                fanObject.GetComponent<Animator>().speed = fanSpeed;
                fanConveyor.SetActive(true);
                killZone.SetActive(true);

                fanActive = true;
            }
        }
        if (other.tag == "Player" && other.GetComponent<PlayerController>().Player.GetButtonDown("InteractOption2"))
        {
            if (!fanActive)
            {
                fanObject.GetComponent<Animator>().speed = fanSpeed;
                fanSucker.runFanSuck = true;
                killZone.SetActive(true);

                fanActive = true;
            }
        }
    }

    private void DeactivateFan()
    {
        fanActive = false;
        fanObject.GetComponent<Animator>().speed = 0.2f;
        fanConveyor.SetActive(false);
        fanSucker.runFanSuck = false;
        killZone.SetActive(false);
    }
}
