using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using UnityEngine;

[XmlRoot("NoteCollection")]
public class NoteContainer {

    public string songName = "Test song";

    [XmlArray("Notes"), XmlArrayItem("Note")]
    public List<NoteAttributes> notes = new List<NoteAttributes>();

    public NoteContainer()
    {

    }

    public void addItem(NoteAttributes NA)
    {
        notes.Add(NA);
    }

    public void testNotes()
    {
        notes.Add(new NoteAttributes());
        notes.Add(new NoteAttributes());
        notes.Add(new NoteAttributes());
    }

}
