using System.Collections;
using System.Collections.Generic;
using System.IO;
using GreedyGame.Class;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;


namespace GreedyGame.Controller
{
    public class TemplateEditorController : MonoBehaviour
    {
        public GameObject selectGameObject;
        public JSONClass templateLoaded;
        private string jsonString;

        public void ExportGameObjectToJSON()
        {
            if (selectGameObject == null)
            {
                Debug.LogWarning("No gameobject selected");
                return;
            }
            JSONClass hierarchyJSON = CreateJSONHierarchy(selectGameObject.transform);
            jsonString = JsonUtility.ToJson(hierarchyJSON, true);
            LoadTemplateToInspector(jsonString);
        }

        public void LoadTemplateToInspector(string jsonString)
        {
            Debug.Log(jsonString);
            JSONClass templateData = JsonUtility.FromJson<JSONClass>(jsonString);
            templateLoaded = templateData;
            Debug.Log("Templated Loaded");
        }

        public void SaveUpdatedData()
        {
            string updatedJSON = JsonConvert.SerializeObject(templateLoaded, Formatting.Indented);
            if (updatedJSON == jsonString)
            {
                Debug.LogWarning("No edits found");
                return;
            }
            templateLoaded = JsonUtility.FromJson<JSONClass>(updatedJSON);
            Debug.Log("Template Updated");
            Debug.Log(updatedJSON);
            string path = "Assets/Resources/" + templateLoaded.name + ".json";
            File.WriteAllText(path, updatedJSON);
            Debug.Log("JSON File Updated");
            UpdateGameObject();
        }

        public void ResetData()
        {
            jsonString = "";
            templateLoaded = null;
            selectGameObject = null;
        }
        
        private JSONClass CreateJSONHierarchy(Transform transform)
        {
            JSONClass jsonObject = new JSONClass();
            jsonObject.name = transform.name;
            jsonObject.properties = new Properties()
            {
                position = new CustomVectors
                    { x = transform.position.x, y = transform.position.y, z = transform.position.z },
                rotation = new CustomVectors
                    { x = transform.eulerAngles.x, y = transform.eulerAngles.y, z = transform.eulerAngles.z },
                scale = new CustomVectors
                    { x = transform.localScale.x, y = transform.localScale.y, z = transform.localScale.z },
                color = new ColorData { r = 0, g = 0, b = 0, a = 1 },
                uiAttribute = UIAttribute.GameObject
            };
            int childCount = transform.childCount;
            jsonObject.newChildren = new JSONClass[childCount];
            for (int i = 0; i < childCount; i++)
            {
                Transform childTransform = transform.GetChild(i);
                jsonObject.newChildren[i] = CreateJSONHierarchy(childTransform);
            }
            return jsonObject;
        }

        private void UpdateGameObject()
        {
            JSONClass templateData = templateLoaded;
            GameObject root = CreateUIELements(templateData);
            root.transform.SetParent(GameObject.Find("Generated UI").transform);
            Debug.Log("Updated GameObject");
            DestroyImmediate(selectGameObject);
            ResetData();
        }

        private GameObject CreateUIELements(JSONClass template)
        {
            GameObject uiElement = new GameObject(template.name);
            uiElement.transform.localPosition = new Vector3(template.properties.position.x, template.properties.position.y, template.properties.position.z);
            uiElement.transform.localRotation = Quaternion.Euler(template.properties.rotation.x, template.properties.rotation.y, template.properties.rotation.z);
            uiElement.transform.localScale = new Vector3(template.properties.scale.x, template.properties.scale.y, template.properties.scale.z);
            int attributeValue = (int)template.properties.uiAttribute;
            if (attributeValue == 1) // Button
            {
                uiElement.AddComponent<Button>();
            }
            else if (attributeValue == 2) // Text
            {
                uiElement.AddComponent<TMPro.TextMeshProUGUI>();
            }
            else if (attributeValue == 3) // Image
            {
                uiElement.AddComponent<Image>();
            }

            foreach (var child in template.newChildren)
            {
                GameObject c = CreateUIELements(child);
                c.transform.SetParent(uiElement.transform);
            }
            return uiElement;
        }
        
        
        
        
    }
}
