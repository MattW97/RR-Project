using UnityEngine;
using Obi;

public class WeaponScript : MonoBehaviour
{

    public Vector3 position;
    public Vector3 rotation;

    public GameObject[] bulletSpawn;
    public GameObject bullet;
    public GameObject player;

    private Transform thisTransform;
    private Transform pickUpVolume;

    private float despawnTimer;
    private float initDespawnTime = 15;
    private float fireRate = 0.75f;
    private float shotTimer;
    private float reloadTime = 1;
    public float ammoAmount;
    public float impactAmount;

    [HideInInspector] public float initAmmoAmount = 4;
    [HideInInspector] public float maxMeleeDamage;
    [HideInInspector] public float meleeDamage;

    private bool canShoot;
    private bool reloading;
    //public bool initialPickup;
    public bool canDealDamage;

    public Rigidbody thisRigidbody;

    public ParticleSystem muzzleFlash;

    private AudioSource audioSource;
    public AudioClip[] randomShootAudio;
    public AudioClip[] randomBaseballBatImpactAudio;
    public AudioClip[] randomMacheteImpactAudio;
    public AudioClip[] randomMalletImpactAudio;

    private string playerTag;

    public int weaponPushAmount;

    public enum WeaponType
    {
        Shotgun,
        BaseballBat,
        Mallet,
        Machete
    }

    public WeaponType weaponSelection;

    void Start()
    {
        thisTransform = this.GetComponent<Transform>();
        thisRigidbody = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();
        pickUpVolume = thisTransform.Find("PickUpVolume");

        impactAmount = 5.0f;
        ammoAmount = initAmmoAmount;
        canShoot = false;
        reloading = false;
        canDealDamage = false;
        //initialPickup = false;

        despawnTimer = initDespawnTime;

        if (weaponSelection == WeaponType.BaseballBat)
        {
            maxMeleeDamage = 12;
            weaponPushAmount = 10;
        }
        if (weaponSelection == WeaponType.Mallet)
        {
            maxMeleeDamage = 18;
            weaponPushAmount = 20;
        }
        if (weaponSelection == WeaponType.Machete)
        {
            maxMeleeDamage = 10;
            weaponPushAmount = 5;
        }
    }

