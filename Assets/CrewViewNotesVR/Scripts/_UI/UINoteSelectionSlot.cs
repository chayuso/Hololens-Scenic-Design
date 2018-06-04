using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINoteSelectionSlot : MonoBehaviour {

    public int ID;
    public Text title;
    public Text author;
    // public Text title;
    public Text time;
    public Text content;
    // public Image thumbNail;
    public Button selectButton;
    //public Button deleteButton;

    NoteManager noteManager;
    

    // Use this for initialization
    void Start () {

        noteManager = GameObject.FindGameObjectWithTag("NoteManager").GetComponent<NoteManager>();
        
        selectButton.onClick.AddListener(SelectButton);

	}

    private void SelectButton()
    {        
        noteManager.messagePanel.SetActive(true);
        noteManager.noteListPanel.SetActive(false);

        // need to get info about the note by title
        // do not use curNote.title because it will mess up in Update() in NoteManager
        noteManager.messageTitle = title.text;

        // after title has been set, reproduce view by current note title
        noteManager.ReProduceView();


    }

    // Update is called once per frame
    void Update () {
		
	}
}
