using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {
    public bool LeftHandHolding = false;
    public bool RightHandHolding = false;
    public string CurrentHand = "None"; 
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("MC_LEFT_TRIGGER_BUTTON"))
        {
            CurrentHand = "Left";
        }
        else if (Input.GetButtonDown("MC_LEFT_STICK_CLICK"))
        {
            CurrentHand = "Left";
        }
        else if (Input.GetButtonDown("MC_LEFT_TOUCHPAD_CLICK"))
        {
            CurrentHand = "Left";
        }
        else if (Input.GetButtonDown("MC_LEFT_MENU"))
        {
            CurrentHand = "Left";
        }
        else if (Input.GetButtonDown("MC_LEFT_GRIP"))
        {
            CurrentHand = "Left";
        }
        else if (Input.GetButtonDown("MC_RIGHT_TRIGGER_BUTTON"))
        {
            CurrentHand = "Right";
        }
        else if (Input.GetButtonDown("MC_RIGHT_STICK_CLICK"))
        {
            CurrentHand = "Right";
        }
        else if (Input.GetButtonDown("MC_RIGHT_TOUCHPAD_CLICK"))
        {
            CurrentHand = "Right";
        }
        else if (Input.GetButtonDown("MC_RIGHT_MENU"))
        {
            CurrentHand = "Right";
        }
        else if (Input.GetButtonDown("MC_RIGHT_GRIP"))
        {
            CurrentHand = "Right";
        }
    }
}
