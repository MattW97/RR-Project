using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class ObiGoreScript : MonoBehaviour {

    private float timer;
    private float resetTimer = 0.2f;

    public bool bloodTriggered;

    void Start ()
    {
        GetComponent<ObiEmitter>().speed = 0;
        GetComponent<ObiEmitter>().randomVelocity = 0;
        timer = resetTimer;
    }

    void Update ()
    {
        if (bloodTriggered == true)
        {
            GetComponent<ObiEmitter>().speed = 10;
            GetComponent<ObiEmitter>().randomVelocity = 0.5f;
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                GetComponent<ObiEmitter>().speed = 0;
                GetComponent<ObiEmitter>().randomVelocity = 0f;
                timer = resetTimer;
                bloodTriggered = false;
            }
        }
    }

}