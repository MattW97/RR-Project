using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using UnityEngine.SceneManagement;

/// <summary>
/// NEW
/// Handles the start menu UI nothing special just movin between menus,
/// Add access to options etc in here
/// </summary>

public class StartUIController : MonoBehaviour {

    public GameObject defaultMenu;
    public GameObject creditMenu;
    public GameObject optionsMenu;
    public GameObject charSelectionMenu;
    public GameObject overrideToggle;
    public GameObject defaultSelected;
    public GameObject closeOptions;
    
    private EventSystem es;

    [Header("Links")]
    public ControllerAssigner _controlAssign;
    public ChosenChar _chosenChar;

    // Use this for initialization
    void Start () {

        es = EventSystem.current;
        es.SetSelectedGameObject(defaultSelected);

        defaultMenu.SetActive(true);
        creditMenu.SetActive(false);
        optionsMenu.SetActive(false);
        charSelectionMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
