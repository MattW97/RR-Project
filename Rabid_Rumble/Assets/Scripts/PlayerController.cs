using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Obi;

public class PlayerController : MonoBehaviour
{

    #region floats
    [Header("Variables")]
    [HideInInspector]
    public float movementSpeed = 4;
    [HideInInspector]
    public float jumpForce = 3.0f;
    [HideInInspector]
    public float numToMash = 5;
    [HideInInspector]
    public float playerNum;   
    [HideInInspector]
    public float forwardBackward;
    [HideInInspector]
    public float rightLeft;
    public float knockbackTimer;
    public float knockbackTimerReset = 0.1f;
    public float groundDistance = 0.3f;

    private float distanceCheck;
    private float playerTurnSpeed = 20;
    private float numToMashMultiplier = 2.0f;
    private float joystickAxisValue;
    private float verticalVelocity;
    private float respawnTimer;
    private float respawnTimerReset = 3;

    public float currentSearchProgressBarTime;
    private float searchProgressBarTime = 5;
    #endregion

    #region ints
    public int kingScore = 0;
    [HideInInspector]
    public int totalCurrentMashes = 0;
    private int deathCount = 0;
    private int goreAmmount = 0;
    public int pushAmount;
    #endregion

    #region bools 
    public bool canControl;
    public bool canPickUpWeapon;
    public bool isHoldingWeapon;
    public bool beingDragged;
    public bool draggingPlayer;
    public bool pickUpMode;
    public bool inCountdown;

    public bool isDead;
    public bool ragdolling;
    public bool spawnGore;
    public bool isPushed;
    public bool isGrounded;

    private bool playerInGame;
    private bool inRange;
    private bool deathVibrate;
    private bool isLockedOn;

    [Space]
    #endregion

    #region Inputs
    [HideInInspector]
    public string leftStickHorizontal;
    [HideInInspector]
    public string leftStickVertical;
    [HideInInspector]
    public string rightStickHorizontal;
    [HideInInspector]
    public string rightStickVertical;
    [HideInInspector]
    public string fireButton;
    [HideInInspector]
    public string aimButton;
    [HideInInspector]
    public string pickUp;
    [HideInInspector]
    public string reload;
    [HideInInspector]
    public string mashButton;
    [HideInInspector]
    public string bButton;
    [HideInInspector]
    public string jumpButton;
    [HideInInspector]
    public string interactButton;
    #endregion

    #region GameObjects
    private GameObject closestPlayer;
    private GameObject pelvisGameObj;
    private GameObject characterInfo;
    private GameObject bButtonMash;
    private GameObject bButtonSearchProgress;

    public GameObject weapon;
    public GameObject gorePackage;
    public GameObject tagSetter;
    public GameObject playerIndicator;
    public GameObject targetPlayer;
    public GameObject targetIndicator;
    #endregion

    private Image bButtonMashFill;
    private Image bButtonPickUp;
    private Image bButtonSearch;
    private Image yButtonUI;

    #region Vector3s
    private Vector3 movementInput;

    [HideInInspector] public Vector3 movementVelocity;
    [HideInInspector] public Vector3 playerDirection;

    public Vector3 startingPosition;
    public Vector3 pushbackDirection;
    #endregion

    public string playerTag;

    private Transform thisTransform;
    private Rigidbody thisRigidbody;

    private Color player1Colour;
    private Color player2Colour;
    private Color player3Colour;
    private Color player4Colour;

    [HideInInspector] public Rigidbody rightHand;
    [HideInInspector] public PickUp pickUpScript;
    [HideInInspector] public PlayerHealthManager healthManager;

    #region Per-Instance Variables
    [Header("Change These For Each Player")]
    public GameObject playerSkeletalMesh;
    public GameObject skeletalMeshRef;
    public List<Transform> otherPlayersOrigin;
    public Transform thisPlayersOrigin;
    public Text deathCounterText;
    #endregion

    public int playerId = 0; // The Rewired player id of this character
    private Player player; // The Rewired Player

