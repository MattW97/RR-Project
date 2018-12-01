﻿using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour {

    Animator animator;
    PlayerController playerController;

    [HideInInspector] public WeaponScript weapon;

    private AudioSource audioSource;
    //public AudioClip shotgunFire;
    public AudioClip baseballBatSwing;
    //public AudioClip malletSwing;
    //public AudioClip macheteSwing;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = playerController.rightLeft;
        float v = playerController.forwardBackward;

        animator.SetFloat("MoveRight", h);
        animator.SetFloat("MoveForward", v);

        if(playerController.weapon != null)
        {
            weapon = playerController.weapon.GetComponent<WeaponScript>();
        }
        
        if (!playerController.isHoldingWeapon)
        {
            // If unarmed
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(3, 0);
            animator.SetLayerWeight(4, 0);

            if (playerController.Player.GetAxis("Attack") > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("MasterMovementBlendTree"))
            {
                animator.ResetTrigger("AttackTrigger");
                animator.SetTrigger("AttackTrigger");
            }
        }
        else
        {
            if (weapon.weaponSelection == WeaponScript.WeaponType.Shotgun)
            {
                animator.SetLayerWeight(1, 1);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 0);

                if (playerController.Player.GetAxis("Attack") > 0 && animator.GetCurrentAnimatorStateInfo(1).IsName("ShotgunMovementBlendTree") &&
                    playerController.weapon.GetComponent<WeaponScript>().initAmmoAmount > 0)
                {
                    animator.ResetTrigger("AttackTrigger");
                    animator.SetTrigger("AttackTrigger");
                }
            }
            if (weapon.weaponSelection == WeaponScript.WeaponType.BaseballBat)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 1);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 0);

                if (playerController.Player.GetAxis("Attack") > 0 && animator.GetCurrentAnimatorStateInfo(2).IsName("BaseballBatMovementBlendTree"))
                {
                    animator.ResetTrigger("AttackTrigger");
                    animator.SetTrigger("AttackTrigger");

                    audioSource.PlayOneShot(baseballBatSwing, 0.5f);
                }

            }
            if (weapon.weaponSelection == WeaponScript.WeaponType.Mallet)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 1);
                animator.SetLayerWeight(4, 0);

                if (playerController.Player.GetAxis("Attack") > 0 && animator.GetCurrentAnimatorStateInfo(3).IsName("MalletMovementBlendTree"))
                {
                    animator.ResetTrigger("AttackTrigger");
                    animator.SetTrigger("AttackTrigger");

                    audioSource.PlayOneShot(baseballBatSwing, 0.5f);
                }
            }
            if (weapon.weaponSelection == WeaponScript.WeaponType.Machete)
            {               
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 1);

                if (playerController.Player.GetAxis("Attack") > 0 && animator.GetCurrentAnimatorStateInfo(4).IsName("MacheteMovementBlendTree"))
                {
                    animator.ResetTrigger("AttackTrigger");
                    animator.SetTrigger("AttackTrigger");

                    audioSource.PlayOneShot(baseballBatSwing, 0.5f);
                }
            }
        }      
    }

    public void DamageOn()
    {
        if(playerController.isHoldingWeapon)
        {
            playerController.weapon.GetComponent<WeaponScript>().canDealDamage = true;
        }
    }

    public void DamageOff()
    {
        if(playerController.weapon != null)
        {
            playerController.weapon.GetComponent<WeaponScript>().canDealDamage = false;
        }   
    }

    public void Shoot()
    {
        if (weapon.weaponSelection == WeaponScript.WeaponType.Shotgun)
        {
            playerController.weapon.GetComponent<WeaponScript>().Shoot();
        }
    }
}
