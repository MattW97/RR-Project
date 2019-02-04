using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTimeActiveScript : MonoBehaviour {
    public GameObject gameTimeRef;
    public GameObject sawBladeSparks;
    private float gameTime;
    public float triggerTrapTime;
    public float animationSpeed;

    private void Start()
    {
        GetComponent<Animator>().speed = 0;
    }

    private void Update()
    {
        gameTime = gameTimeRef.GetComponent<UtilityManager>().debuggedTimer;

        if (gameTime <= triggerTrapTime)
        {
            sawBladeSparks.SetActive(true);
            GetComponent<Animator>().speed = animationSpeed;
        }
    }

    public void ActivateParticle()
    {
        sawBladeSparks.GetComponentInChildren<ParticleSystem>().Play();
        Debug.Log("Activated");
    }

    public void DeactivateParticle()
    {
        sawBladeSparks.GetComponentInChildren<ParticleSystem>().Stop();
        Debug.Log("Deactivated");
    }
}
