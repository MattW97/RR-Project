using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GameModeManager))]
public class GameModeManagerEditor : Editor
{

    public SerializedProperty
         mode_Prop,
         zonePositions,
         KOTHPrefab,
         KOTHCountdownMinutes,
         KOTHCountdownSeconds,
         FFACountdownMinutes,
         FFACountdownSeconds,
         TDMCountdownMinutes,
         TDMCountdownSeconds,
         team1Colours,
         team2Colours;


    void OnEnable()
    {
        // Setup the SerializedProperties
        mode_Prop = serializedObject.FindProperty("mode");
        zonePositions = serializedObject.FindProperty("KOTHZonePositions");
        KOTHPrefab = serializedObject.FindProperty("KOTHPrefab");
        KOTHCountdownMinutes = serializedObject.FindProperty("KOTHCountdownMinutes");
        KOTHCountdownSeconds = serializedObject.FindProperty("KOTHCountdownSeconds");
        FFACountdownMinutes = serializedObject.FindProperty("FFACountdownMinutes");
        FFACountdownSeconds = serializedObject.FindProperty("FFACountdownSeconds");
        TDMCountdownMinutes = serializedObject.FindProperty("TDMCountdownMinutes");
        TDMCountdownSeconds = serializedObject.FindProperty("TDMCountdownSeconds");
        team1Colours = serializedObject.FindProperty("team1Colour");
        team2Colours = serializedObject.FindProperty("team2Colour");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(mode_Prop);

        GameModeManager.GameMode gm = (GameModeManager.GameMode)mode_Prop.enumValueIndex;

        switch (gm)
        {
            case GameModeManager.GameMode.FreeForAll:

                EditorGUILayout.PropertyField(FFACountdownMinutes, new GUIContent("FFA Countdown Minutes"));
                EditorGUILayout.PropertyField(FFACountdownSeconds, new GUIContent("FFA Countdown Seconds"));
                break;

            case GameModeManager.GameMode.KingOfTheHill:

                EditorGUILayout.PropertyField(zonePositions, new GUIContent("Zone Positions"), true);
                EditorGUILayout.PropertyField(KOTHPrefab, new GUIContent("KOTH Prefab"));
                EditorGUILayout.PropertyField(KOTHCountdownMinutes, new GUIContent("KOTH Countdown Minutes"));
                EditorGUILayout.PropertyField(KOTHCountdownSeconds, new GUIContent("KOTH Countdown Seconds"));

                break;

            case GameModeManager.GameMode.TeamDeathmatch:

                EditorGUILayout.PropertyField(TDMCountdownMinutes, new GUIContent("TDM Countdown Minutes"));
                EditorGUILayout.PropertyField(TDMCountdownSeconds, new GUIContent("TDM Countdown Seconds"));
                EditorGUILayout.PropertyField(team1Colours, new GUIContent("Team 1 Colours"));
                EditorGUILayout.PropertyField(team2Colours, new GUIContent("Team 2 Colours"));

                break;

        }

        serializedObject.ApplyModifiedProperties();
    }
}

    