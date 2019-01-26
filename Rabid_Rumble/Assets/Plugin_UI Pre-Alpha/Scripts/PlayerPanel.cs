using UnityEngine;
using UnityEngine.UI;
using Rewired;
using System.Collections.Generic;

/// <summary>
/// This Script handles the player selection on the start screen
/// </summary>
public class PlayerPanel : MonoBehaviour
{
    public bool hasControllerAssinged;
    private bool leftSelection = false;
    private bool rightSelection = false;

    private bool test;
    private bool test2;
    private bool test3;
    private bool test4;

    private bool leftP1;
    private bool rightP1;
    private bool leftP2;
    private bool rightP2;
    private bool leftP3;
    private bool rightP3;
    private bool leftP4;
    private bool rightP4;

    private GameObject lastChar1;
    private GameObject lastChar2;
    private GameObject lastChar3;
    private GameObject lastChar4;

    //public PlayerController player;
    public ControllerAssigner controlAssign;
    public GameObject joinMessage;
    //public GameObject startGameMessage;
    //public Color32 playerColour;

    public Color32 arrowHighlight;
    public Image leftArrow;
    public Image rightArrow;
    public Text charNameText;

    public Player rewiredPlayer;
    public int rewiredPlayerId;
    public Controller playerController;

    public ChosenChar _chosenCharScript;
    private int charNum;

    private void Start()
    {
        joinMessage.SetActive(true);
        //startGameMessage.SetActive(false);
        //this.GetComponent<Image>().color = Color.white;
    }

    private void OnEnable()
    {
        foreach (Player player in ReInput.players.Players)
        {
            player.controllers.maps.SetMapsEnabled(true, "Assignment");
            player.controllers.maps.SetMapsEnabled(true, "UI");
        }
        joinMessage.SetActive(true);
    }

    private void Update()
    {
        if (rewiredPlayer != null)
        {
            if (rewiredPlayer.GetButtonDown("UICancel"))
            {
                if (hasControllerAssinged)
                {
                    controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                    rewiredPlayer.controllers.RemoveController(playerController);

                    rewiredPlayer.controllers.maps.SetMapsEnabled(true, "Assignment");

                    //rewiredPlayer.controllers.maps.SetMapsEnabled(false, "UI");

                    controlAssign.existingConNums.Remove(rewiredPlayerId);
                    _chosenCharScript.existingConNums.Remove(rewiredPlayerId);

                    if (rewiredPlayerId == 0)
                    {
                        if (_chosenCharScript.p1ChosenCharacter != null)
                        {
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p1ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p1CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }

                    }

                    if (rewiredPlayerId == 1)
                    {
                        if (_chosenCharScript.p2ChosenCharacter != null)
                        {
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p2ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p2CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }

                    }

                    if (rewiredPlayerId == 2)
                    {
                        if (_chosenCharScript.p3ChosenCharacter != null)
                        {
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p3ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p3CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }
                    }

                    if (rewiredPlayerId == 3)
                    {
                        if (_chosenCharScript.p4ChosenCharacter != null)
                        {
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p4ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p4CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }
                    }

                    joinMessage.SetActive(true);
                    hasControllerAssinged = false;
                }
                else if (!hasControllerAssinged)
                {

                    foreach (Player player in ReInput.players.Players)
                    {
                        player.controllers.maps.SetMapsEnabled(false, "Assignment");
                        player.controllers.maps.SetMapsEnabled(true, "UI");
                    }

                    if (_chosenCharScript.p1ChosenCharacter != null)
                    {
                        rewiredPlayer.controllers.RemoveController(rewiredPlayer.controllers.GetController<Controller>(1));
                        controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                        controlAssign.existingConNums.Remove(1);
                        _chosenCharScript.existingConNums.Remove(1);
                        _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p1ChosenCharacter);

                        foreach (Transform child in _chosenCharScript.p1CharSpawn)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    if (_chosenCharScript.p2ChosenCharacter != null)
                    {
                        rewiredPlayer.controllers.RemoveController(rewiredPlayer.controllers.GetController<Controller>(2));
                        controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                        controlAssign.existingConNums.Remove(2);
                        _chosenCharScript.existingConNums.Remove(2);
                        _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p2ChosenCharacter);

                        foreach (Transform child in _chosenCharScript.p2CharSpawn)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    if (_chosenCharScript.p3ChosenCharacter != null)
                    {
                        rewiredPlayer.controllers.RemoveController(rewiredPlayer.controllers.GetController<Controller>(3));
                        controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                        controlAssign.existingConNums.Remove(3);
                        _chosenCharScript.existingConNums.Remove(3);
                        _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p3ChosenCharacter);

                        foreach (Transform child in _chosenCharScript.p3CharSpawn)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    if (_chosenCharScript.p4ChosenCharacter != null)
                    {
                        rewiredPlayer.controllers.RemoveController(rewiredPlayer.controllers.GetController<Controller>(4));
                        controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                        controlAssign.existingConNums.Remove(4);
                        _chosenCharScript.existingConNums.Remove(4);
                        _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p4ChosenCharacter);

                        foreach (Transform child in _chosenCharScript.p4CharSpawn)
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    joinMessage.SetActive(true);
                    hasControllerAssinged = false;
                    controlAssign._startMenu.ClosePlayerSelection();
                    controlAssign._startMenu.closeCharSelection = false;
                }
            }

            if (rewiredPlayer.GetAxis("UILeft") < -0.5f && hasControllerAssinged)
            {
                leftArrow.color = arrowHighlight;
                rightArrow.color = Color.white;

                if (!leftSelection)
                    LeftArrowSelection();

            }

            if (rewiredPlayer.GetAxis("UIRight") > 0.5f && hasControllerAssinged)
            {
                rightArrow.color = arrowHighlight;
                leftArrow.color = Color.white;

                if (!rightSelection)
                    RightArrowSelection();
            }

            else if (rewiredPlayer.GetAxis("UIRight") == 0 && rewiredPlayer.GetAxis("UILeft") == 0)
            {
                leftArrow.color = Color.white;
                rightArrow.color = Color.white;

                leftSelection = false;
                rightSelection = false;
            }

            if (leftP1)
            {
                if (test)
                {
                    charNum--;
                    if (charNum == 0 || charNum <= 0)
                    {
                        charNum = _chosenCharScript.charOptions.Count;
                    }

                    _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p1ChosenCharacter))
                {
                    if (_chosenCharScript.p1ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar1);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p1ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p1CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p1CharSpawn.position, _chosenCharScript.p1CharSpawn.rotation, _chosenCharScript.p1CharSpawn);

                    leftP1 = false;
                }
                else
                {
                    test = true;
                }
            }

