using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AttributeHolder))]
[CanEditMultipleObjects]

public class NoteEditor : Editor {


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
       // DrawDefaultInspector();


        

        AttributeHolder myScript = (AttributeHolder)target;


        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Tap")){
        foreach (Object cur in targets)
        {
            myScript = (AttributeHolder)cur;
            myScript.setNoteType(noteType.Tap);
        }

        }

        if (GUILayout.Button("Hold"))
        {
            foreach (Object cur in targets)
            {
                myScript = (AttributeHolder)cur;
                myScript.setNoteType(noteType.Hold);
            }
        }

        if (GUILayout.Button("Double Tap"))
        {
            foreach (Object cur in targets)
            {
                myScript = (AttributeHolder)cur;
                myScript.setNoteType(noteType.DoubleTap);
            }
        }




        GUILayout.EndHorizontal();

    }

}
