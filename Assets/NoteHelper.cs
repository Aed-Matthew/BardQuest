using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

using System.Xml;
using System.Xml.Serialization;
public static class NoteHelper
{
    private const float sampleRate = 44100.0f;

    //Turns a noteAttribute into a gameObject
    public static GameObject NoteToObject(NoteAttributes AttributeClass)
    {
        GameObject temp = GameObject.Instantiate(Resources.Load("Prefabs/CustomEditor/musicNote") as GameObject);
        temp.name = "Note";
        temp.GetComponent<AttributeHolder>().setNote(AttributeClass);
        temp.transform.position = GetNotePos(AttributeClass);
        temp.tag = "note";
        temp.transform.Translate(0, AttributeClass.yOffset, 0);

        return temp;
    }

    public static NoteAttributes ObjectToNote(GameObject NoteClass)
    {
        var AttributeData = NoteClass.GetComponent<AttributeHolder>();
        if (AttributeData == null) {return null;}
        AttributeData.getNote().yOffset = NoteClass.transform.position.y;
        return AttributeData.getNote();
    }


    public static void saveNoteContainer(string songName, NoteContainer NC)
    {

        string path = "Assets/Resources/Songs/NoteData/" + songName + ".xml";
        var serializer = new XmlSerializer(typeof(NoteContainer));
        var stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, NC);
        stream.Close();
        UnityEditor.AssetDatabase.Refresh();
    }

    public static float samplesToOffset(int samples)
    {
        return (float)samples / sampleRate;
    }

    public static Vector2 GetNotePos(NoteAttributes NoteAtt) //Returns where a note should spawn
    {
        Vector2 pos = new Vector2();

        float offset = ((float)NoteAtt.spawnTick / 44100.0f);
        pos.y = NoteAtt.yOffset;
        pos.x = -offset;
        return pos;
    }

    public static Vector2 GetNotePos(GameObject NoteObj)
    {
        return (
            GetNotePos(NoteObj.GetComponent<NoteAttributes>())
            );
    }

    public static NoteContainer loadSong(string songName)
    {
        NoteContainer myNoteContainer;
        string path = "Assets/Resources/Songs/NoteData/" + songName + ".xml";

        if (songName.Equals(""))
        {
            Debug.LogError("No song set in soundManager");
            return null;
        }

        TextAsset musicData = Resources.Load("Songs/NoteData/" + songName) as TextAsset;


        if (musicData == null)
        {
            myNoteContainer = new NoteContainer();
        }
        else
        {
            var serializer = new XmlSerializer(typeof(NoteContainer));
            var stream = new FileStream(path, FileMode.Open);
            NoteContainer container = serializer.Deserialize(stream) as NoteContainer;
            stream.Close();

            myNoteContainer = container;

            Debug.Log("Loaded song with " + container.notes.Count + " notes.");
        }

        return myNoteContainer;
    }
}