    void Awake()
    {
        characterInfo = GameObject.Find("PlayerPickerInfo");
        playerSkeletalMesh = characterInfo.GetComponent<ChosenChar>().SelectedCharacter(playerId);

        thisTransform = GetComponent<Transform>();

        if (playerSkeletalMesh == null)
        {
            canControl = false;
            PlayerInGame = false;
            gameObject.SetActive(false);
        }
        else
        {
            Instantiate(playerSkeletalMesh, thisTransform.position, thisTransform.rotation, thisTransform);
            canControl = true;
            PlayerInGame = true;
        }

        thisRigidbody = GetComponent<Rigidbody>();
        Player = ReInput.players.GetPlayer(playerId);

    }

    void Start()
    {
        // Set the game to active in the Game Manager
        deathCounterText.gameObject.SetActive(false);

        healthManager = GetComponent<PlayerHealthManager>();
        isDead = false;
        respawnTimer = respawnTimerReset;
        isHoldingWeapon = false;
        isLockedOn = false;
        startingPosition = transform.position + new Vector3(0, 0.01f, 0);
        RagdollSetup();
        playerTag = tagSetter.tag;
        knockbackTimer = knockbackTimerReset;
        currentSearchProgressBarTime = 0;

        player1Colour = new Color(1.0f, 0.1674208f, 0.1674208f);
        player2Colour = new Color(0.145098f, 0.4509804f, 0.8666667f);
        player3Colour = new Color(0.1641987f, 1.0f, 0.09558827f);
        player4Colour = new Color(0.7725491f, 0.8901961f, 0.03137255f);

        // Recursivly searches all children to find the right hand
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.gameObject.name == "RightPalm")
            {
                rightHand = child.transform.GetComponent<Rigidbody>();
                pickUpScript = child.transform.GetComponent<PickUp>();
            }

            if (child.gameObject.tag == "SkeletalMesh")
            {
                skeletalMeshRef = child.gameObject;
            }

            if (child.gameObject.name == "B-ButtonMash")
            {
                bButtonMash = child.gameObject;
                bButtonMashFill = bButtonMash.transform.Find("B-Colour").GetComponent<Image>();
            }

            if (child.gameObject.name == "B-ButtonPickUp")
            {
                bButtonPickUp = child.gameObject.GetComponent<Image>();
            }

            if (child.gameObject.name == "SearchProgressBar")
            {
                bButtonSearchProgress = child.gameObject;
                bButtonSearch = bButtonSearchProgress.transform.Find("B-ButtonSearch").GetComponent<Image>();

                bButtonSearchProgress.GetComponent<Image>().fillAmount = 0;
            }

