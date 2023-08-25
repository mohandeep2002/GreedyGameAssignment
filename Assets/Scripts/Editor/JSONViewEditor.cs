using System;
using UnityEditor;
using UnityEngine;

public class JSONViewEditor : EditorWindow
{
    private TextAsset templateJson;
    private string loadedJson;

    [MenuItem("GreedyGame/JSON View")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow<JSONViewEditor>("JSON Data", EditorStyles.standardFont);
    }

    private void OnGUI()
    {
        GUILayout.Label("JSON Data", EditorStyles.largeLabel);
        templateJson = EditorGUILayout.ObjectField("Template JSON", templateJson, typeof(TextAsset), false) as TextAsset;
        if (GUILayout.Button("Load JSON"))
        {
            if (templateJson != null)
            {
                loadedJson = templateJson.text;
            }
            else
            {
                Debug.LogWarning("File Not Attached");
            }
        }
        GUILayout.Space(10);
        GUILayout.Label("Loaded JSON Data: ", EditorStyles.boldLabel);
        loadedJson = EditorGUILayout.TextArea(loadedJson, GUILayout.Height(200));
        GUILayout.Space(10);
    }
}
