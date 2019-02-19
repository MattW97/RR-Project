using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeManager : MonoBehaviour {

    public bool ScriptActive;
    private GameModeUISelector _GameModeUILink;
    private UtilityManager _utilityManagerLink;

    public enum GameMode { FreeForAll, KingOfTheHill };
    public GameMode mode;
    public List<Transform> KOTHZonePositions;
    public GameObject KOTHPrefab;

    //Game Timer
    public float FFACountdownMinutes;
    public float FFACountdownSeconds;
    public float KOTHCountdownMinutes;
    public float KOTHCountdownSeconds;


    private void Awake()
    {
        _GameModeUILink = GameObject.Find("PlayerPickerInfo").GetComponent<GameModeUISelector>();
        _utilityManagerLink = GameObject.Find("GameManager").GetComponent<UtilityManager>();

    }

    private void Start()
    {
        mode = (GameMode)System.Enum.Parse(typeof(GameMode), _GameModeUILink.gameModeString);

        if (mode == GameMode.FreeForAll)
        {
            _utilityManagerLink.countdownMinutes = FFACountdownMinutes;
            _utilityManagerLink.countdownSeconds = FFACountdownSeconds;
        }
        if (mode == GameMode.KingOfTheHill)
        {
            _utilityManagerLink.countdownMinutes = KOTHCountdownMinutes;
            _utilityManagerLink.countdownSeconds = KOTHCountdownSeconds;

            int zonePos = Random.Range(0, KOTHZonePositions.Count);
            Instantiate(KOTHPrefab, KOTHZonePositions[zonePos].position, KOTHZonePositions[zonePos].rotation, KOTHZonePositions[zonePos]);

        }

    }
}
