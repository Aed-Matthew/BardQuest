using UnityEngine;
using System.Collections;

public class notePlacerControl : MonoBehaviour {

    public AudioClip soundToPlace;
    private AudioSource soundSource;


    int songPos = 0;



	// Use this for initialization
	void Start () {
        soundSource = gameObject.AddComponent<AudioSource>();
	}


    public void playSong(string songName, float songPos)
    {
        soundSource.clip = Resources.Load("Songs/MusicData/" + songName) as AudioClip;



        //Caluclate delay from slider value, song frequency, and song length
        soundSource.Play();
        soundSource.time = soundToPlace.length * songPos;
    }


    public void saveSong()
    {
        GameObject.Find("soundManager").GetComponent<SoundManager>().saveNotes();
    }
    public void loadSong()
    {
        GameObject.Find("soundManager").GetComponent<SoundManager>().loadSong();
    }



    void Update()
    {
        if (Input.GetKeyDown("e")){
            createNote();
        }


        float moveVal = (songPos - soundSource.timeSamples);
        int sampleRate = soundToPlace.frequency;


        foreach(GameObject cur in GameObject.FindGameObjectsWithTag("note"))
        {
            cur.transform.Translate(moveVal / (float)sampleRate * -1, 0, 0);
        }


        songPos = soundSource.timeSamples;
    }



private void createNote()
    {
        GameObject listObject = GameObject.Find("soundManager");
        NoteAttributes NA = new NoteAttributes();

        NA.spawnTick = soundSource.timeSamples;
        NA.speed = 1.0f;
        NA.myNoteType = noteType.Tap;
        GameObject newNote = NoteHelper.NoteToObject(NA);
        newNote.tag = "note";
        newNote.transform.position = new Vector2(0, 0);
    }

}
