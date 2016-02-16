using UnityEngine;
using System.Collections;

public class notePlacerControl : MonoBehaviour {

    public AudioClip soundToPlace;
    public AudioSource soundSource;

    LineRenderer lineRenderer;
    private float lineRendererOffset = 0f;

    int songPos = 0;



	// Use this for initialization
	void Start () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        soundSource = gameObject.AddComponent<AudioSource>();
	}


    public void playSong(string songName, float songPos)
    {
        if (soundSource == null) { return; }
        soundSource.clip = Resources.Load("Songs/MusicData/" + songName) as AudioClip;



        //Caluclate delay from slider value, song frequency, and song length
        soundSource.Play();
        soundSource.time = soundToPlace.length * songPos;
    }

    public void pauseSong()
    {
        soundSource.Pause();

    }


    public void saveSong()
    {
        GameObject.Find("soundManager").GetComponent<SoundManager>().saveNotes();
    }
    public void loadSong()
    {
        GameObject.Find("soundManager").GetComponent<SoundManager>().loadSong();
    }


    private void updateLineRenderer()
    {

        lineRenderer.SetWidth(0.1f, 0.2f);
        float moveVal = (songPos - soundSource.timeSamples);
        int sampleRate = soundToPlace.frequency;


       // lineRendererOffset += (moveVal / (float)sampleRate * -1);


        float[] Samples = new float[256];
        soundSource.GetSpectrumData(Samples, 0, FFTWindow.Hamming);

        Vector3[] points = new Vector3[256];
        for(int i = 0; i < 256; i++)
        {
            points[i] = new Vector3(i*1, Samples[i]*20, 0);
        }




        lineRenderer.SetVertexCount(256);
        lineRenderer.SetPositions(points);
    }
    void Update()
    {
        updateLineRenderer();



        float parseTime = soundSource.timeSamples / 44100f;
        GameObject.FindObjectOfType<SubtitleParserScript>().setTime(parseTime);

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



public void createNote()
    {
        GameObject listObject = GameObject.Find("soundManager");
        NoteAttributes NA = new NoteAttributes();

        NA.spawnTick = soundSource.timeSamples;
        NA.speed = 1.0f;
        NA.myNoteType = noteType.Tap;
        GameObject newNote = NoteHelper.NoteToObject(NA);
        newNote.tag = "note";
        newNote.transform.position = new Vector2(0, 0);
        newNote.transform.localPosition = Vector2.zero;
    }

}
