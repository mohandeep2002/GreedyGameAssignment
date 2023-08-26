using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class TemplateCreation : MonoBehaviour
{
    public List<Parent> parentList = new List<Parent>(); 
    
    //[MenuItem("MyMenu/Export Parent List to JSON")]
    public void MyFunction()
    {
        print("Calling from editor :" + parentList[0].properties.name);
        
        //string json = JsonConvert.SerializeObject(parentList, Formatting.Indented);
        //string json = JsonUtility.ToJson(parentList, true);
        string json = JsonConvert.SerializeObject(parentList, Formatting.Indented);
        print("Calling from editor :" + json);
        string path = "Assets/parentlist.json"; // Change this to your desired path
        File.WriteAllText(path, json);
        Debug.Log("Parent list exported to JSON at " + path);
    }
}

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
}