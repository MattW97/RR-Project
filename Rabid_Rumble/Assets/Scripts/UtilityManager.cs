using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UtilityManager : MonoBehaviour
{
    public float countdownSeconds = 30;
    public float countdownMinutes = 1;
    public float debuggedTimer = 90;

    public Canvas inGameUI;
    public Text countdownTimerText;
    public GameObject gameOverUI;

    public bool activeGame;
    public bool countdownTimerActive;

    void Start()
    {
        activeGame = false;
        countdownTimerActive = false;
        inGameUI.gameObject.SetActive(false);
        gameOverUI.SetActive(false);

        countdownTimerText.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":0" + Mathf.RoundToInt(countdownSeconds).ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            // Reload current scene
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if(activeGame)
        {
            inGameUI.gameObject.SetActive(true);

            #region Countdown clock logic
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

                if(countdownMinutes > 0)
                {
                    countdownMinutes = countdownMinutes - 1;
                } 
            }

            if (Mathf.RoundToInt(countdownSeconds) > 9)
            {
                countdownTimerText.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":" + Mathf.RoundToInt(countdownSeconds).ToString();
            }
            else
            {
                if(countdownTimerActive)
                {
                    countdownTimerText.text = Mathf.RoundToInt(countdownMinutes).ToString() + ":0" + Mathf.RoundToInt(countdownSeconds).ToString();
                }                
            }

            if (countdownMinutes == 0 && Mathf.RoundToInt(countdownSeconds) == 0)
            {
                countdownSeconds = 0;
                countdownMinutes = 0;
                countdownTimerText.text = "ROUND OVER";

                gameOverUI.SetActive(true);
            }

            debuggedTimer -= Time.deltaTime;
            #endregion
        }
    }

    public void Restart()
    {
        // Reload current scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
