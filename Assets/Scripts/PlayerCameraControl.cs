using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCameraControl : NetworkBehaviour
{
    public Vector3 CurrentPosition;
    [SerializeField]
    public MouseLook m_MouseLook;
    private CameraTeleport CameraShifter;
    private bool RightBumperDown = false;
    private bool LeftBumperDown = false;
    public Vector3 Camrotation;

    private float speed = 200f;
    public float journeyLength;
    private bool gradient=false;
    GameObject CanvasUI;
    Color gradientColor;
    private float fadeRate=5f;
    
    // Use this for initialization
    void Start () {
        if (!transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        else
        {
            gameObject.GetComponent<Camera>().enabled = true;
            CurrentPosition = transform.position;
            m_MouseLook.Init(transform.parent.transform, transform);
            CameraShifter = GetComponent<CameraTeleport>();
            CanvasUI = GameObject.FindGameObjectWithTag("Canvas");
            CanvasUI.GetComponent<Canvas>().enabled = true;

            gradientColor = CanvasUI.transform.Find("Gradient").GetComponent<Image>().color;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        else
        {
            //transform.position = CurrentPosition;
            float distCoveredCamera = Time.deltaTime * (speed * 0.1f);
            float fracJourneyCamera = distCoveredCamera / journeyLength;
            transform.position = Vector3.Lerp(transform.position, CurrentPosition, fracJourneyCamera);
            if (!transform.parent.gameObject.GetComponent<PlayerCharacterControl>().disableMove)
            {
                RotateView();
            }

            if (gradient)
            {
                transform.parent.GetComponent<Renderer>().enabled = true;
                if (Vector3.Distance(transform.position, CurrentPosition) < .1f)
                {
                    gradient = false;
                    transform.parent.GetComponent<Renderer>().enabled = false;
                }
                
                CanvasUI.transform.Find("Gradient").GetComponent<Image>().color=Color.Lerp(CanvasUI.transform.Find("Gradient").GetComponent<Image>().color, gradientColor, fadeRate*3*Time.deltaTime);
            }
            else
            {
                transform.parent.GetComponent<Renderer>().enabled = false;
                CanvasUI.transform.Find("Gradient").GetComponent<Image>().color=Color.Lerp(CanvasUI.transform.Find("Gradient").GetComponent<Image>().color, Color.clear, fadeRate/2*Time.deltaTime);
            }
            if (Input.GetAxis("Horizontal") <.19  && Input.GetAxis("Horizontal") > -.19
                && Input.GetAxis("Vertical") < .19 && Input.GetAxis("Vertical") > -.19
                && !Input.GetButton("XBOX_RIGHT_BUMPER")
                && !Input.GetButton("XBOX_LEFT_BUMPER")
                &&  Input.GetAxis("XBOX_DPAD_VERTICAL") < .25
                && Input.GetAxis("XBOX_DPAD_VERTICAL") > -.25)
            {
                if (Vector3.Distance(transform.position, transform.parent.transform.position) > .01f)
                {
                        CurrentPosition = new Vector3(transform.parent.position.x, transform.parent.position.y , transform.parent.position.z );
                        journeyLength = Vector3.Distance(transform.position, CurrentPosition);
                        gradient = true;
                        
                }
            }
            else
            {
                if (Input.GetAxis("CONTROLLER_RIGHT_TRIGGER") < .55 && Input.GetAxis("CONTROLLER_LEFT_TRIGGER") < .55)
                {
                    transform.parent.GetComponent<Renderer>().enabled = true;
                }
            }

            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > .25 && !RightBumperDown && !transform.parent.gameObject.GetComponent<PlayerCharacterControl>().disableMove)
            {
                RightBumperDown = true;
                CameraShifter.NextCam();
            }
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > -.1 && Input.GetAxis("XBOX_DPAD_HORIZONTAL") < .1)
            {
                RightBumperDown = false;
            }
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") < -.25 && !LeftBumperDown && !transform.parent.gameObject.GetComponent<PlayerCharacterControl>().disableMove)
            {
                LeftBumperDown = true;
                CameraShifter.PreviousCam();
            }
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > -.1 && Input.GetAxis("XBOX_DPAD_HORIZONTAL") < .1)
            {
                LeftBumperDown = false;
            }

        }
    }
    void FixedUpdate()
    {
        if (!transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        else
        {
            m_MouseLook.UpdateCursorLock();
        }
    }
    private void RotateView()
    {
        m_MouseLook.LookRotation(transform.parent.transform, transform);
    }
}
