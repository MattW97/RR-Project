using UnityEngine;
using UnityEngine.UI;
using Rewired;

/// <summary>
/// This Script handles the player selection on the start screen
/// </summary>
public class PlayerPanel : MonoBehaviour {

    public bool hasControllerAssinged;
    public PlayerController player;
    public ControllerAssigner controlAssign;
    public GameObject joinMessage;
    public GameObject startGameMessage;
    public Color32 playerColour;

    public Player rewiredPlayer;
    public int rewiredPlayerId;
    public Controller playerController;

    private void Start()
    {
        joinMessage.SetActive(true);
        startGameMessage.SetActive(false);
        this.GetComponent<Image>().color = Color.white;
    }

    private void Update()
    {
        if (player.Player.GetButtonDown("UICancel") && hasControllerAssinged)
        {
            controlAssign.maxPlayers = controlAssign.maxPlayers + 1;
            rewiredPlayer.controllers.RemoveController(playerController);

            // Disable the Assignment map category in Player so no more JoinGame Actions return
            rewiredPlayer.controllers.maps.SetMapsEnabled(true, "Assignment");

            // Enable UI control for this Player now that he has joined
            rewiredPlayer.controllers.maps.SetMapsEnabled(false, "UI");

            controlAssign.existingConNums.Remove(rewiredPlayerId);

            joinMessage.SetActive(true);
            startGameMessage.SetActive(false);
            this.GetComponent<Image>().color = Color.white;
            hasControllerAssinged = false;
        }
    }

    public void AssignController(int rewiredPlayerId)
    {
        joinMessage.SetActive(false);
        startGameMessage.SetActive(true);
        this.GetComponent<Image>().color = playerColour;
        hasControllerAssinged = true;
    }
}
