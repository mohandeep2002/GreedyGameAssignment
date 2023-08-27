using System.Collections;
using System.Collections.Generic;
using GreedyGame.Controller;
using UnityEditor;
using UnityEngine;


namespace GreedyGame.EditorScripts
{
    [CustomEditor(typeof(TemplateEditorController))]
    public class UITempEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            TemplateEditorController templateEditorController = (TemplateEditorController)target;
            if (GUILayout.Button("Load Template"))
            {
                templateEditorController.ExportGameObjectToJSON();
            }
        }
    }

}
