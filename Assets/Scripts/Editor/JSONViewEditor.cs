using System;
using UnityEditor;
using UnityEngine;

public class JSONViewEditor : EditorWindow
{
    private TextAsset templateJson;
    private string loadedJson;

    [MenuItem("GreedyGame/Load View Edit JSON")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow<JSONViewEditor>("Load View Edit JSON", EditorStyles.standardFont);
    }

    private void OnGUI()
    {
        GUILayout.Label("LOAD, VIEW, EDIT JSON Data", EditorStyles.largeLabel);
        templateJson = EditorGUILayout.ObjectField("JSON File", templateJson, typeof(TextAsset), false) as TextAsset;
        if (GUILayout.Button("Load JSON"))
        {
            if (templateJson != null)
            {
                loadedJson = templateJson.text;
                Debug.Log("JSON Data Loaded");
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
        if (GUILayout.Button("Save Changes"))
        {
            if (templateJson != null)
            {
                SaveJSONChanges(templateJson, loadedJson);
            }
            else
            {
                Debug.LogWarning("Check the File");
            }
        }
    }

    private void SaveJSONChanges(TextAsset jsonAsset, string updatedJson)
    {
        string oldJson = jsonAsset.text;
        if (oldJson.Equals(updatedJson))
        {
            Debug.LogWarning("Data Not Changed");
            loadedJson = templateJson.text;
        }
        else
        {
            string assetPath = AssetDatabase.GetAssetPath(jsonAsset);
            System.IO.File.WriteAllText(assetPath, updatedJson);
            AssetDatabase.Refresh();
            Debug.Log("Data Updated!!");
        }
    }
}
