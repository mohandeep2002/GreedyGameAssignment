using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TemplateCreation))]
public class TemplateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TemplateCreation myScript = (TemplateCreation)target;

        if (GUILayout.Button("Call MyFunction"))
        {
            myScript.MyFunction();
        }
    }
}
