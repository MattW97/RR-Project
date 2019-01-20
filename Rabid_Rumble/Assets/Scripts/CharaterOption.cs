using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;

public class CharaterOption : MonoBehaviour {

    public GameObject characterPrefab;
    public GameObject menuRenderPrefab;
    public bool selected;
    public bool confirmed;
    public ChosenChar _chosenCharScript;

    private int conNum;

    // For indicating the player is unavaliable
    //public Image locked;


    public void ReturnPlayer()
    {
        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("UISubmit"))
            {
                conNum = i;
            }
        }

        if (conNum == 0)
        {
            foreach (Transform child in _chosenCharScript.p1CharSpawn)
            {
                Destroy(child.gameObject);
            }

            selected = true;
            _chosenCharScript.p1ChosenCharacter = characterPrefab;
            Instantiate(menuRenderPrefab, _chosenCharScript.p1CharSpawn.position, _chosenCharScript.p1CharSpawn.rotation, _chosenCharScript.p1CharSpawn);

        }
        else if (conNum == 1)
        {
            foreach (Transform child in _chosenCharScript.p2CharSpawn)
            {
                Destroy(child.gameObject);
            }

            selected = true;
            _chosenCharScript.p2ChosenCharacter = characterPrefab;
            Instantiate(menuRenderPrefab, _chosenCharScript.p2CharSpawn.position, _chosenCharScript.p2CharSpawn.rotation, _chosenCharScript.p2CharSpawn);

        }
        else if (conNum == 2)
        {
            foreach (Transform child in _chosenCharScript.p3CharSpawn)
            {
                Destroy(child.gameObject);
            }

            selected = true;
            _chosenCharScript.p3ChosenCharacter = characterPrefab;
            Instantiate(menuRenderPrefab, _chosenCharScript.p3CharSpawn.position, _chosenCharScript.p3CharSpawn.rotation, _chosenCharScript.p3CharSpawn);

        }
        else if (conNum == 3)
        {
            foreach (Transform child in _chosenCharScript.p4CharSpawn)
            {
                Destroy(child.gameObject);
            }

            selected = true;
            _chosenCharScript.p4ChosenCharacter = characterPrefab;
            Instantiate(menuRenderPrefab, _chosenCharScript.p4CharSpawn.position, _chosenCharScript.p4CharSpawn.rotation, _chosenCharScript.p4CharSpawn);

        }


    }
}