    void Update()
    {
        if (transform.parent == null)
        {
            despawnTimer -= Time.deltaTime;

            if (despawnTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void GetPickedUp(Rigidbody rightPalm)
    {
        player = rightPalm.transform.root.gameObject;

        playerTag = player.gameObject.GetComponent<PlayerController>().playerTag;

        thisRigidbody.isKinematic = true;

        gameObject.transform.parent = rightPalm.transform;
        gameObject.transform.localPosition = position;
        gameObject.transform.localEulerAngles = rotation;

        pickUpVolume.gameObject.SetActive(false);
        gameObject.GetComponent<MeshCollider>().isTrigger = true;

        canShoot = true;

        despawnTimer = initDespawnTime;

        gameObject.GetComponent<Outline>().enabled = false;

        //initialPickup = true;
    }

    public void GetDropped()
    {
        gameObject.transform.parent = null;

        pickUpVolume.gameObject.SetActive(true);
        gameObject.GetComponent<MeshCollider>().isTrigger = false;

        thisRigidbody.isKinematic = false;

        canShoot = false;

        gameObject.GetComponent<Outline>().enabled = true;
    }

    public void Shoot()
    {
        //PlayerController playerController = player.GetComponent<PlayerController>();

        // If right trigger is pressed...
        if (weaponSelection == WeaponType.Shotgun)
        {
            if (initAmmoAmount > 0)
            {
                for (int i = 0; i < bulletSpawn.Length; i++)
                {
                    Instantiate(bullet, bulletSpawn[i].transform.position, bulletSpawn[i].transform.rotation);
                }

                muzzleFlash.Play();

                int index = Random.Range(0, randomShootAudio.Length);
                audioSource.PlayOneShot(randomShootAudio[index], 1.0f);

                initAmmoAmount = initAmmoAmount - 1;
                player.gameObject.GetComponent<PlayerController>().ControllerVibrate(0.1f, 1.0f);
            }
            # region OldCode
            //if (playerController.Player.GetAxis("Attack") > 0 && !reloading && !playerController.pickUpMode)
            //{
            //    if (initAmmoAmount > 0)
            //    {

            //        shotTimer -= Time.deltaTime;

            //        if (shotTimer <= 0 && player.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(1).IsName("ShotgunMovementBlendTree"))
            //        {
            //            for (int i = 0; i < bulletSpawn.Length; i++)
            //            {

            //                Instantiate(bullet, bulletSpawn[i].transform.position, bulletSpawn[i].transform.rotation);
            //            }

            //            muzzleFlash.Play();
            //            shotTimer = fireRate;
            //            initAmmoAmount = initAmmoAmount - 1;
            //        }
            //    }
            //}
            //else
            //{
            //    // Stops delay between pressing the fire button and the shot firing
            //    // Makes it so shotTimer is only active whilst the fire button is down
            //    shotTimer = 0;
            //}
            #endregion
        }
    }

    public void Reload(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        // If X is pressed...
        if (playerController.Player.GetButtonDown("Reload") && !playerController.pickUpMode && initAmmoAmount < ammoAmount || initAmmoAmount == 0)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {

                initAmmoAmount = ammoAmount;
                shotTimer = 0;
                reloadTime = 1;
                reloading = false;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (player != null)
        {
            if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PlayerController>().playerTag != playerTag && canDealDamage)
            {
                collider.gameObject.GetComponent<PlayerController>().pushAmount = weaponPushAmount;

                collider.gameObject.GetComponent<PlayerHealthManager>().DamagePlayer(meleeDamage);

                Transform bloodParticleObject = collider.gameObject.transform.Find("BloodSplatterParticle");
                bloodParticleObject.rotation = Quaternion.LookRotation(thisTransform.forward);
                bloodParticleObject.GetComponent<ParticleSystem>().Play();

                Transform obiBloodObject = collider.gameObject.transform.Find("ObiBloodEmitter");
                obiBloodObject.rotation = Quaternion.LookRotation(thisTransform.forward);
                collider.gameObject.GetComponentInChildren<ObiBloodScript>().bloodTriggered = true;

                player.gameObject.GetComponent<PlayerController>().ControllerVibrate(0.1f, 1.0f);
                collider.gameObject.GetComponent<PlayerController>().ControllerVibrate(0.2f, 1.0f);

                Vector3 moveDirection = collider.transform.position - thisTransform.position;

                collider.GetComponent<PlayerController>().pushbackDirection = moveDirection;
                collider.GetComponent<PlayerController>().isPushed = true;

                // Impact sounds
                if (weaponSelection == WeaponType.BaseballBat)
                {
                    int index = Random.Range(0, randomBaseballBatImpactAudio.Length);
                    audioSource.PlayOneShot(randomBaseballBatImpactAudio[index], 1.0f);
                    collider.gameObject.GetComponentInChildren<ObiBloodScript>().weaponType = "BaseballBat";
                }
                if (weaponSelection == WeaponType.Mallet)
                {
                    int index = Random.Range(0, randomMalletImpactAudio.Length);
                    audioSource.PlayOneShot(randomMalletImpactAudio[index], 1.0f);
                    collider.gameObject.GetComponentInChildren<ObiBloodScript>().weaponType = "Mallet";
                }
                if (weaponSelection == WeaponType.Machete)
                {
                    int index = Random.Range(0, randomMacheteImpactAudio.Length);
                    audioSource.PlayOneShot(randomMacheteImpactAudio[index], 1.0f);
                    collider.gameObject.GetComponentInChildren<ObiBloodScript>().weaponType = "Machete";
                }
            }
        }
    }

    public void FlashOn()
    {
        gameObject.GetComponent<Outline>().enabled = true;
        Invoke("FlashOff", 0.2f);
    }

    public void FlashOff()
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }
}