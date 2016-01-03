﻿using UnityEngine;
using System.Collections;

//Handles how the notes should look in the editor
public class editorTinter : MonoBehaviour {

	// Use this for initialization

    NoteAttributes NA;
    SpriteRenderer SR;

	void Start () {
        NA = gameObject.GetComponent<AttributeHolder>().getNote();
        SR = gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating("updateColor", 0.0f, 0.1f);
	}

    void updateColor()
    {
        NA = gameObject.GetComponent<AttributeHolder>().getNote();
     if (NA.myNoteType == noteType.Tap)
     {
        SR.color = Color.red;
     }

     switch (NA.myNoteType)
     {
         case noteType.Tap:
             SR.color = Color.red;
             break;

         case noteType.DoubleTap:
             SR.color = Color.green;
             break;

         case noteType.Hold:
             SR.color = Color.magenta;
             break;

     }
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}