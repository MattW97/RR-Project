using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the start menu UI nothing special just movin between menus,
/// Add access to options etc in here
/// </summary>
public class StartMenu : MonoBehaviour
{

    [Header("Game Objects")]
    public GameObject startMenuGameObj;
    public GameObject creditMenuGameObj;
    public GameObject optionMenuGameObj;
    public GameObject playerSelectionGameObj;
    public GameObject mapSelectionGameObj;
    public GameObject startGameInfo;
    public GameObject parentObject;
    public GameObject overrideButton;
    public GameObject fightButton;
    public GameObject closeOptionsButton;

    public string currentMenu;

    public List<Sprite> countdownSprites;
    private int spritenumber;
    public GameObject countdownScreen;
    public Image countdownPosition;

    public Camera mainCamera;
    public Camera menuFlyCamera;
    public Camera countdownCamera;

    public bool closeCharSelection;
    private bool playerOverride;
    private bool countdown;
    private EventSystem es;

    [Header("Links")]
    public List<PlayerController> players;
    public ControllerAssigner controlAssign;
    public ChosenChar _chosenChar;
    private List<PlayerController> playersInGame;

    //private bool inStart;

    void Start()
    {
        currentMenu = "MainMenu";
        mainCamera.rect = new Rect(0, 0, 0, 0);

        playersInGame = new List<PlayerController>();

        es = EventSystem.current;
        es.SetSelectedGameObject(null);

        


        menuFlyCamera.gameObject.SetActive(true);
        countdownCamera.gameObject.SetActive(false);

        //inStart = true;

        startMenuGameObj.SetActive(true);
        startGameInfo.SetActive(false);
        playerSelectionGameObj.SetActive(false);
        optionMenuGameObj.SetActive(false);
        creditMenuGameObj.SetActive(false);
        mapSelectionGameObj.SetActive(false);
        countdownScreen.SetActive(false);

        foreach (PlayerController player in players)
        {
            if (isActiveAndEnabled)
            {
                player.gameObject.SetActive(false);
            }
        }


        es.SetSelectedGameObject(fightButton);
    }

