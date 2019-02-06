using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIPositioner : MonoBehaviour
{

    [Header("Render World Spawns")]
    public Transform p1CharSpawn;
    public Transform p2CharSpawn;
    public Transform p3CharSpawn;
    public Transform p4CharSpawn;

    public CameraScript _cameraScript;

    [Header("Result Animations")]
    public AnimationClip firstPlace;
    public AnimationClip secondPlace;
    public AnimationClip thirdPlace;
    public AnimationClip fourthPlace;
    public AnimationClip idleClip;

    private int p1Deaths;
    private int p2Deaths;
    private int p3Deaths;
    private int p4Deaths;

    private float lowestScore;
    private int index;

    private List<float> scores = new List<float>();

    private GameObject characterInfo;
    private ChosenChar _chosenCharScript;

    private void Awake()
    {
        characterInfo = GameObject.Find("PlayerPickerInfo");
        _chosenCharScript = characterInfo.GetComponent<ChosenChar>();
    }

    private void OnEnable()
    {
        //if (idleClip != null)
        //{
        //    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = idleClip;
        //    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = idleClip;
        //    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = idleClip;
        //    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = idleClip;
        //}
        SpawnRenders();
    }

    public void SpawnRenders()
    {
        if (_cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
        {
            p1Deaths = _cameraScript.player1.gameObject.GetComponent<PlayerController>().DeathCount;
            scores.Add(p1Deaths);
        }
        if (_cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
        {
            p2Deaths = _cameraScript.player2.gameObject.GetComponent<PlayerController>().DeathCount;
            scores.Add(p2Deaths);
        }
        if (_cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
        {
            p3Deaths = _cameraScript.player3.gameObject.GetComponent<PlayerController>().DeathCount;
            scores.Add(p3Deaths);
        }
        if (_cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
        {
            p4Deaths = _cameraScript.player4.gameObject.GetComponent<PlayerController>().DeathCount;
            scores.Add(p4Deaths);
        }

        if(scores.Count == 2)
        {
            #region 2 Player

            #region 1st Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

            scores.RemoveAt(index);
            #endregion

            #region 2nd Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            
            scores.RemoveAt(index);
            #endregion

            #endregion
        }
        else if (scores.Count == 3)
        {
            #region 3 Player

            #region 1st Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

            print(index + " " + lowestScore);
            scores.RemoveAt(index);
            #endregion

            #region 2nd Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            print(index + " " + lowestScore);
            scores.RemoveAt(index);
            #endregion

            #region 3rd Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            print(index + " " + lowestScore);
            scores.RemoveAt(index);
            #endregion

            #endregion
        }
        else if (scores.Count == 4)
        {
            #region 4 Player

            #region 1st Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

            print(index + " " + lowestScore);
            scores.RemoveAt(index);
            #endregion

            #region 2nd Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
            print(index + " " + lowestScore);
            scores.RemoveAt(index);
            #endregion

            #region 3rd Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
            print(index + " " + lowestScore);
            scores.RemoveAt(index);
            #endregion

            #region 4th Place
            lowestScore = scores.Min();
            index = scores.IndexOf(scores.Min());

            if (p1Deaths == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p2Deaths == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p3Deaths == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            else if (p4Deaths == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
            print(index + " " + lowestScore);
            scores.RemoveAt(index);
            #endregion

            #endregion
        }               

        foreach (Transform child in p1CharSpawn)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in p2CharSpawn)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in p3CharSpawn)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in p4CharSpawn)
        {
            Destroy(child.gameObject);
        }

        if (_chosenCharScript.p1ChosenCharacter)
            Instantiate(_chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab, p1CharSpawn.position, p1CharSpawn.rotation, p1CharSpawn);
        if (_chosenCharScript.p2ChosenCharacter)
            Instantiate(_chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab, p2CharSpawn.position, p2CharSpawn.rotation, p2CharSpawn);
        if (_chosenCharScript.p3ChosenCharacter)
            Instantiate(_chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab, p3CharSpawn.position, p3CharSpawn.rotation, p3CharSpawn);
        if (_chosenCharScript.p4ChosenCharacter)
            Instantiate(_chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab, p4CharSpawn.position, p4CharSpawn.rotation, p4CharSpawn);
    }
}
