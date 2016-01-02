using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


public enum noteType { Tap, DoubleTap, Hold };
public enum movementType {Line, Sway} ;


public class NoteAttributes  {
    public noteType myNoteType = noteType.Tap;
    public movementType myMovementType = movementType.Line;
    public int spawnTick = 0;
    public float speed = 1.0f;
    public float yOffset = 0.0f;
}



