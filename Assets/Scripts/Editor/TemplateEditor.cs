using UnityEditor;
using UnityEngine;
using GreedyGame.Controller;

namespace GreedyGame.EditorScripts
{
    [CustomEditor(typeof(TemplateCreationController))]
    public class TemplateEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TemplateCreationController myScript = (TemplateCreationController)target;

            if (GUILayout.Button("Generate Template"))
            {
                myScript.MyFunction();
                AssetDatabase.Refresh();
            }
        }
    }
}

