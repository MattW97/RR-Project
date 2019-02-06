using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashMenu : MonoBehaviour {

    public GameObject startMenu;

    private void Start()
    {
        startMenu.SetActive(false);
    }

    private void Update()
    {
        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("UISubmit"))
            {
                this.gameObject.SetActive(false);
                startMenu.SetActive(true);
            }
        }
    }
}