    private void Update()
    {
        #region Check For Player joined On Start Menu

        if (controlAssign.existingConNums.Count >= 2)
        {
            //startGameInfo.SetActive(true);
        }
        else
        {
            startGameInfo.SetActive(false);
        }

        for (int i = 1; i <= 4; i++)
        {
            if (controlAssign.existingConNums.Count >= 2)
            {
                for (int j = 0; j < ReInput.players.playerCount; j++)
                {
                    if (ReInput.players.GetPlayer(j).GetButtonDown("Jump"))
                    {
                        playerSelectionGameObj.SetActive(false);
                        mapSelectionGameObj.SetActive(true);

                        //SceneManager.LoadScene("TESTLEVEL");


                        //menuFlyCamera.gameObject.SetActive(false);
                        //playerSelectionGameObj.SetActive(false);

                        //countdown = true;

                        //foreach (PlayerController player in players)
                        //{
                        //    player.inCountdown = true;

                        //    if (player.PlayerInGame)
                        //    {
                        //        player.canControl = false;

                        //        if (!playersInGame.Contains(player))
                        //            playersInGame.Add(player);

                        //        player.gameObject.SetActive(true);
                        //    }
                        //    else
                        //    {
                        //        player.PlayerInGame = false;
                        //    }
                        //}

                    }
                }
            }
        }


        //Begin Countdown Here
        if (countdown)
        {
            StartCoroutine(Countdown());
            countdown = false;
        }

        #endregion

        #region Back To Start

        //if (Input.GetButtonDown("B_1") || Input.GetButtonDown("B_2") || Input.GetButtonDown("B_3") || Input.GetButtonDown("B_4"))
        //{
        //    startMenuGameObj.SetActive(true);
        //    playerSelectionGameObj.SetActive(false);
        //    optionMenuGameObj.SetActive(false);
        //    creditMenuGameObj.SetActive(false);

        //    es.SetSelectedGameObject(null);
        //    es.SetSelectedGameObject(fightButton);
        //}

        #endregion

        #region PlayerOverride

        //if (controlAssign.existingConNums.Count >= 1)
        //{
        //    overrideButton.SetActive(true);
        //}
        //else
        //{
        //    overrideButton.SetActive(false);
        //}


        if (playerOverride)
        {
            playerSelectionGameObj.SetActive(false);
            HideAll();

            foreach (PlayerController player in players)
            {
                player.inCountdown = true;

                if (player.playerNum == 0)
                {
                    player.playerNum = 3;
                    player.PlayerInGame = true;

                    player.canControl = false;

                    if (!playersInGame.Contains(player))
                        playersInGame.Add(player);

                    player.gameObject.SetActive(true);
                }
                else if (player.playerNum != 0)
                {
                    player.PlayerInGame = true;

                    player.canControl = false;

                    if (!playersInGame.Contains(player))
                        playersInGame.Add(player);

                    player.gameObject.SetActive(true);
                }
            }
        }
        #endregion

        #region Current Menu Switch

        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("UICancel"))
            {
                switch (currentMenu)
                {
                    case "MainMenu":
                        break;
                    case "PlayerSelection":                        
                        break;
                    case "Options":
                        CloseOptions();
                        break;
                    case "Credits":
                        CloseCredits();
                        break;
                }
            }
        }      


        #endregion

    }

    private bool turnCountCamOff;

    IEnumerator Countdown()
    {
        menuFlyCamera.gameObject.SetActive(false);
        
        if (!turnCountCamOff)
        {
            countdownCamera.gameObject.SetActive(true);
        }

        countdownScreen.SetActive(true);

        countdownPosition.sprite = countdownSprites[spritenumber];

        yield return new WaitForSeconds(1);

        spritenumber++;

        if (spritenumber == 3)
        {
            turnCountCamOff = true;
            countdownCamera.gameObject.SetActive(false);
            mainCamera.rect = new Rect(0, 0, 1, 1);
        }

        if (spritenumber != countdownSprites.Count)
        {
            StartCoroutine(Countdown());
        }        
        else
        {
            foreach (PlayerController player in playersInGame)
            {
                player.canControl = true;
                player.inCountdown = false;
            }

            menuFlyCamera.gameObject.SetActive(false);
            parentObject.SetActive(false);
        }
    }

    public void TempRunOverride()
    {
        SceneManager.LoadScene("TESTLEVEL");
    }
    public void RunOverride()
    {
        playerOverride = true;
        menuFlyCamera.gameObject.SetActive(false);
        StartCoroutine(Countdown());
    }

    public void OpenPlayerSelection()
    {
        currentMenu = "PlayerSelection";
        startMenuGameObj.SetActive(false);
        playerSelectionGameObj.SetActive(true);
    }
    public void ClosePlayerSelection()
    {
        currentMenu = "MainMenu";
        startMenuGameObj.SetActive(true);
        playerSelectionGameObj.SetActive(false);

        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(fightButton);
    }

    public void Credits()
    {
        currentMenu = "Credits";
        startMenuGameObj.SetActive(false);
        creditMenuGameObj.SetActive(true);
    }
    public void CloseCredits()
    {
        currentMenu = "MainMenu";
        startMenuGameObj.SetActive(true);
        creditMenuGameObj.SetActive(false);
        
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(fightButton);
    }

    public void Options()
    {
        currentMenu = "Options";
        startMenuGameObj.SetActive(false);
        optionMenuGameObj.SetActive(true);


        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(closeOptionsButton);
    }
    public void CloseOptions()
    {
        currentMenu = "MainMenu";
        startMenuGameObj.SetActive(true);
        optionMenuGameObj.SetActive(false);

        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(fightButton);
    }

    private void HideAll()
    {
        playerSelectionGameObj.SetActive(false);
        menuFlyCamera.gameObject.SetActive(false);
        startMenuGameObj.SetActive(false);
        startGameInfo.SetActive(false);
        optionMenuGameObj.SetActive(false);
        creditMenuGameObj.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
