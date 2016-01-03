using UnityEngine;
using System.Collections;

public class gameCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.rect = new Rect(0, 0, 800, 600);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
