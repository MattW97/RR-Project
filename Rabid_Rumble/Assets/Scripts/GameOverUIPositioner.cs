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

    private int p1Score;
    private int p2Score;
    private int p3Score;
    private int p4Score;

    public Text p1ScorePos;
    public Text p2ScorePos;
    public Text p3ScorePos;
    public Text p4ScorePos;

    private float lowestScore; //Used for Game Modes: FreeForAll (Add Here Additional Modes)
    private float highestScore; //Used for Game Modes: KingOfTheHill (Add Here Additional Modes)
    private int index;

    private List<float> scores = new List<float>();

    private GameObject characterInfo;
    private ChosenChar _chosenCharScript;
    private GameModeManager _GameModeManager;

    private void Awake()
    {
        _GameModeManager = GameObject.Find("GameManager").GetComponent<GameModeManager>();

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
        if (_GameModeManager.mode == GameModeManager.GameMode.FreeForAll)
        {
            if (_cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p1Score = _cameraScript.player1.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p1Score);
                p1ScorePos.text = "DEATHS: \n" + p1Score.ToString();
            }
            if (_cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p2Score = _cameraScript.player2.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p2Score);
                p2ScorePos.text = "DEATHS: \n" + p2Score.ToString();
            }
            if (_cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p3Score = _cameraScript.player3.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p3Score);
                p3ScorePos.text = "DEATHS: \n" + p3Score.ToString();
            }
            if (_cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p4Score = _cameraScript.player4.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p4Score);
                p4ScorePos.text = "DEATHS: \n" + p4Score.ToString();
            }

            if (scores.Count == 2)
            {
                #region 2 Player

                #region 1st Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
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

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 3rd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
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

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 3rd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 4th Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
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

        if (_GameModeManager.mode == GameModeManager.GameMode.KingOfTheHill)
        {
            if (_cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p1Score = _cameraScript.player1.gameObject.GetComponent<PlayerController>().kingScore;
                scores.Add(p1Score);
                p1ScorePos.text = "ZONE SCORE: \n" + p1Score.ToString();
            }
            if (_cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p2Score = _cameraScript.player2.gameObject.GetComponent<PlayerController>().kingScore;
                scores.Add(p2Score);
                p2ScorePos.text = "ZONE SCORE: \n" + p2Score.ToString();
            }
            if (_cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p3Score = _cameraScript.player3.gameObject.GetComponent<PlayerController>().kingScore;
                scores.Add(p3Score);
                p3ScorePos.text = "ZONE SCORE: \n" + p3Score.ToString();
            }
            if (_cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p4Score = _cameraScript.player4.gameObject.GetComponent<PlayerController>().kingScore;
                scores.Add(p4Score);
                p4ScorePos.text = "ZONE SCORE: \n" + p4Score.ToString();
            }

            if (scores.Count == 2)
            {
                #region 2 Player

                #region 1st Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;

                scores.RemoveAt(index);
                #endregion

                #endregion
            }
            else if (scores.Count == 3)
            {
                #region 3 Player

                #region 1st Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                print(index + " " + highestScore);
                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                print(index + " " + highestScore);
                scores.RemoveAt(index);
                #endregion

                #region 3rd Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                print(index + " " + highestScore);
                scores.RemoveAt(index);
                #endregion

                #endregion
            }
            else if (scores.Count == 4)
            {
                #region 4 Player

                #region 1st Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                print(index + " " + highestScore);
                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                print(index + " " + highestScore);
                scores.RemoveAt(index);
                #endregion

                #region 3rd Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                print(index + " " + highestScore);
                scores.RemoveAt(index);
                #endregion

                #region 4th Place
                highestScore = scores.Max();
                index = scores.IndexOf(scores.Max());

                if (p1Score == highestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == highestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == highestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == highestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                print(index + " " + highestScore);
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

        if (_GameModeManager.mode == GameModeManager.GameMode.TeamDeathmatch)
        {
            if (_cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p1Score = _cameraScript.player1.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p1Score);
                p1ScorePos.text = "DEATHS: \n" + p1Score.ToString();
            }
            if (_cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p2Score = _cameraScript.player2.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p2Score);
                p2ScorePos.text = "DEATHS: \n" + p2Score.ToString();
            }
            if (_cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p3Score = _cameraScript.player3.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p3Score);
                p3ScorePos.text = "DEATHS: \n" + p3Score.ToString();
            }
            if (_cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
            {
                p4Score = _cameraScript.player4.gameObject.GetComponent<PlayerController>().DeathCount;
                scores.Add(p4Score);
                p4ScorePos.text = "DEATHS: \n" + p4Score.ToString();
            }

            if (scores.Count == 2)
            {
                #region 2 Player

                #region 1st Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
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

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 3rd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
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

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = firstPlace;

                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 2nd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = secondPlace;
                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 3rd Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p4ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = thirdPlace;
                print(index + " " + lowestScore);
                scores.RemoveAt(index);
                #endregion

                #region 4th Place
                lowestScore = scores.Min();
                index = scores.IndexOf(scores.Min());

                if (p1Score == lowestScore && _cameraScript.player1.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p1ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p2Score == lowestScore && _cameraScript.player2.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p2ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p3Score == lowestScore && _cameraScript.player3.gameObject.GetComponent<PlayerController>().PlayerInGame)
                    _chosenCharScript.p3ChosenCharacter.GetComponent<CharacterDetails>().characterPrefab.GetComponent<Animation>().clip = fourthPlace;
                else if (p4Score == lowestScore && _cameraScript.player4.gameObject.GetComponent<PlayerController>().PlayerInGame)
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
}
