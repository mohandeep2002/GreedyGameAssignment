using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using GreedyGame.Class;

namespace GreedyGame.Controller
{
    public class TemplateCreationController : MonoBehaviour
    {
        [Header("JSON Template Creator")]
        // public List<JSONClass> parentList = new List<JSONClass>();
        public JSONClass parentList;
        public void TemplateCreation()
        {
            string json = JsonConvert.SerializeObject(parentList, Formatting.Indented);
            // print("Calling from editor :" + json);
            string templateName;
            if (parentList.name.Equals(""))
            {
                templateName = "NoNamedJSON";
            }
            else templateName = parentList.name;
            string path = "Assets/Resources/" + parentList.name + ".json";
            File.WriteAllText(path, json);
            Debug.Log("Parent list exported to JSON at " + path);
            Debug.Log("Template created at " + path);
            ResetObject();
        }

        public void ResetObject()
        {
            parentList = null;
        }
    }
}

