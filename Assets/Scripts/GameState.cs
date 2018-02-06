using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class GameState : MonoBehaviour {
    public bool translate = true;
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;
    public bool scale = false;
    public int mode = 1;
    public bool dragging = false;
    private bool liftedMouseButton = true;
    public bool previousState=false;
    public bool currentState = false;
    public FirstPersonController FPController;
    private float speedRiseFall=.05f;
    public float scaleSpeed = 5f;
    public bool disableRiseFall = false;
	// Use this for initialization
	void Start () {
        FPController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (dragging) {
            if (Input.GetButtonUp("XBOX_A"))
            {
                dragging=false;
            }
        }
        if (!disableRiseFall)
        {
            if (Input.GetButton("XBOX_RIGHT_BUMPER"))
            {
                FPController.transform.localPosition = new Vector3(FPController.transform.localPosition.x, FPController.transform.localPosition.y + (speedRiseFall), FPController.transform.localPosition.z);
            }
            if (Input.GetButton("XBOX_LEFT_BUMPER"))
            {
                FPController.transform.localPosition = new Vector3(FPController.transform.localPosition.x, FPController.transform.localPosition.y - (speedRiseFall), FPController.transform.localPosition.z);
            }
        }
        if (currentState!=previousState)
        {
            currentState=false;
            dragging = !dragging;
        }
        if (Input.GetMouseButtonDown(1)&&liftedMouseButton)
        {
            ToggleMode();
            liftedMouseButton = false;
        }
        if (!liftedMouseButton)
        {
            if (Input.GetMouseButtonUp(1))
            {
                liftedMouseButton = true;
            }
        }
	}
    public void ToggleMode()
    {
        if (mode == 5)
        {
            mode = 1;
            translate = true;
            rotateX = false;
            rotateY = false;
            rotateZ = false;
            scale = false;
        }
        else if (mode == 1)
        {
            mode = 2;
            translate = false;
            rotateX = true;
            rotateY = false;
            rotateZ = false;
            scale = false;
        }
        else if (mode == 2)
        {
            mode = 3;
            translate = false;
            rotateX = false;
            rotateY = true;
            rotateZ = false;
            scale = false;
        }
        else if (mode == 3)
        {
            mode = 4;
            translate = false;
            rotateX = false;
            rotateY = false;
            rotateZ = true;
            scale = false;
        }
        else if (mode == 4)
        {
            mode = 5;
            translate = false;
            rotateX = false;
            rotateY = false;
            rotateZ = false;
            scale = true;
        }
    }
    public void setMode(int number)
    {
        if (number == 1)
        {
            mode = 1;
            translate = true;
            rotateX = false;
            rotateY = false;
            rotateZ = false;
            scale = false;
        }
        else if (number == 2)
        {
            mode = 2;
            translate = false;
            rotateX = true;
            rotateY = false;
            rotateZ = false;
            scale = false;
        }
        else if (number == 3)
        {
            mode = 3;
            translate = false;
            rotateX = false;
            rotateY = true;
            rotateZ = false;
            scale = false;
        }
        else if (number == 4)
        {
            mode = 4;
            translate = false;
            rotateX = false;
            rotateY = false;
            rotateZ = true;
            scale = false;
        }
        else if (number == 5)
        {
            mode = 5;
            translate = false;
            rotateX = false;
            rotateY = false;
            rotateZ = false;
            scale = true;
        }
    }
    
}
