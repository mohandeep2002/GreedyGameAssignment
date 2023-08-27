using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using GreedyGame.Class;
using Newtonsoft.Json;
using System.Text.Json;

namespace GreedyGame.EditorScripts
{
    public class UITemplate : EditorWindow
    {
        private TextAsset templateJson;
        private Vector2 scrollPosition;

        [MenuItem("GreedyGame/UI Creator From Template")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow<UITemplate>("UI Creator From Template");
        }
        
        private void OnGUI()
        {
            GUILayout.Label("UI Creator From Template", EditorStyles.largeLabel);
            templateJson = EditorGUILayout.ObjectField("JSON File", templateJson, typeof(TextAsset), false) as TextAsset;
            if (GUILayout.Button("Generate UI in Hierarchy"))
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
            if (json.Length == 0)
            {
                Debug.LogWarning("Empty JSON file");
                return;
            }
            JSONClass templateData = JsonUtility.FromJson<JSONClass>(json);
            GameObject root = InstantiateUIElement(templateData);
            root.transform.SetParent(GameObject.Find("Generated UI").transform);
        }
        
        private GameObject InstantiateUIElement(JSONClass template)
        {
            GameObject uiElement = new GameObject(template.name);
            uiElement.transform.localPosition = new Vector3(template.properties.position.x, template.properties.position.y, template.properties.position.z);
            uiElement.transform.localRotation = Quaternion.Euler(template.properties.rotation.x, template.properties.rotation.y, template.properties.rotation.z);
            uiElement.transform.localScale = new Vector3(template.properties.scale.x, template.properties.scale.y, template.properties.scale.z);
            int attributeValue = (int)template.properties.uiAttribute;
            if (attributeValue == 0) // Button
            {
                uiElement.AddComponent<Button>();
            }
            else if (attributeValue == 1) // Text
            {
                uiElement.AddComponent<TMPro.TextMeshProUGUI>();
            }
            else if (attributeValue == 2) // Image
            {
                uiElement.AddComponent<Image>();
            }

            foreach (var child in template.newChildren)
            {
                GameObject c = InstantiateUIElement(child);
                c.transform.SetParent(uiElement.transform);
            }

            return uiElement;
        }
    }

    /*[System.Serializable]
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
    }*/
}
