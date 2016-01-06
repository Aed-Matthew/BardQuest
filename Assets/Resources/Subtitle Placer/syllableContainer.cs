using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot]
public class syllableContainer {

    [XmlArray("Syllables"), XmlArrayItem("Syllable")]
    public List<syllableTiming> notes = new List<syllableTiming>();

    public void addSyllable(syllableTiming ST)
    {
        notes.Add(ST);
    }

    public MemoryStream saveNote()
    {
        var serializer = new XmlSerializer(typeof(NoteContainer));
        //var stream = new FileStream(path, FileMode.Create);
        var stream = new MemoryStream();
        serializer.Serialize(stream, this);
        return stream;
    }
}
