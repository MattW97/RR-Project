using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour {

    Animator animator;
    PlayerController playerController;

    [HideInInspector] public WeaponScript weapon;
    [HideInInspector] public float damageMultiplier;
    private float heavyChargeTime;
    private float currentHeavyChargeTime;

    private bool weaponCharged;
    

    private AudioSource audioSource;
    //public AudioClip shotgunFire;
    public AudioClip swingClip;
    //public AudioClip malletSwing;
    //public AudioClip macheteSwing;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        audioSource = GetComponentInParent<AudioSource>();

        currentHeavyChargeTime = 0;
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

        if (weaponCharged)
        {
            if (currentHeavyChargeTime > heavyChargeTime)
            {
                weapon.FlashOn();
            }
        }
        
        if (!playerController.isHoldingWeapon)
        {
            // If unarmed
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(3, 0);
            animator.SetLayerWeight(4, 0);
            animator.SetLayerWeight(5, 0);
            animator.SetLayerWeight(6, 0);

            if (playerController.Player.GetAxis("Attack") > 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("MasterMovementBlendTree"))
            {
                animator.ResetTrigger("LightAttackTrigger");
                animator.SetTrigger("LightAttackTrigger");
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
                animator.SetLayerWeight(5, 0);
                animator.SetLayerWeight(6, 0);

                if (playerController.Player.GetAxis("Attack") > 0 && animator.GetCurrentAnimatorStateInfo(1).IsName("ShotgunMovementBlendTree") &&
                    playerController.weapon.GetComponent<WeaponScript>().initAmmoAmount > 0)
                {
                    animator.ResetTrigger("LightAttackTrigger");
                    animator.SetTrigger("LightAttackTrigger");
                }
            }

            if (weapon.weaponSelection == WeaponScript.WeaponType.BaseballBat)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 1);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 0);
                animator.SetLayerWeight(5, 0);
                animator.SetLayerWeight(6, 0);

                heavyChargeTime = 1;

                if (playerController.Player.GetAxis("Attack") > 0)
                {
                    if(currentHeavyChargeTime < heavyChargeTime)
                    {
                        currentHeavyChargeTime += Time.deltaTime;
                    }
                }

                if (playerController.Player.GetAxis("Attack") == 0 && currentHeavyChargeTime > 0 && animator.GetCurrentAnimatorStateInfo(2).IsName("BaseballBatMovementBlendTree"))
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        // Light Attack
                        animator.ResetTrigger("LightAttackTrigger");
                        animator.SetTrigger("LightAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        // Applies a damage multiplier to weapon damage for light attacks

                        // Rounds damage multiplier to 2 decimal places
                        damageMultiplier = Mathf.Round((currentHeavyChargeTime / heavyChargeTime) * 100) / 100;

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * ((damageMultiplier / 2) + 0.5f);
                    }

                    if (currentHeavyChargeTime > heavyChargeTime)
                    {
                        // Heavy Attack
                        animator.ResetTrigger("HeavyAttackTrigger");
                        animator.SetTrigger("HeavyAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage;
                    }
                }
            }

            if (weapon.weaponSelection == WeaponScript.WeaponType.Mallet)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 1);
                animator.SetLayerWeight(4, 0);
                animator.SetLayerWeight(5, 0);
                animator.SetLayerWeight(6, 0);

                heavyChargeTime = 2;

                if (playerController.Player.GetAxis("Attack") > 0)
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        currentHeavyChargeTime += Time.deltaTime;
                    }
                }

                if (playerController.Player.GetAxis("Attack") == 0 && currentHeavyChargeTime > 0 && animator.GetCurrentAnimatorStateInfo(3).IsName("MalletMovementBlendTree"))
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        // Light Attack
                        animator.ResetTrigger("LightAttackTrigger");
                        animator.SetTrigger("LightAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);
                      
                        // Rounds damage multiplier to 2 decimal places
                        damageMultiplier = Mathf.Round((currentHeavyChargeTime / heavyChargeTime) * 100) / 100;

                        currentHeavyChargeTime = 0;

                        // Applies a damage multiplier to weapon damage for light attacks
                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * ((damageMultiplier / 2) + 0.5f);
                    }

                    if (currentHeavyChargeTime > heavyChargeTime)
                    {
                        // Heavy Attack
                        animator.ResetTrigger("HeavyAttackTrigger");
                        animator.SetTrigger("HeavyAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * 2;
                    }
                }
            }

            if (weapon.weaponSelection == WeaponScript.WeaponType.Machete)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 1);
                animator.SetLayerWeight(5, 0);
                animator.SetLayerWeight(6, 0);

                heavyChargeTime = 1;

                if (playerController.Player.GetAxis("Attack") > 0)
                {
                    if(currentHeavyChargeTime < heavyChargeTime)
                    {
                        currentHeavyChargeTime += Time.deltaTime;
                    }
                }

                if (playerController.Player.GetAxis("Attack") == 0 && currentHeavyChargeTime > 0 && animator.GetCurrentAnimatorStateInfo(4).IsName("MacheteMovementBlendTree"))
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        // Light Attack
                        animator.ResetTrigger("LightAttackTrigger");
                        animator.SetTrigger("LightAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        // Applies a damage multiplier to weapon damage for light attacks

                        // Rounds damage multiplier to 2 decimal places
                        damageMultiplier = Mathf.Round((currentHeavyChargeTime / heavyChargeTime) * 100) / 100;

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * ((damageMultiplier / 2) + 0.5f);
                    }

                    if (currentHeavyChargeTime > heavyChargeTime)
                    {
                        // Heavy Attack
                        animator.ResetTrigger("HeavyAttackTrigger");
                        animator.SetTrigger("HeavyAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage;
                    }
                }
            }

            if (weapon.weaponSelection == WeaponScript.WeaponType.Flyswatter)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 0);
                animator.SetLayerWeight(5, 1);
                animator.SetLayerWeight(6, 0);

                heavyChargeTime = 1;

                if (playerController.Player.GetAxis("Attack") > 0)
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        currentHeavyChargeTime += Time.deltaTime;
                    }
                }

                if (playerController.Player.GetAxis("Attack") == 0 && currentHeavyChargeTime > 0 && animator.GetCurrentAnimatorStateInfo(5).IsName("FlyswatterMovementBlendTree"))
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        // Light Attack
                        animator.ResetTrigger("LightAttackTrigger");
                        animator.SetTrigger("LightAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        // Applies a damage multiplier to weapon damage for light attacks

                        // Rounds damage multiplier to 2 decimal places
                        damageMultiplier = Mathf.Round((currentHeavyChargeTime / heavyChargeTime) * 100) / 100;

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * ((damageMultiplier / 2) + 0.5f);
                    }

                    if (currentHeavyChargeTime > heavyChargeTime)
                    {
                        // Heavy Attack
                        animator.ResetTrigger("HeavyAttackTrigger");
                        animator.SetTrigger("HeavyAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage;
                    }
                }
            }

            if (weapon.weaponSelection == WeaponScript.WeaponType.Chainsaw)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 0);
                animator.SetLayerWeight(5, 0);
                animator.SetLayerWeight(6, 1);

                heavyChargeTime = 1;

                if (playerController.Player.GetAxis("Attack") > 0)
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        currentHeavyChargeTime += Time.deltaTime;
                    }
                }

                if (playerController.Player.GetAxis("Attack") == 0 && currentHeavyChargeTime > 0 && animator.GetCurrentAnimatorStateInfo(6).IsName("ChainsawMovementBlendTree"))
                {
                    if (currentHeavyChargeTime < heavyChargeTime)
                    {
                        // Light Attack
                        animator.ResetTrigger("LightAttackTrigger");
                        animator.SetTrigger("LightAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        // Applies a damage multiplier to weapon damage for light attacks

                        // Rounds damage multiplier to 2 decimal places
                        damageMultiplier = Mathf.Round((currentHeavyChargeTime / heavyChargeTime) * 100) / 100;

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * ((damageMultiplier / 2) + 0.5f);
                    }

                    if (currentHeavyChargeTime > heavyChargeTime)
                    {
                        // Heavy Attack
                        animator.ResetTrigger("HeavyAttackTrigger");
                        animator.SetTrigger("HeavyAttackTrigger");

                        audioSource.PlayOneShot(swingClip, 1.0f);

                        currentHeavyChargeTime = 0;

                        playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage;
                    }
                }
            }
        }      
    }

    public void DamageOn()
    {
        if(playerController.isHoldingWeapon)
        {
            playerController.weapon.GetComponent<WeaponScript>().canDealDamage = true;
            playerController.weapon.GetComponent<MeleeWeaponTrail>().Emit = true;
        }
    }

    public void DamageOff()
    {
        if(playerController.weapon != null)
        {
            playerController.weapon.GetComponent<WeaponScript>().canDealDamage = false;
            playerController.weapon.GetComponent<MeleeWeaponTrail>().Emit = false;
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
