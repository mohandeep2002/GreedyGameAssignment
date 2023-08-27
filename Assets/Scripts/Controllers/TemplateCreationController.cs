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
        public void MyFunction()
        {
            string json = JsonConvert.SerializeObject(parentList, Formatting.Indented);
            print("Calling from editor :" + json);
            if (templateName.Equals("")) templateName = "defaultJSON";
            string path = "Assets/Resources/GeneratedJSON/" + templateName + ".json";
            File.WriteAllText(path, json);
            Debug.Log("Parent list exported to JSON at " + path);
            
        }
    }
    
    /*
    [Serializable]
    public class Parent
    {
        public Properties properties;
        public Children[] addChildren;
    }
    
    [Serializable]
    public class Children
    {
        public Parent childParent;
    }
    [Serializable]
    public class Properties
    {
        public string name;
        public Vectors position;
        public Vectors rotation;
        public Vectors scale;
        public string colour;
        public UIattribute uiAttribute;
    }
    [Serializable]
    public class Vectors
    {
        public float x;
        public float y;
        public float z;
    }
    [Serializable]
    public enum UIattribute
    {
        Button,
        Text,
        Image
    }*/
}

