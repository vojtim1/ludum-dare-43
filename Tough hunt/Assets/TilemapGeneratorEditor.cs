using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TilemapGenerator))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TilemapGenerator myScript = (TilemapGenerator)target;
        if(GUILayout.Button("Regenerate"))
        {
            myScript.GenerateTilemap();
        }
    }
}