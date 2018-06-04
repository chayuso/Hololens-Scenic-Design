using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProjectSlot : MonoBehaviour {

    // public int ID;
    public Text title;
    // public Text author;
    // public Text title;
    // public Text time;    
    // public Image thumbNail;

    // select project
    public Button selectProjectButton;
    Manager manager;
   
    // Use this for initialization
    void Start () {

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        selectProjectButton.onClick.AddListener(SelectButton);

    }

    private void SelectButton()
    {

        manager.curProject = title.text;

        manager.OpenProject(gameObject.name);   
                
    }

    // Update is called once per frame
    void Update () {
		
	}
}
