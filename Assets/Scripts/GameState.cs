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
    private CameraShiftController CameraShifter;
    private bool RightBumperDown = false;
    private bool LeftBumperDown = false;
    public GameObject BallController;
	// Use this for initialization
	void Start () {
        FPController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        CameraShifter = GameObject.FindObjectOfType<CameraShiftController>();
        BallController = GameObject.Find("BallPlayerController (1)");
        BallController.transform.rotation = FPController.transform.rotation;
        BallController.transform.position = FPController.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        BallController.transform.rotation = FPController.transform.rotation;
        //if (Input.GetAxis("XBOX_DPAD_VERTICAL") > .25)//&& !dragging)
        if (Input.GetButtonUp("XBOX_B"))
        {
            FPController.transform.position = BallController.transform.position;
            
        }
        if (dragging) {
            if (Input.GetButtonUp("XBOX_A"))
            {
                dragging=false;
            }
        }
        if (!disableRiseFall && !dragging)
        {
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > .25 && !RightBumperDown)
            {
                RightBumperDown = true;
                CameraShifter.NextCam();
            }
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > -.1 && Input.GetAxis("XBOX_DPAD_HORIZONTAL") < .1)
            {
                RightBumperDown = false;
            }
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") < -.25 && !LeftBumperDown)
            {
                LeftBumperDown = true;
                CameraShifter.PreviousCam();
            }
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > -.1 && Input.GetAxis("XBOX_DPAD_HORIZONTAL") < .1)
            {
                LeftBumperDown = false;
            }
        }
        if (!disableRiseFall)
        {
            if (Input.GetButton("XBOX_RIGHT_BUMPER"))
            {
                BallController.transform.localPosition = new Vector3(BallController.transform.localPosition.x, BallController.transform.localPosition.y + (speedRiseFall), BallController.transform.localPosition.z);
            }
            if (Input.GetButton("XBOX_LEFT_BUMPER"))
            {
                BallController.transform.localPosition = new Vector3(BallController.transform.localPosition.x, BallController.transform.localPosition.y - (speedRiseFall), BallController.transform.localPosition.z);
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
