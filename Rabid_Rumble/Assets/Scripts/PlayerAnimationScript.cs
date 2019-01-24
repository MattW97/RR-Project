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

        // If unarmed
        if (!playerController.isHoldingWeapon)
        {
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(3, 0);
            animator.SetLayerWeight(4, 0);

            if (playerController.Player.GetButtonDown("Attack") && animator.GetCurrentAnimatorStateInfo(0).IsName("MasterMovementBlendTree"))
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

                if (playerController.Player.GetButtonDown("Attack") && animator.GetCurrentAnimatorStateInfo(1).IsName("ShotgunMovementBlendTree") &&
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

                Attack(1.0f, 2, "BaseballBatMovementBlendTree", swingClip, 2.0f);
            }

            if (weapon.weaponSelection == WeaponScript.WeaponType.Mallet)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 1);
                animator.SetLayerWeight(4, 0);

                Attack(1.5f, 3, "MalletMovementBlendTree", swingClip, 2.5f);
            }

            if (weapon.weaponSelection == WeaponScript.WeaponType.Machete)
            {               
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);
                animator.SetLayerWeight(4, 1);

                Attack(0.5f, 4, "MacheteMovementBlendTree", swingClip, 1.5f);
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

    void Attack(float heavyChargeTime, int animatorStateIndex, string animatorStateName, AudioClip soundFX, float heavyAttackMultiplier)
    {
        // Charge a heavy attack whilst holding the attack button down
        if (playerController.Player.GetButton("Attack"))
        {
            if (currentHeavyChargeTime < heavyChargeTime)
            {
                currentHeavyChargeTime += Time.deltaTime;
            }

            // Vibrate controller when heavy attack is fully charged
            if (currentHeavyChargeTime >= heavyChargeTime)
            {
                playerController.ControllerVibrate(1, 1.0f);
            }
        }

        // Attack if current heavy charge time is more than 0
        if (playerController.Player.GetButtonUp("Attack") && currentHeavyChargeTime > 0 && animator.GetCurrentAnimatorStateInfo(animatorStateIndex).IsName(animatorStateName))
        {
            // Stops vibration
            playerController.ControllerVibrate(0f, 0f);

            // Light Attack
            // If current charge is less than the required charge, do a light attack
            if (currentHeavyChargeTime < heavyChargeTime)
            {
                animator.ResetTrigger("LightAttackTrigger");
                animator.SetTrigger("LightAttackTrigger");

                // Play sound effect
                audioSource.PlayOneShot(soundFX, 1.0f);

                // Rounds damage multiplier to 2 decimal places
                damageMultiplier = Mathf.Round((currentHeavyChargeTime / heavyChargeTime) * 100) / 100;

                //Resets current heavy charge time to 0
                currentHeavyChargeTime = 0;

                // Applies a damage multiplier based on how long the attack button is held down for
                playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * ((damageMultiplier / 2) + 0.5f);
            }

            // Heavy Attack
            // If current charge is more than the required charge, do a heavy attack
            if (currentHeavyChargeTime > heavyChargeTime)
            {
                animator.ResetTrigger("HeavyAttackTrigger");
                animator.SetTrigger("HeavyAttackTrigger");

                // Play sound effect
                audioSource.PlayOneShot(soundFX, 1.0f);

                //Resets current heavy charge time to 0
                currentHeavyChargeTime = 0;

                // Applies a heavy attack multiplier for more than usual damage
                playerController.weapon.GetComponent<WeaponScript>().meleeDamage = playerController.weapon.GetComponent<WeaponScript>().maxMeleeDamage * heavyAttackMultiplier;
            }
        }
    }
}
