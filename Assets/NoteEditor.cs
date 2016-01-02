using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AttributeHolder))]
public class NoteEditor : Editor {


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();


        

        AttributeHolder myScript = (AttributeHolder)target;

        if (GUILayout.Button("Build Object"))
        {

        }

    }

}
