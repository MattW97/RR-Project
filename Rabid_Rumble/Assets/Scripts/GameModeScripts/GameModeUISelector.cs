using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeUISelector : MonoBehaviour {

    public string gameModeString;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void FreeForAll()
    {
        gameModeString = "FreeForAll";
        
    }

    public void KingOfTheHill()
    {
        gameModeString = "KingOfTheHill";
    }

    public void TeamDeathmatch()
    {
        gameModeString = "TeamDeathmatch";
    }

}
