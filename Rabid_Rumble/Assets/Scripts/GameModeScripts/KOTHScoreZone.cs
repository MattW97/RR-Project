using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KOTHScoreZone : MonoBehaviour
{
    private GameModeManager _GameModeManager;
    private int addScoreIn;
    private int playersInZone = 0;

    public float countdownSeconds = 30;
    public float countdownMinutes = 1;
    public float debuggedTimer = 120;

    public TextMesh countdownTimerText1;
    public TextMesh countdownTimerText2;
    public TextMesh countdownTimerText3;
    public TextMesh countdownTimerText4;

    public List<TextMesh> contestedTexts;
    public List<SpriteRenderer> contestedCrownImages;
    public List<SpriteRenderer> allCrownImages;

    public Color32 normalTextColour;
    public Color32 contestedWarningColour;
    public Color32 p1ScoreColour;
    public Color32 p2ScoreColour;
    public Color32 p3ScoreColour;
    public Color32 p4ScoreColour;
    public Material textMaterial;

    public bool countdownTimerActive;
    public bool activeGame;

    private void OnEnable()
    {
        _GameModeManager = GameObject.Find("GameManager").GetComponent<GameModeManager>();
    }

    void Start()
    {
        if (_GameModeManager.mode == GameModeManager.GameMode.KingOfTheHill)
        {
            countdownTimerActive = true;
        }

        countdownTimerText1.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
        countdownTimerText2.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
        countdownTimerText3.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
        countdownTimerText4.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) //Layer 9 should be player
        {
            if (other.GetComponent<PlayerController>())
            {
                playersInZone += 1;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9) //Layer 9 should be player
        {
            if (other.GetComponent<PlayerController>())
            {
                playersInZone -= 1;

                if (playersInZone == 0)
                {
                    foreach (SpriteRenderer crowns in allCrownImages)
                    {
                        crowns.color = normalTextColour;
                    }
                }
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9) //Layer 9 should be player
        {
            if (playersInZone == 1)
            {
                if (addScoreIn != 80)
                {
                    addScoreIn += 1;
                }
                else
                {
                    if (other.GetComponent<PlayerController>())
                    {
                        other.GetComponent<PlayerController>().kingScore += 1;
                        addScoreIn = 0;
                    }
                }

                switch (other.transform.root.name)
                {
                    case "Player 1":
                        foreach (SpriteRenderer crowns in allCrownImages)
                        {
                            crowns.color = p1ScoreColour;
                        }
                        break;
                    case "Player 2":
                        foreach (SpriteRenderer crowns in allCrownImages)
                        {
                            crowns.color = p2ScoreColour;
                        }
                        break;
                    case "Player 3":
                        foreach (SpriteRenderer crowns in allCrownImages)
                        {
                            crowns.color = p3ScoreColour;
                        }
                        break;
                    case "Player 4":
                        foreach (SpriteRenderer crowns in allCrownImages)
                        {
                            crowns.color = p4ScoreColour;
                        }
                        break;

                }
            }
        }
    }

    private void Update()
    {
            #region Zone Countdown clock logic
            if (countdownMinutes > 0)
            {
                if (countdownTimerActive)
                {
                    countdownSeconds -= Time.deltaTime;
                }
            }
            else if (countdownMinutes == 0 && countdownSeconds > 0)
            {
                if (countdownTimerActive)
                {
                    countdownSeconds -= Time.deltaTime;
                }
            }

            if (Mathf.RoundToInt(countdownSeconds) == -1)
            {
                countdownSeconds = 59.4999f;

                if (countdownMinutes > 0)
                {
                    countdownMinutes = countdownMinutes - 1;
                }
            }

            if (Mathf.RoundToInt(countdownSeconds) > 9)
            {
                countdownTimerText1.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
                countdownTimerText2.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
                countdownTimerText3.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
                countdownTimerText4.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
            }
            else
            {
                if (countdownTimerActive)
                {
                    countdownTimerText1.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":0" + Mathf.RoundToInt(countdownSeconds).ToString();
                    countdownTimerText2.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":0" + Mathf.RoundToInt(countdownSeconds).ToString();
                    countdownTimerText3.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":0" + Mathf.RoundToInt(countdownSeconds).ToString();
                    countdownTimerText4.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":0" + Mathf.RoundToInt(countdownSeconds).ToString();
                }
            }

            if (countdownMinutes == 0 && Mathf.RoundToInt(countdownSeconds) == 0)
            {
                countdownSeconds = 0;
                countdownMinutes = 0;                 
            
                int zonePos = Random.Range(0, _GameModeManager.KOTHZonePositions.Count);
                Instantiate(_GameModeManager.KOTHPrefab, _GameModeManager.KOTHZonePositions[zonePos].position, _GameModeManager.KOTHZonePositions[zonePos].rotation, _GameModeManager.KOTHZonePositions[zonePos]);
                
                Destroy(transform.parent.gameObject);
            }

            debuggedTimer -= Time.deltaTime;
            #endregion

            #region Toggle Contested Text and Crowns
            if (playersInZone > 1)
            {
                textMaterial.color = contestedWarningColour;

                foreach (TextMesh textMesh in contestedTexts)
                {
                    textMesh.gameObject.SetActive(true);
                }
                foreach (SpriteRenderer crowns in contestedCrownImages)
                {
                    crowns.gameObject.SetActive(false);
                }
                foreach (SpriteRenderer crowns in allCrownImages)
                {
                    crowns.color = normalTextColour;
                }
            }
            else
            {
                textMaterial.color = normalTextColour;

                foreach (TextMesh textMesh in contestedTexts)
                {
                    textMesh.gameObject.SetActive(false);
                }
                foreach (SpriteRenderer crowns in contestedCrownImages)
                {
                    crowns.gameObject.SetActive(true);
                }
            }
            #endregion

    }
}
