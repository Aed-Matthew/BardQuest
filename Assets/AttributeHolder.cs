using UnityEngine;
using System.Collections;

public class AttributeHolder : MonoBehaviour {
    public NoteAttributes myNoteAttribute;

    private static GameObject NoteList;
    
    public void setNote(NoteAttributes NA){
        myNoteAttribute = NA;
    }

    public NoteAttributes getNote()
    {
        return myNoteAttribute;
        
    }

	// Use this for initialization
	void Start () {
	if (NoteList == null)
    {
        NoteList = new GameObject();
        NoteList.name = "Note List";
    }

    gameObject.transform.parent = NoteList.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
