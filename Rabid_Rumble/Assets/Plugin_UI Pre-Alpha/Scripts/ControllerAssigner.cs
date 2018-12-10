using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/// <summary>
///  This script handles player assigning to avaliable positions within the game.
///  So player will get given which ever is the lowest avaliable player position.
/// </summary>
public class ControllerAssigner : MonoBehaviour
{

    public int maxPlayers = 4;
    private List<PlayerMap> playerMap; // Maps Rewired Player ids to game player ids
    private int gamePlayerIdCounter = 0;

    public int rumbleStrength;

    public PlayerPanel[] playerPanels;
    public List<int> existingConNums;

    public ChosenChar _chosenCharLink;

    void Awake()
    {
        playerMap = new List<PlayerMap>();
    }

    private void Start()
    {
        existingConNums.Clear();
        _chosenCharLink.existingConNums.Clear();
    }

    private void Update()
    {
        // Watch for JoinGame action in each Player
        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("JoinGame"))
            {
                AssignNextPlayer(i);
                existingConNums.Add(i);
                _chosenCharLink.existingConNums.Add(i);
            }
        }
    }

    void AssignNextPlayer(int rewiredPlayerId)
    {
        if (playerMap.Count >= maxPlayers)
        {
            Debug.LogError("Max player limit already reached!");
            return;
        }

        int gamePlayerId = GetNextGamePlayerId();

        // Add the Rewired Player as the next open game player slot
        playerMap.Add(new PlayerMap(rewiredPlayerId, gamePlayerId));

        Player rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);


        // Set vibration by motor type
        foreach (Joystick j in rewiredPlayer.controllers.Joysticks)
        {
            if (!j.supportsVibration) continue;
            j.SetVibration(rumbleStrength, rumbleStrength);
        }

        // Set vibration by motor index
        foreach (Joystick j in rewiredPlayer.controllers.Joysticks)
        {
            if (!j.supportsVibration) continue;
            if (j.vibrationMotorCount > 0) j.SetVibration(0, rumbleStrength);
            if (j.vibrationMotorCount > 1) j.SetVibration(1, rumbleStrength);
        }
        
        StartCoroutine(Vibration(rewiredPlayer));

        // Disable the Assignment map category in Player so no more JoinGame Actions return
        rewiredPlayer.controllers.maps.SetMapsEnabled(false, "Assignment");

        // Enable UI control for this Player now that he has joined
        rewiredPlayer.controllers.maps.SetMapsEnabled(true, "UI");

        //playerPanels[rewiredPlayerId].player.PlayerInGame = true;
        playerPanels[rewiredPlayerId].rewiredPlayerId = rewiredPlayerId;
        playerPanels[rewiredPlayerId].AssignController(rewiredPlayerId);
        playerPanels[rewiredPlayerId].rewiredPlayer = rewiredPlayer;
        playerPanels[rewiredPlayerId].playerController = rewiredPlayer.controllers.GetController<Controller>(rewiredPlayerId);

        //Debug.Log("Added Rewired Player id " + rewiredPlayerId + " to game player " + gamePlayerId);
    }

    IEnumerator Vibration(Player rewiredPlayer)
    {
        yield return new WaitForSeconds(.1f);
        // Stop vibration
        foreach (Joystick j in rewiredPlayer.controllers.Joysticks)
        {
            j.StopVibration();
        }
    }

    private int GetNextGamePlayerId()
    {
        return gamePlayerIdCounter++;
    }

    private class PlayerMap
    {
        public int rewiredPlayerId;
        public int gamePlayerId;

        public PlayerMap(int rewiredPlayerId, int gamePlayerId)
        {
            this.rewiredPlayerId = rewiredPlayerId;
            this.gamePlayerId = gamePlayerId;
        }
    }
}
