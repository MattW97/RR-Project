using Rewired;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapPopulator : MonoBehaviour
{
    public Text timerCountdown;
    public Text loadCountdown;
    public Text mapACount;
    public Text mapBCount;
    public Text mapCCount;
    public Text mapDCount;
    public MapUIPosition choosenMap;
    public Image screenBlur;

    public int timeLeft = 10;
    private int loadIn = 5;

    public List<MapDetails> allMaps;
    public List<MapUIPosition> mapPositions;
    [HideInInspector] public List<MapDetails> currentMapPicks;
    [HideInInspector] public List<MapUIPosition> sameVoteNum;
    [HideInInspector] public List<MapUIPosition> zeroVoteNum;

    private MapUIPosition highestMap;
    private bool getMaps = false;
    private bool voting = false;
    private bool load = false;
    private int mapA = 0;
    private int mapB = 0;
    private int mapC = 0;
    private int mapD = 0;
    private int lastVote;

    private void Start()
    {
        choosenMap.gameObject.SetActive(false);
        loadCountdown.gameObject.SetActive(false);
        screenBlur.gameObject.SetActive(false);
        //GetFourMaps();
    }

    private void OnEnable()
    {
        GetFourMaps();
    }

    private void Update()
    {
        if (getMaps)
        {
            int mapNumber = Random.Range(0, allMaps.Count);
            MapDetails map = allMaps[mapNumber];

            if (!currentMapPicks.Contains(map))
            {
                if (!mapPositions[0].mapAssigned)
                {
                    mapPositions[0].mapName.text = map.mapName;
                    mapPositions[0].mapImage.sprite = map.mapSprite;
                    mapPositions[0].sceneToLoad = map.associatedLevel;
                    mapPositions[0].mapAssigned = true;
                    currentMapPicks.Add(map);
                }
                else if (!mapPositions[1].mapAssigned)
                {
                    mapPositions[1].mapName.text = map.mapName;
                    mapPositions[1].mapImage.sprite = map.mapSprite;
                    mapPositions[1].sceneToLoad = map.associatedLevel;
                    mapPositions[1].mapAssigned = true;
                    currentMapPicks.Add(map);
                }
                else if (!mapPositions[2].mapAssigned)
                {
                    mapPositions[2].mapName.text = map.mapName;
                    mapPositions[2].mapImage.sprite = map.mapSprite;
                    mapPositions[2].sceneToLoad = map.associatedLevel;
                    mapPositions[2].mapAssigned = true;
                    currentMapPicks.Add(map);
                }
                else if (!mapPositions[3].mapAssigned)
                {
                    mapPositions[3].mapName.text = map.mapName;
                    mapPositions[3].mapImage.sprite = map.mapSprite;
                    mapPositions[3].sceneToLoad = map.associatedLevel;
                    mapPositions[3].mapAssigned = true;
                    currentMapPicks.Add(map);
                }

            }

        }

        if (voting)
        {
            timerCountdown.text = timeLeft.ToString();
            mapACount.text = mapA.ToString();
            mapBCount.text = mapB.ToString();
            mapCCount.text = mapC.ToString();
            mapDCount.text = mapD.ToString();

            if (timeLeft <= 0)
            {
                StopCoroutine(VoteTimer());

                foreach (Player player in ReInput.players.Players)
                {
                    player.controllers.maps.SetMapsEnabled(false, "UIVoting");
                }

                foreach (MapUIPosition mapUI in mapPositions)
                {
                    if (mapUI.voteNumber != 0)
                    {
                        if (mapUI.voteNumber > lastVote)
                        {
                            lastVote = mapUI.voteNumber;
                            highestMap = mapUI;
                        }
                        else if (mapUI.voteNumber == lastVote)
                        {
                            sameVoteNum.Add(mapUI);
                        }
                    }
                    else
                    {
                        zeroVoteNum.Add(mapUI);
                    }
                }

                if (zeroVoteNum.Count == 4)
                {
                    int mapPick = Random.Range(0, zeroVoteNum.Count);
                    highestMap = zeroVoteNum[mapPick];
                }
                else if (sameVoteNum.Count >= 1)
                {
                    sameVoteNum.Add(highestMap);
                    int mapPick = Random.Range(0, sameVoteNum.Count);
                    highestMap = sameVoteNum[mapPick];
                }

                choosenMap.gameObject.SetActive(true);
                choosenMap.GetComponent<Image>().color = highestMap.GetComponent<Image>().color;
                choosenMap.mapName.text = highestMap.GetComponent<MapUIPosition>().mapName.gameObject.GetComponent<Text>().text;
                choosenMap.mapImage.sprite = highestMap.GetComponent<Image>().sprite;
                choosenMap.sceneToLoad = highestMap.sceneToLoad;

                StartCoroutine(TimerToLevelLoad());
                loadCountdown.gameObject.SetActive(true);
                voting = false;
            }

            for (int j = 0; j < ReInput.players.playerCount; j++)
            {
                if (ReInput.players.GetPlayer(j).GetButtonUp("UIA"))
                {
                    ReInput.players.GetPlayer(j).controllers.maps.SetMapsEnabled(false, "UIVoting");
                    //A Map Vote
                    mapPositions[0].voteNumber++;
                    mapA++;
                }
                if (ReInput.players.GetPlayer(j).GetButtonUp("UIB"))
                {
                    ReInput.players.GetPlayer(j).controllers.maps.SetMapsEnabled(false, "UIVoting");
                    //B Map Vote
                    mapPositions[1].voteNumber++;
                    mapB++;
                }
                if (ReInput.players.GetPlayer(j).GetButtonUp("UIX"))
                {
                    ReInput.players.GetPlayer(j).controllers.maps.SetMapsEnabled(false, "UIVoting");
                    //X Map Vote
                    mapPositions[2].voteNumber++;
                    mapC++;
                }
                if (ReInput.players.GetPlayer(j).GetButtonUp("UIY"))
                {
                    ReInput.players.GetPlayer(j).controllers.maps.SetMapsEnabled(false, "UIVoting");
                    //Y Map Vote
                    mapPositions[3].voteNumber++;
                    mapD++;
                }
            }
        }

        if (loadIn <= 0)
        {
            StopCoroutine(TimerToLevelLoad());
            screenBlur.gameObject.SetActive(true);
            load = true;
        }
        else
        {
            loadCountdown.text = "IN... " + loadIn.ToString();
        }

        if (load)
        {
            SceneManager.LoadScene(choosenMap.sceneToLoad);
            load = false;
        }

    }

    public void GetFourMaps()
    {
        getMaps = true;
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1);
        getMaps = false;
        Voting();
    } // Temp

    private void Voting()
    {
        foreach (Player player in ReInput.players.Players)
        {
            player.controllers.maps.SetMapsEnabled(true, "UIVoting");
        }

        StartCoroutine(VoteTimer());
        voting = true;
    }

    IEnumerator VoteTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

    IEnumerator TimerToLevelLoad()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            loadIn--;
        }
    }
}
