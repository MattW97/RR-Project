using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class ObiBloodScript : MonoBehaviour
{    
    private ObiSolver solver;
    private Obi.ObiSolver.ObiCollisionEventArgs collisionEvent;

    private float timer;
    private float resetTimer = 0.1f;  

    public string weaponType;

    public bool bloodTriggered;
    private bool setWeaponStats;

    void Awake()
    {
        solver = GetComponent<Obi.ObiSolver>();
    }

    void OnEnable()
    {
        solver.OnCollision += Solver_OnCollision;
    }

    void OnDisable()
    {
        solver.OnCollision -= Solver_OnCollision;
    }

    void Start ()
    {
        GetComponent<ObiEmitter>().speed = 0;
        GetComponent<ObiEmitter>().randomVelocity = 0;
        timer = resetTimer;
        setWeaponStats = true;
	}

    void Update()
    {
            if (bloodTriggered == true)
            {
                if (weaponType == "BaseballBat")
                {
                    if (setWeaponStats == true)
                    {
                        GetComponent<ObiEmitter>().speed = 5;
                        GetComponent<ObiEmitter>().randomVelocity = 0.3f;
                        timer = 0.1f;
                        setWeaponStats = false;
                    }
                }

                if (weaponType == "Machete")
                {
                    if (setWeaponStats == true)
                    {
                        GetComponent<ObiEmitter>().speed = 5;
                        GetComponent<ObiEmitter>().randomVelocity = 0.1f;
                        timer = 0.1f;
                        setWeaponStats = false;
                    }
                }

                if (weaponType == "Mallet")
                {
                    if (setWeaponStats == true)
                    {
                        GetComponent<ObiEmitter>().speed = 5;
                        GetComponent<ObiEmitter>().randomVelocity = 0.5f;
                        timer = 0.1f;
                        setWeaponStats = false;
                    }
                }

            if (weaponType == "GorePackage")
            {
                if (setWeaponStats == true)
                {
                    GetComponent<ObiEmitter>().speed = 10;
                    GetComponent<ObiEmitter>().randomVelocity = 0.5f;
                    timer = 0.2f;
                    setWeaponStats = false;
                }
            }

            timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    GetComponent<ObiEmitter>().speed = 0;
                    GetComponent<ObiEmitter>().randomVelocity = 0;
                    timer = resetTimer;
                    bloodTriggered = false;
                    setWeaponStats = true;
                }
            }      
    }

    void Solver_OnCollision(object sender, Obi.ObiSolver.ObiCollisionEventArgs e)
    {
        foreach (Oni.Contact contact in e.contacts)
        {
            // this one is an actual collision:
            if (contact.distance < 0.01)
            {
                Component collider;
                if (ObiCollider.idToCollider.TryGetValue(contact.other, out collider))
                {
                                       
                }
            }
        }
    }
}
