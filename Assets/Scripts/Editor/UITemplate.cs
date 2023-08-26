using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITemplate : EditorWindow
{
    private TextAsset templateJson;
    private Vector2 scrollPosition;

    [MenuItem("GreedyGame/UITemplateTool")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow<UITemplate>("UITemplateTool");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("UI Template Tool", EditorStyles.largeLabel);
        templateJson = EditorGUILayout.ObjectField("JSON File", templateJson, typeof(TextAsset), false) as TextAsset;
        if (GUILayout.Button("Generate UI Template"))
        {
            if (templateJson != null)
            {
                string json = templateJson.text;
                InstantiateUITemplate(json);
            }
            else Debug.LogWarning("File Not found");
        }
    }

    private void InstantiateUITemplate(string json)
    {
        UITemplateData templateData = JsonUtility.FromJson<UITemplateData>(json);
        GameObject root = InstantiateUIElement(templateData);
        root.transform.SetParent(Selection.activeTransform);
    }
    
    private GameObject InstantiateUIElement(UITemplateData template)
    {
        GameObject uiElement = new GameObject(template.name);
        uiElement.transform.localPosition = new Vector3(template.position.x, template.position.y, 0);
        uiElement.transform.localRotation = Quaternion.Euler(0, 0, template.rotation);
        uiElement.transform.localScale = new Vector3(template.scale.x, template.scale.y, 1);
        foreach (var component in template.components)
        {
            if (component.type == "Button")
            {
                Button button = uiElement.AddComponent<Button>();
                
                //button.GetComponentInChildren<Text>().text = component.text;
            }
        }

        foreach (var child in template.children)
        {
            GameObject c = InstantiateUIElement(child);
            c.transform.SetParent(uiElement.transform);
        }

        return uiElement;
    }
}

[System.Serializable]
public class UITemplateData
{
    public string name;
    public Vector2 position;
    public float rotation;
    public Vector2 scale;
    public ColorData color;
    public UIComponentData[] components;
    public UITemplateData[] children;
}

[System.Serializable]
public class ColorData
{
    public float r, g, b, a;
}

[System.Serializable]
public class UIComponentData
{
    public string type;
    public string text;
}