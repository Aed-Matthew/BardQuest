using UnityEngine;
using UnityEditor;

using System.Collections;


[CustomEditor(typeof(notePlacerControl))]
public class NotePlacerButton : Editor{

    string songName = "BardsTheme";
    float songPos = 0.0f;




    public override void OnInspectorGUI()
    {
        notePlacerControl myScript = (notePlacerControl)target;
        songName = EditorGUILayout.TextField("Song name: ", songName);


        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Play"))
        {
            myScript.playSong(songName, songPos);
        }


        if (GUILayout.Button("Pause"))
        {
       //TODO
            myScript.pauseSong();
            songPos = ((float)(myScript.soundSource.timeSamples) / myScript.soundToPlace.frequency)/myScript.soundToPlace.length;
            Debug.Log(songPos);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();


        if (GUILayout.Button("Load Notes"))
        {
            myScript.loadSong();
        }

        if (GUILayout.Button("Save Notes"))
        {
            myScript.saveSong();
        }

        EditorGUILayout.EndHorizontal();


       songPos = GUILayout.HorizontalSlider(songPos, 0.0f, 1.0f);
    }


    void Update()
    {
        
    }

}
