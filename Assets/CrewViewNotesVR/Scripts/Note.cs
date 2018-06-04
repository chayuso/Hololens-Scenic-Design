using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour {

    public int id;
    public string project;
    public string title;
    public string author;
    public string dateTime;

    public Vector3 p;
    public Quaternion q;
    
    public float scale;
    
    public string content; // text, imageURL, audioURL...
 

    public AudioClip voiceClip;

    public Note()
    {
        
    }

    private void Awake()
    {
        // when a note prefab is initiated


    }

    // Use this for initialization
    void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {

        
	}




}