            if (leftP2)
            {
                if (test2)
                {
                    charNum--;
                    if (charNum == 0 || charNum <= 0)
                    {
                        charNum = _chosenCharScript.charOptions.Count;
                    }

                    _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test2 = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p2ChosenCharacter))
                {
                    if (_chosenCharScript.p2ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar2);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p2ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p2CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p2CharSpawn.position, _chosenCharScript.p2CharSpawn.rotation, _chosenCharScript.p2CharSpawn);

                    leftP2 = false;
                }
                else
                {
                    test2 = true;
                }
            }

            if (leftP3)
            {
                if (test3)
                {
                    charNum--;
                    if (charNum == 0 || charNum <= 0)
                    {
                        charNum = _chosenCharScript.charOptions.Count;
                    }

                    _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test3 = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p3ChosenCharacter))
                {
                    if (_chosenCharScript.p3ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar3);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p3ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p3CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p3CharSpawn.position, _chosenCharScript.p3CharSpawn.rotation, _chosenCharScript.p3CharSpawn);

                    leftP3 = false;
                }
                else
                {
                    test3 = true;
                }
            }

            if (leftP4)
            {
                if (test4)
                {
                    charNum--;
                    if (charNum == 0 || charNum <= 0)
                    {
                        charNum = _chosenCharScript.charOptions.Count;
                    }

                    _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test4 = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p4ChosenCharacter))
                {
                    if (_chosenCharScript.p4ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar4);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p4ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p4CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p4CharSpawn.position, _chosenCharScript.p4CharSpawn.rotation, _chosenCharScript.p4CharSpawn);

                    leftP4 = false;
                }
                else
                {
                    test4 = true;
                }
            }

            if (rightP1)
            {
                if (test)
                {
                    charNum++;

                    if (charNum == _chosenCharScript.charOptions.Count || charNum >= _chosenCharScript.charOptions.Count)
                    {
                        charNum = 0;
                    }

                    _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p1ChosenCharacter))
                {
                    if (_chosenCharScript.p1ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar1);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p1ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p1CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p1CharSpawn.position, _chosenCharScript.p1CharSpawn.rotation, _chosenCharScript.p1CharSpawn);

                    rightP1 = false;
                }
                else
                {
                    test = true;
                }
            }

            if (rightP2)
            {
                if (test2)
                {
                    charNum++;

                    if (charNum == _chosenCharScript.charOptions.Count || charNum >= _chosenCharScript.charOptions.Count)
                    {
                        charNum = 0;
                    }

                    _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test2 = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p2ChosenCharacter))
                {
                    if (_chosenCharScript.p2ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar2);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p2ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p2CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p2CharSpawn.position, _chosenCharScript.p2CharSpawn.rotation, _chosenCharScript.p2CharSpawn);

                    rightP2 = false;
                }
                else
                {
                    test2 = true;
                }
            }

            if (rightP3)
            {
                if (test3)
                {
                    charNum++;

                    if (charNum == _chosenCharScript.charOptions.Count || charNum >= _chosenCharScript.charOptions.Count)
                    {
                        charNum = 0;
                    }

                    _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test3 = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p3ChosenCharacter))
                {
                    if (_chosenCharScript.p3ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar3);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p3ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p3CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p3CharSpawn.position, _chosenCharScript.p3CharSpawn.rotation, _chosenCharScript.p3CharSpawn);

                    rightP3 = false;
                }
                else
                {
                    test3 = true;
                }
            }

            if (rightP4)
            {
                if (test4)
                {
                    charNum++;

                    if (charNum == _chosenCharScript.charOptions.Count || charNum >= _chosenCharScript.charOptions.Count)
                    {
                        charNum = 0;
                    }

                    _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

                    test4 = false;
                }

                if (!_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p4ChosenCharacter))
                {
                    if (_chosenCharScript.p4ChosenCharacter != null)
                        _chosenCharScript.currentSelectedChars.Remove(lastChar4);

                    _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p4ChosenCharacter);

                    foreach (Transform child in _chosenCharScript.p4CharSpawn)
                    {
                        Destroy(child.gameObject);
                    }

                    charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                    _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                    Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p4CharSpawn.position, _chosenCharScript.p4CharSpawn.rotation, _chosenCharScript.p4CharSpawn);

                    rightP4 = false;
                }
                else
                {
                    test4 = true;
                }
            }
        } 
        /// This Needs reworking potentially so that it doesnt auto back out from menu
        else if (rewiredPlayer == null)
        {
            for (int i = 0; i < ReInput.players.playerCount; i++)
            {
                if (ReInput.players.GetPlayer(i).GetButtonDown("UICancel"))
                {
                    if (!hasControllerAssinged)
                    {
                        foreach (Player player in ReInput.players.Players)
                        {
                            player.controllers.maps.SetMapsEnabled(false, "Assignment");
                            player.controllers.maps.SetMapsEnabled(true, "UI");

                            player.controllers.RemoveController(player.controllers.GetController<Controller>(1));
                            player.controllers.RemoveController(player.controllers.GetController<Controller>(2));
                            player.controllers.RemoveController(player.controllers.GetController<Controller>(3));
                            player.controllers.RemoveController(player.controllers.GetController<Controller>(4));
                        }

                        if (_chosenCharScript.p1ChosenCharacter != null)
                        {
                            // Error thrown cus it cant find Rewired need to find solution to this!!!
                            controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                            controlAssign.existingConNums.Remove(1);
                            _chosenCharScript.existingConNums.Remove(1);
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p1ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p1CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }

                        if (_chosenCharScript.p2ChosenCharacter != null)
                        {
                            controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                            controlAssign.existingConNums.Remove(2);
                            _chosenCharScript.existingConNums.Remove(2);
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p2ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p2CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }

                        if (_chosenCharScript.p3ChosenCharacter != null)
                        {
                            controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                            controlAssign.existingConNums.Remove(3);
                            _chosenCharScript.existingConNums.Remove(3);
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p3ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p3CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }

                        if (_chosenCharScript.p4ChosenCharacter != null)
                        {
                            controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
                            controlAssign.existingConNums.Remove(4);
                            _chosenCharScript.existingConNums.Remove(4);
                            _chosenCharScript.currentSelectedChars.Remove(_chosenCharScript.p4ChosenCharacter);

                            foreach (Transform child in _chosenCharScript.p4CharSpawn)
                            {
                                Destroy(child.gameObject);
                            }
                        }

                        joinMessage.SetActive(true);
                        hasControllerAssinged = false;
                        controlAssign._startMenu.ClosePlayerSelection();
                        controlAssign._startMenu.closeCharSelection = false;
                    }
                }
            }
        } 
    }

    private void LeftArrowSelection()
    {
        leftSelection = true;

        if (charNum == 0 || charNum <= 0)
        {
            charNum = _chosenCharScript.charOptions.Count;
        }

        if (rewiredPlayerId == 0)
            lastChar1 = _chosenCharScript.p1ChosenCharacter;
        if (rewiredPlayerId == 1)
            lastChar2 = _chosenCharScript.p2ChosenCharacter;
        if (rewiredPlayerId == 2)
            lastChar3 = _chosenCharScript.p3ChosenCharacter;
        if (rewiredPlayerId == 3)
            lastChar4 = _chosenCharScript.p4ChosenCharacter;

        charNum--;

        if (rewiredPlayerId == 0)
        {
            _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p1ChosenCharacter))
            {
                leftP1 = true;
            }
            else
            {
                if (_chosenCharScript.p1ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar1);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p1ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p1CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p1CharSpawn.position, _chosenCharScript.p1CharSpawn.rotation, _chosenCharScript.p1CharSpawn);
            }
        }

        if (rewiredPlayerId == 1)
        {
            _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p2ChosenCharacter))
            {
                leftP2 = true;
            }
            else
            {
                if (_chosenCharScript.p2ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar2);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p2ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p2CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p2CharSpawn.position, _chosenCharScript.p2CharSpawn.rotation, _chosenCharScript.p2CharSpawn);
            }
        }

        if (rewiredPlayerId == 2)
        {
            _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p3ChosenCharacter))
            {
                leftP3 = true;

            }
            else
            {
                if (_chosenCharScript.p3ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar3);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p3ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p3CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p3CharSpawn.position, _chosenCharScript.p3CharSpawn.rotation, _chosenCharScript.p3CharSpawn);
            }
        }

        if (rewiredPlayerId == 3)
        {
            _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p4ChosenCharacter))
            {
                leftP4 = true;
            }
            else
            {
                if (_chosenCharScript.p4ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar4);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p4ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p4CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p4CharSpawn.position, _chosenCharScript.p4CharSpawn.rotation, _chosenCharScript.p4CharSpawn);
            }
        }
    }

    private void RightArrowSelection()
    {
        rightSelection = true;

        if (rewiredPlayerId == 0)
            lastChar1 = _chosenCharScript.p1ChosenCharacter;
        if (rewiredPlayerId == 1)
            lastChar2 = _chosenCharScript.p2ChosenCharacter;
        if (rewiredPlayerId == 2)
            lastChar3 = _chosenCharScript.p3ChosenCharacter;
        if (rewiredPlayerId == 3)
            lastChar4 = _chosenCharScript.p4ChosenCharacter;

        charNum++;

        if (charNum == _chosenCharScript.charOptions.Count || charNum >= _chosenCharScript.charOptions.Count)
        {
            charNum = 0;
        }

        if (rewiredPlayerId == 0)
        {
            _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p1ChosenCharacter))
            {
                rightP1 = true;
            }
            else
            {
                if (_chosenCharScript.p1ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar1);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p1ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p1CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p1ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p1CharSpawn.position, _chosenCharScript.p1CharSpawn.rotation, _chosenCharScript.p1CharSpawn);
            }
        }

        if (rewiredPlayerId == 1)
        {
            _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p2ChosenCharacter))
            {
                rightP2 = true;
            }
            else
            {
                if (_chosenCharScript.p2ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar2);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p2ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p2CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p2ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p2CharSpawn.position, _chosenCharScript.p2CharSpawn.rotation, _chosenCharScript.p2CharSpawn);
            }
        }

        if (rewiredPlayerId == 2)
        {
            _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p3ChosenCharacter))
            {
                rightP3 = true;

            }
            else
            {
                if (_chosenCharScript.p3ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar3);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p3ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p3CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p3ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p3CharSpawn.position, _chosenCharScript.p3CharSpawn.rotation, _chosenCharScript.p3CharSpawn);
            }
        }

        if (rewiredPlayerId == 3)
        {
            _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;

            if (_chosenCharScript.currentSelectedChars.Contains(_chosenCharScript.p4ChosenCharacter))
            {
                rightP4 = true;
            }
            else
            {
                if (_chosenCharScript.p4ChosenCharacter != null)
                    _chosenCharScript.currentSelectedChars.Remove(lastChar4);

                _chosenCharScript.currentSelectedChars.Add(_chosenCharScript.p4ChosenCharacter);

                foreach (Transform child in _chosenCharScript.p4CharSpawn)
                {
                    Destroy(child.gameObject);
                }
                charNameText.text = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().charName;
                _chosenCharScript.p4ChosenCharacter = _chosenCharScript.charOptions[charNum].GetComponent<CharacterDetails>().characterPrefab;
                Instantiate(_chosenCharScript.charOptions[charNum], _chosenCharScript.p4CharSpawn.position, _chosenCharScript.p4CharSpawn.rotation, _chosenCharScript.p4CharSpawn);
            }
        }
    }

    public void AssignController(int rewiredPlayerId)
    {
        joinMessage.SetActive(false);
        RightArrowSelection();
        //startGameMessage.SetActive(true);
        //this.GetComponent<Image>().color = playerColour;
        hasControllerAssinged = true;
    }
}