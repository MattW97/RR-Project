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
            if (sawBladeSparks != null)
            {
                sawBladeSparks.SetActive(true);
            }
            GetComponent<Animator>().speed = animationSpeed;
        }
    }

    public void ActivateParticle()
    {
        if (sawBladeSparks != null)
        {
            sawBladeSparks.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    public void DeactivateParticle()
    {
        if (sawBladeSparks != null)
        {
            sawBladeSparks.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }
}
