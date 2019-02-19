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
         FFACountdownSeconds;

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
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(mode_Prop);

        GameModeManager.GameMode gm = (GameModeManager.GameMode)mode_Prop.enumValueIndex;

        switch (gm)
        {
            case GameModeManager.GameMode.FreeForAll:
                //    EditorGUILayout.PropertyField(controllable_Prop, new GUIContent("controllable"));
                //    EditorGUILayout.IntSlider(valForA_Prop, 0, 10, new GUIContent("valForA"));
                //    EditorGUILayout.IntSlider(valForAB_Prop, 0, 100, new GUIContent("valForAB"));

                EditorGUILayout.PropertyField(FFACountdownMinutes, new GUIContent("FFA Countdown Minutes"));
                EditorGUILayout.PropertyField(FFACountdownSeconds, new GUIContent("FFA Countdown Seconds"));
                break;

            case GameModeManager.GameMode.KingOfTheHill:

                EditorGUILayout.PropertyField(zonePositions, new GUIContent("Zone Positions"), true);
                EditorGUILayout.PropertyField(KOTHPrefab, new GUIContent("KOTH Prefab"));
                EditorGUILayout.PropertyField(KOTHCountdownMinutes, new GUIContent("KOTH Countdown Minutes"));
                EditorGUILayout.PropertyField(KOTHCountdownSeconds, new GUIContent("KOTH Countdown Seconds"));

                break;

        }

        serializedObject.ApplyModifiedProperties();
    }
}

    