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
        public string templateName;
        // public List<JSONClass> parentList = new List<JSONClass>();
        public JSONClass parentList;
        public void TemplateCreation()
        {
            string json = JsonConvert.SerializeObject(parentList, Formatting.Indented);
            print("Calling from editor :" + json);
            if (templateName.Equals("")) templateName = "defaultJSON";
            string path = "Assets/Resources/GeneratedJSON/" + templateName + ".json";
            File.WriteAllText(path, json);
            Debug.Log("Parent list exported to JSON at " + path);
        }

        public void ResetObject()
        {
            parentList = null;
            templateName = "";
        }
    }
}

