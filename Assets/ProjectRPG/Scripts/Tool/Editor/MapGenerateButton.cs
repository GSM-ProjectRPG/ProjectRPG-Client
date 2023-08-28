using UnityEngine;
using UnityEditor;

namespace ProjectRPG
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGenerateButton : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Generate Map", GUILayout.Width(300), GUILayout.Height(30)))
            {
                var generator = (MapGenerator)target;
                generator.GenerateMap();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
}