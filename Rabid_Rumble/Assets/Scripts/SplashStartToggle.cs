using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashStartToggle : MonoBehaviour {

    [Header("Game Objects")]
    public GameObject splashMenu;
    public GameObject startMenu;
    public GameObject creditMenuGameObj;
    public GameObject optionMenuGameObj;
    public GameObject playerSelectionGameObj;
    public GameObject mapSelectionGameObj;

    void Start()
    {

        splashMenu.SetActive(true);
        startMenu.SetActive(false);
        playerSelectionGameObj.SetActive(false);
        optionMenuGameObj.SetActive(false);
        creditMenuGameObj.SetActive(false);
        mapSelectionGameObj.SetActive(false);
    }
}
