//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Rewired;

//public class PlayerSelector : MonoBehaviour {

//    public List<CharaterOption> gameCharacters;

//    public int gridColumns = 2;
//    public int gridRows = 6;
    
//    private bool p1AxisHeld;
//    private bool p2AxisHeld;


//    int CharacterMenuSelection(int selectedIndex, string horizontalAxis, string verticalAxis)
//    {
//        if (ReInput.players.GetPlayer(0).GetAxisRaw(horizontalAxis) > 0)
//        {
//            if (selectedIndex == UFE.config.characters.Length - 1)
//            {
//                selectedIndex = 0;
//            }
//            else
//            {
//                selectedIndex += 1;
//            }
//        }
//        else if (ReInput.players.GetPlayer(0).GetAxisRaw(horizontalAxis) < 0)
//        {
//            if (selectedIndex == 0)
//            {
//                selectedIndex = UFE.config.characters.Length - 1;
//            }
//            else
//            {
//                selectedIndex -= 1;
//            }
//        }

//        if (ReInput.players.GetPlayer(0).GetAxisRaw(verticalAxis) < 0)
//        {
//            if (selectedIndex <= 3)
//            {
//                selectedIndex += 4;
//            }
//        }
//        else if (ReInput.players.GetPlayer(0).GetAxisRaw(verticalAxis) > 0)
//        {
//            if (selectedIndex > 3)
//            {
//                selectedIndex -= 4;
//            }
//        }

//        return selectedIndex;
//    }
//    void Update()
//    {
//        if (Input.GetAxisRaw(p1HorizontalAxis) == 0 && Input.GetAxisRaw(p1VerticalAxis) == 0) p1AxisHeld = false;
//        if (Input.GetAxisRaw(p2HorizontalAxis) == 0 && Input.GetAxisRaw(p2VerticalAxis) == 0) p2AxisHeld = false;

//        // Select Character
//        if (!p1AxisHeld && UFE.config.player1Character == null)
//        {
//            p1HoverIndex = CharacterMenuSelection(p1HoverIndex, p1HorizontalAxis, p1VerticalAxis);

//            if (Input.GetButtonDown(UFE.GetInputReference(selectButton, UFE.config.player1_Inputs)))
//            {
//                if (UFE.config.soundfx) Camera.main.audio.PlayOneShot(selectSound);
//                UFE.config.player1Character = UFE.config.characters[p1HoverIndex];
//            }
//        }

//        if (!p2AxisHeld && UFE.config.player2Character == null)
//        {
//            p2HoverIndex = CharacterMenuSelection(p2HoverIndex, p2HorizontalAxis, p2VerticalAxis);

//            //Debug.Log(selectButton.ToString() +" = "+ Input.GetButtonDown(UFE.GetInputReference(selectButton, UFE.config.player2_Inputs)));
//            if (Input.GetButtonDown(UFE.GetInputReference(selectButton, UFE.config.player2_Inputs)))
//            {
//                if (UFE.config.soundfx) Camera.main.audio.PlayOneShot(selectSound);
//                UFE.config.player2Character = UFE.config.characters[p2HoverIndex];
//            }
//        }



//        // Both selected
//        if (UFE.config.player1Character != null && UFE.config.player2Character != null)
//        {
//            startingStageSelect = true;
//            Invoke("StartStageSelect", .8f);
//        }


//        // Deselect Character
//        if (!startingStageSelect && Input.GetButtonDown(UFE.GetInputReference(deselectButton, UFE.config.player1_Inputs)))
//            UFE.config.player1Character = null;

//        if (!startingStageSelect && Input.GetButtonDown(UFE.GetInputReference(deselectButton, UFE.config.player2_Inputs)))
//            UFE.config.player2Character = null;


//        if (Input.GetAxisRaw(p1HorizontalAxis) != 0 || Input.GetAxisRaw(p1VerticalAxis) != 0) p1AxisHeld = true;
//        if (Input.GetAxisRaw(p2HorizontalAxis) != 0 || Input.GetAxisRaw(p2VerticalAxis) != 0) p2AxisHeld = true;
//    }
//}
//}
