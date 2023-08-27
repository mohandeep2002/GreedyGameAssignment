using System.Collections;
using System.Collections.Generic;
using GreedyGame.Class;
using UnityEngine;


namespace GreedyGame.Controller
{
    public class TemplateEditorController : MonoBehaviour
    {
        public GameObject selectGameObject;
        public JSONClass templateLoaded;

        public void ExportGameObjectToJSON()
        {
            if (selectGameObject == null)
            {
                Debug.LogWarning("No gameobject selected");
                return;
            }
            JSONClass hierarchyJSON = CreateJSONHierarchy(selectGameObject.transform);
            string jsonString = JsonUtility.ToJson(hierarchyJSON, true);
            
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
    }
}