            if (child.gameObject.name == "Y-Button")
            {
                yButtonUI = child.gameObject.GetComponent<Image>();
            }
        }

        bButtonMash.SetActive(false);
        bButtonPickUp.enabled = false;
        bButtonSearchProgress.SetActive(false);
        yButtonUI.enabled = false;
        playerIndicator.SetActive(true);

        // Set player number for UI
        #region Player numbers
        if (tagSetter.tag == "Player 1")
        {
            playerNum = 1;
        }
        if (tagSetter.tag == "Player 2")
        {
            playerNum = 2;
        }
        if (tagSetter.tag == "Player 3")
        {
            playerNum = 3;
        }
        if (tagSetter.tag == "Player 4")
        {
            playerNum = 4;
        }
        #endregion

    }

    void Update()
    {
        // Enable the end game death count and disable player control
        if (GameObject.Find("GameManager").GetComponent<UtilityManager>().countdownSeconds == 0 && GameObject.Find("GameManager").GetComponent<UtilityManager>().countdownMinutes == 0)
        {
            canControl = false;
            deathCounterText.gameObject.SetActive(true);
            thisRigidbody.velocity = new Vector3(0, 0, 0);
            forwardBackward = 0;
            rightLeft = 0;
        }

        tagSetter.transform.position = skeletalMeshRef.transform.Find("Pelvis").transform.position;

        if (!isDead)
        {
            if (canControl)
            {
                if (GameObject.Find("GameManager").GetComponent<UtilityManager>().activeGame != true)
                {
                    GameObject.Find("GameManager").GetComponent<UtilityManager>().activeGame = true;
                    GameObject.Find("GameManager").GetComponent<UtilityManager>().countdownTimerActive = true;
                }

                // Only get values if moving
                if (thisRigidbody.velocity.x > 0 || thisRigidbody.velocity.x < 0 || thisRigidbody.velocity.z > 0 || thisRigidbody.velocity.z < 0)
                {
                    GetCharDirections();
                }

                joystickAxisValue = Mathf.Clamp01(new Vector2(Player.GetAxis("LeftHoz"), Player.GetAxis("LeftVert")).sqrMagnitude);

                CharMovement();
                GrabAndDrag();

                #region Pick Up Weapon and Drop Weapon
                if (!isHoldingWeapon && canPickUpWeapon)
                {
                    //if (weapon.GetComponent<WeaponScript>().initialPickup == false)
                    //{
                    //    PickUpWeapon();
                    //    isHoldingWeapon = !isHoldingWeapon;
                    //}

                    if (player.GetButtonDown("Interact"))
                    {
                        PickUpWeapon();
                        isHoldingWeapon = !isHoldingWeapon;
                    }
                }
                else if (isHoldingWeapon)
                {
                    if (player.GetButtonDown("Interact"))
                    {
                        DropWeapon();
                        isHoldingWeapon = !isHoldingWeapon;
                        weapon.GetComponent<WeaponScript>().canDealDamage = false;
                    }

                    canPickUpWeapon = false;
                    weapon.GetComponent<WeaponScript>().Reload(gameObject);

                    bButtonPickUp.enabled = false;
                }
                #endregion

                //yButtonUI.SetActive(false);
                bButtonMash.SetActive(false);
                yButtonUI.enabled = false;
                playerIndicator.SetActive(true);
            }

            if (ragdolling)
            {
                ButtonMashing();
                //thisPlayersOrigin.position = rightHand.transform.position;
                transform.position = new Vector3(skeletalMeshRef.transform.Find("Pelvis").transform.position.x, skeletalMeshRef.transform.Find("Pelvis").transform.position.y, skeletalMeshRef.transform.Find("Pelvis").transform.position.z);
                //new Vector3(skeletalMeshRef.transform.Find("Pelvis").transform.position.x, 0, skeletalMeshRef.transform.Find("Pelvis").transform.position.z)

                bButtonMashFill.fillAmount = totalCurrentMashes / numToMash;
            }
        }

        if (isDead)
        {
            #region Respawning
            respawnTimer -= Time.deltaTime;
            playerIndicator.SetActive(false);

            if (!deathVibrate)
            {
                ControllerVibrate(0.5f, 1.0f);
                deathVibrate = true;
            }

            SkinnedMeshRenderer[] children = skeletalMeshRef.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer child in children)
            {
                child.enabled = false;
            }

            if (goreAmmount == 1)
            {
                Instantiate(gorePackage, thisPlayersOrigin.position, thisPlayersOrigin.rotation);

                goreAmmount++;

                DeathCount = DeathCount + 1;
            }
            else
            {
                goreAmmount++;
            }

            if (respawnTimer <= 0)
            {
                foreach (SkinnedMeshRenderer child in children)
                {
                    child.enabled = true;
                }

                thisTransform.position = startingPosition;
                isDead = false;
                canControl = true;
                ragdolling = false;
                Ragdoll(false);
                numToMash = 5;
                healthManager.currentHealth = healthManager.startingHealth;
                respawnTimer = respawnTimerReset;
                goreAmmount = 0;
                deathVibrate = false;
                skeletalMeshRef.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            #endregion
        }

        // Drop weapon if you get knocked out or die
        if (!canControl || isDead)
        {
            //thisTransform.position =  pelvisGameObj.transform.position;
            isHoldingWeapon = false;
            DropWeapon();
        }

        if(!canControl && ragdolling)
        {
            bButtonMash.SetActive(true);
        }

        if (!draggingPlayer)
        {
            DistanceToPlayer();
        }
        else
        {
            bButtonPickUp.enabled = false;
        }

        //deathCounterText.text = " DEATHS: " + DeathCount.ToString();
    }

    private void FixedUpdate()
    {
        if (isPushed)
        {
            if (knockbackTimer > 0)
            {
                Pushback(pushbackDirection, pushAmount);
                knockbackTimer -= Time.deltaTime;
            }
            if (knockbackTimer <= 0)
            {
                if (ragdolling)
                {
                    isPushed = false;
                    knockbackTimer = knockbackTimerReset;
                }
                else
                {
                    isPushed = false;
                    canControl = true;
                    knockbackTimer = knockbackTimerReset;
                }                
            }
        }

        if (!Physics.Raycast(thisPlayersOrigin.transform.position, -Vector3.up, groundDistance, LayerMask.GetMask("Ground")))
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }
    }

    void CharMovement()
    {
        #region Character Movement
        // Configure input for left analog stick
        movementInput = new Vector3(Player.GetAxis("LeftHoz") * movementSpeed, 0, Player.GetAxis("LeftVert") * movementSpeed);
        movementVelocity = movementInput;

        if (isGrounded)
        {
            thisRigidbody.velocity = new Vector3(movementVelocity.x, 0, movementVelocity.z);
        }
        else
        {
            thisRigidbody.velocity = new Vector3(movementVelocity.x, -6, movementVelocity.z);
        }

        // When Left Trigger is pressed...
        //if (Player.GetAxis("Aim") > 0)
        //{
        //    movementSpeed = 3;

        //    playerReticle.SetActive(true);

        //    // Configure input for right analog stick
        //    playerDirection = Vector3.right * Player.GetAxis("RightHoz") - Vector3.forward * Player.GetAxis("RightVert");

        //    if (playerDirection != Vector3.zero)
        //    {
        //        Quaternion targetRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        //        thisTransform.rotation = Quaternion.Lerp(thisTransform.rotation, targetRotation, playerTurnSpeed * Time.deltaTime);
        //    }
        //}
        //else
        //{
        //    playerReticle.SetActive(false);

        //    if (movementInput != Vector3.zero)
        //    {
        //        movementSpeed = 4;
        //        transform.rotation = Quaternion.LookRotation(movementInput);
        //    }
        //}

        if (Player.GetAxis("Aim") > 0)
        {
            isLockedOn = true;
        }
        else
        {
            isLockedOn = false;
        }

        if(isLockedOn)
        {
            Vector3 lookPos = new Vector3(targetPlayer.transform.position.x, thisTransform.position.y, targetPlayer.transform.position.z);
            thisTransform.LookAt(lookPos);

            targetIndicator.SetActive(true);

            if(targetPlayer.name == "Player Origin 1")
            {
                targetIndicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", player1Colour);
            }
            else if (targetPlayer.name == "Player Origin 2")
            {
                targetIndicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", player2Colour);
            }
            else if (targetPlayer.name == "Player Origin 3")
            {
                targetIndicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", player3Colour);
            }
            else if (targetPlayer.name == "Player Origin 4")
            {
                targetIndicator.GetComponent<Renderer>().material.SetColor("_EmissionColor", player4Colour);
            }
        }
        else
        {
            LockOnToPlayer();
            targetIndicator.SetActive(false);

            if (movementInput != Vector3.zero)
            {
                movementSpeed = 4;
                thisTransform.rotation = Quaternion.LookRotation(movementInput);
            }         
        }
        #endregion
    }

    void ButtonMashing()
    {
        #region Button Mashing 
        // Button mashing if the player is just ragdolling
        if (!beingDragged && player.GetButtonDown("Interact") || beingDragged && player.GetButtonDown("Interact"))
        {
            if (totalCurrentMashes >= (numToMash - 1))
            {
                //BreakDragging();
                healthManager.currentHealth = healthManager.startingHealth;
                Ragdoll(false);
            }
            else
            {
                totalCurrentMashes++;
                ControllerVibrate(0.1f, 1.0f);
            }
        }
        #endregion
    }

    //Used to calculate player directions for animation blend trees
    void GetCharDirections()
    {
        forwardBackward = Vector3.Dot(movementVelocity.normalized, thisTransform.forward.normalized) * joystickAxisValue;
        rightLeft = Vector3.Dot(-movementVelocity.normalized, Vector3.Cross(thisTransform.forward, thisTransform.up).normalized) * joystickAxisValue;
    }

    /// <summary>
    /// If the player is been dragged call this when the player can break from it.
    /// Works by breaking the joint that is connected to the player.
    /// And currently set ths health to full
    /// </summary>
    private void BreakDragging()
    {
        beingDragged = false;
        healthManager.currentHealth = healthManager.startingHealth;
        rightHand.gameObject.GetComponent<PickUp>().join = false;
        Destroy(rightHand.gameObject.GetComponent<SpringJoint>());
        totalCurrentMashes = 0;
    }

    /// <summary>
    /// This sets the ragdoll up and should be called during the start method
    /// </summary>
    private void RagdollSetup()
    {
        ragdolling = false;
        GetComponentInChildren<Animator>().enabled = true;

        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
        foreach (CharacterJoint cj in GetComponentsInChildren<CharacterJoint>())
        {
            cj.enableCollision = true;
            cj.enableProjection = true;
        }
        thisRigidbody.isKinematic = false;
    }

    /// <summary>
    /// Ragdoll Toggle is used for running and reseting the ragdoll
    /// </summary>
    /// <param name="ragdollToggle"></param>
    public void Ragdoll(bool ragdollToggle)
    {
        if (ragdollToggle) //Initiate Ragdoll
        {
            ragdolling = true;
            canControl = false;
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }
            thisRigidbody.isKinematic = true;
            GetComponentInChildren<Animator>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            skeletalMeshRef.transform.parent = null;

            //foreach (Transform bones in skeletalMeshRef.transform.Find("Pelvis").GetComponentsInChildren<Transform>())
            //{
            //    bones.gameObject.tag = "PlayerRagdoll";
            //}
        }

        else if (!ragdollToggle) //Reset Ragdoll
        {
            ragdolling = false;

            skeletalMeshRef.transform.parent = gameObject.transform;
            skeletalMeshRef.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            skeletalMeshRef.transform.Find("Pelvis").transform.position = new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y + 0.19f), gameObject.transform.position.z);
            //foreach (Transform bones in skeletalMeshRef.transform.Find("Pelvis").GetComponentsInChildren<Transform>())
            //{
            //    bones.gameObject.tag = "Untagged";
            //}

            totalCurrentMashes = 0;
            numToMash = numToMash * numToMashMultiplier;
            canControl = true;
            foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = true;
            }
            thisRigidbody.isKinematic = false;
            GetComponentInChildren<Animator>().enabled = true;
            GetComponent<CapsuleCollider>().enabled = true;
            skeletalMeshRef.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    /// <summary>
    /// Used to be constantly aware of how close the player is to each player
    /// </summary>
    public void DistanceToPlayer()
    {
        foreach (Transform otherPlayertrans in otherPlayersOrigin)
        {
            float dist = Vector3.Distance(otherPlayertrans.position, thisTransform.position);

            distanceCheck = 1;

            if (dist < distanceCheck && otherPlayertrans.root.GetComponent<PlayerController>().PlayerInGame)
            {
                inRange = true;
                ClosestPlayer = otherPlayertrans.gameObject;
                break;
            }
            else
            {
                inRange = false;
            }
        }
    }

    public void LockOnToPlayer()
    {
        foreach (Transform otherPlayertrans in otherPlayersOrigin)
        {
            float dist = Vector3.Distance(thisTransform.position, otherPlayertrans.position);

            if (targetPlayer == null)
            {
                targetPlayer = otherPlayertrans.gameObject;
            }

            if (dist < Vector3.Distance(thisTransform.position, targetPlayer.transform.position) && otherPlayertrans.root.GetComponent<PlayerController>().PlayerInGame)
            {
                targetPlayer = otherPlayertrans.gameObject;
            }
        }
    }

    void GrabAndDrag()
    {
        #region Grabbing and Dragging
        // Left Bumper picks up player when held
        if (player.GetButton("Interact") && !pickUpMode && inRange && !ragdolling)
        {
            DropWeapon();
            pickUpMode = true;
            draggingPlayer = true;
        }
        else if (!player.GetButton("Interact") && pickUpMode)
        {
            draggingPlayer = false;
            pickUpScript.join = false;
            Destroy(pickUpScript.GetComponent<SpringJoint>());
            pickUpMode = false;
        }

        // Current Setup for picking up player
        if (pickUpMode && inRange && !pickUpScript.join && ClosestPlayer.GetComponentInParent<PlayerController>().ragdolling)
        {
            pickUpScript.join = false;
            pickUpScript.CreateJoint();
        }
        #endregion
    }

    void PickUpWeapon()
    {
        WeaponScript weaponScript = weapon.GetComponent<WeaponScript>();
        weaponScript.GetPickedUp(rightHand);
        yButtonUI.enabled = false;
    }

    void DropWeapon()
    {
        if (weapon != null)
        {
            WeaponScript weaponScript = weapon.GetComponent<WeaponScript>();
            weaponScript.GetDropped();
        }
    }

    void Pushback(Vector3 direction, int pushAmount)
    {
        GetComponent<Rigidbody>().AddForce(direction * pushAmount, ForceMode.Impulse);
        //canControl = false;
    }

    public void ControllerVibrate(float vibrationTime, float intensity)
    {
        foreach (Joystick j in player.controllers.Joysticks)
        {
            if (!j.supportsVibration) continue;
            if (j.vibrationMotorCount > 0) j.SetVibration(0, intensity);
            StartCoroutine(Vibration(player, vibrationTime));
        }
    }

    IEnumerator Vibration(Player player, float vibrationTime)
    {
        yield return new WaitForSeconds(vibrationTime);
        foreach (Joystick j in player.controllers.Joysticks)
        {
            j.StopVibration();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!isHoldingWeapon && canControl)
        {
            if (col.gameObject.tag == "Weapon" && !ragdolling)
            {
                weapon = col.transform.parent.gameObject;
                canPickUpWeapon = true;
                bButtonPickUp.enabled = true;
            }

        }
        else
        {
            bButtonPickUp.enabled = false;
        }

        //if (col.gameObject.name == "BoxSearchZone" && !ragdolling)
        //{      
        //    bButtonSearchProgress.SetActive(true);

        //    if (Input.GetButton("Interact") && currentSearchProgressBarTime < searchProgressBarTime)
        //    {
        //        currentSearchProgressBarTime += Time.deltaTime;
        //        bButtonSearchProgress.GetComponent<Image>().fillAmount = currentSearchProgressBarTime / searchProgressBarTime;
        //    }
        //    else if (!Input.GetButton("Interact") && currentSearchProgressBarTime < searchProgressBarTime)
        //    {
        //        currentSearchProgressBarTime = 0;
        //        bButtonSearchProgress.GetComponent<Image>().fillAmount = 0;
        //    }
        //    else if (currentSearchProgressBarTime >= searchProgressBarTime)
        //    {
        //        currentSearchProgressBarTime = 0;
        //        bButtonSearchProgress.GetComponent<Image>().fillAmount = 0;
        //        bButtonSearchProgress.SetActive(false);
        //        col.gameObject.SetActive(false);

        //        // Spawn weapon
        //        col.gameObject.GetComponent<BoxSearchScript>().SpawnWeapon();
        //    }
        //}
    }

    void OnTriggerExit(Collider col)
    {
        if (!isHoldingWeapon)
        {
            this.weapon = null;
            canPickUpWeapon = false;
            bButtonPickUp.enabled = false;
        }

        if (col.gameObject.name == "BoxSearchZone")
        {
            bButtonSearchProgress.SetActive(false);
        }
    }

    #region Getters/ Setters
    public bool PlayerInGame { get { return playerInGame; } set { playerInGame = value; } }
    public GameObject ClosestPlayer { get { return closestPlayer; } set { closestPlayer = value; } }
    public Player Player { get { return player; } set { player = value; } }

    public int DeathCount
    {
        get
        {
            return deathCount;
        }

        set
        {
            deathCount = value;
        }
    }
    #endregion
}
