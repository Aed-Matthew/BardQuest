﻿using UnityEngine;
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

        if (GUILayout.Button("Play Song"))
        {
            myScript.playSong(songName, songPos);
        }

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