using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

using System.Xml;
using System.Xml.Serialization;


public class SoundManager : MonoBehaviour {

    NoteContainer myNoteContainer;
    public string songName = "";
    private string path;

	// Use this for initialization
	void Start () {


      //  Invoke("saveData", 2.0f);

	}
	
	// Update is called once per frame
	void Update () {
        
	}


    public void loadSong()
    {
        foreach( GameObject GO in GameObject.FindGameObjectsWithTag("note"))
        {
            Destroy(GO);
        }

        myNoteContainer = NoteHelper.loadSong(songName);
        instantiateNotes();


        foreach (GameObject GO in GameObject.FindGameObjectsWithTag("note"))
        {
          //Offset based on song 
            float pos = NoteHelper.samplesToOffset(GameObject.Find("notePlacer").GetComponent<AudioSource>().timeSamples);
            GO.transform.Translate(pos, 0, 0);
        }
    }





    //Take all notes on the field and convert them into a list
    void recompileList()
    {

    }

    void createNotes()
    {

    }

    public void saveNotes()
    {
        //Add note objects to myNotecontainer

        myNoteContainer = new NoteContainer();

        GameObject[] grabList = GameObject.FindGameObjectsWithTag("note");

        foreach(GameObject GO in grabList)
        {
            myNoteContainer.addItem(NoteHelper.ObjectToNote(GO));
        }
        NoteHelper.saveNoteContainer(songName, myNoteContainer);
    }



    void instantiateNotes()
    {
        foreach (NoteAttributes NA in myNoteContainer.notes)
        {
            NoteHelper.NoteToObject(NA);
        }
    }


    //This is not worth the trouble. I'll look into an XML reader/writer for note data.

    void saveData() //Change to save button later
    {
        NoteHelper.saveNoteContainer(songName, myNoteContainer);
    }
}
