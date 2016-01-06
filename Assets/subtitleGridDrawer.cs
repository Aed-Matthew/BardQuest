using UnityEngine;
using System.Collections;

public class subtitleGridDrawer : MonoBehaviour {
    public int[] sentenceTimingsInSeconds;


    public int testVar = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;


        for (int i = 0; i < 20; i++)
        {
            Gizmos.DrawLine(gameObject.transform.position + new Vector3(0, -i*50, 0), gameObject.transform.position + new Vector3(1000, -i * 50, 0));
        }
    }

    void OnGUI()
    {
        if (Application.isEditor)  // or check the app debug flag
        {
            GUI.Label(new Rect(0,0,100,100), "Debug text");
        }
    }
}
