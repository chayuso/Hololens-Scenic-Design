using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA.Input;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
using UnityEngine;

public class MotionControlTesting : NetworkBehaviour
{
    /*InteractionSourceState[] interactionSourceStates;
    public GameObject test;
    public GameObject prefabG;
	// Use this for initialization
	void Start () {

        interactionSourceStates = InteractionManager.GetCurrentReading();
        
    }*/

    // Update is called once per frame
    public Vector3 CurrentPosition;
    [SerializeField]
    public MouseLook m_MouseLook;
    private CameraTeleport CameraShifter;
    private bool RightBumperDown = false;
    private bool LeftBumperDown = false;
    public Vector3 Camrotation;

    private float speed = 400f;
    public float journeyLength;
    public bool gradient = false;
    private float fadeRate = 5f;
    Color gradientColor;
    GameObject raycamera;
    public float elevation = -.5f;
    // Use this for initialization
    void Start()
    {
        if (!transform.parent.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        else
        {
            gameObject.GetComponent<Camera>().enabled = true;
            CurrentPosition = new Vector3(transform.position.x, transform.position.y + elevation, transform.position.z);
            m_MouseLook.Init(transform.parent.parent.transform, transform);
            CameraShifter = GetComponent<CameraTeleport>();
            raycamera = GameObject.FindGameObjectWithTag("raycam");
        }
        gradientColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

        if (!transform.parent.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        else
        {
            //transform.position = CurrentPosition;
            float distCoveredCamera = Time.deltaTime * (speed * 0.1f);
            float fracJourneyCamera = distCoveredCamera / journeyLength;
            transform.position = Vector3.Lerp(transform.position, new Vector3(CurrentPosition.x,CurrentPosition.y+elevation,CurrentPosition.z), fracJourneyCamera);
            transform.parent.position = Vector3.Lerp(transform.parent.position, new Vector3(CurrentPosition.x, CurrentPosition.y + elevation, CurrentPosition.z), fracJourneyCamera);
            if (!transform.parent.parent.gameObject.GetComponent<PlayerVRCharacter>().disableMove)
            {
                RotateView();
            }

            if (gradient)
            {
                transform.parent.parent.GetComponent<Renderer>().enabled = true;
                if (Vector3.Distance(transform.position, new Vector3(CurrentPosition.x, CurrentPosition.y + elevation, CurrentPosition.z)) < .1f)
                {
                    gradient = false;
                    transform.parent.parent.GetComponent<Renderer>().enabled = false;
                }
                transform.Find("FadePlane").GetComponent<Renderer>().material.color = Color.Lerp(transform.Find("FadePlane").GetComponent<Renderer>().material.color, gradientColor, fadeRate * 3 * Time.deltaTime);
            }
            else
            {
                transform.parent.parent.GetComponent<Renderer>().enabled = false;
                transform.Find("FadePlane").GetComponent<Renderer>().material.color = Color.Lerp(transform.Find("FadePlane").GetComponent<Renderer>().material.color, Color.clear, fadeRate / 2 * Time.deltaTime);

            }
            if (Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") <= .19 && Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") >= -.19
                && Input.GetAxis("MC_LEFT_STICK_VERTICAL") <= .19 && Input.GetAxis("MC_LEFT_STICK_VERTICAL") >= -.19
                /*&& Input.GetAxis("MC_RIGHT_STICK_HORIZONTAL") <= .19 && Input.GetAxis("MC_RIGHT_STICK_HORIZONTAL") >= -.19
                && Input.GetAxis("MC_RIGHT_STICK_VERTICAL") <= .19 && Input.GetAxis("MC_RIGHT_STICK_VERTICAL") >= -.19*/
                //&& !Input.GetButton("MC_LEFT_GRIP")
                //&& Input.GetAxis("XBOX_DPAD_VERTICAL") < .25
                //&& Input.GetAxis("XBOX_DPAD_VERTICAL") > -.25
                )
            {
                if (Vector3.Distance(transform.position, new Vector3(transform.parent.parent.transform.position.x, transform.parent.parent.transform.position.y+elevation, transform.parent.parent.transform.position.z)) > .01f)
                {
                    CurrentPosition = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.parent.position.z);
                    journeyLength = Vector3.Distance(transform.position, new Vector3(CurrentPosition.x, CurrentPosition.y + elevation, CurrentPosition.z));
                    
                    
                    gradient = true;
                 
                }
               
            }
            else
            {
                //if (Input.GetAxis("CONTROLLER_RIGHT_TRIGGER") < .55 && Input.GetAxis("CONTROLLER_LEFT_TRIGGER") < .55)
                //{
                    transform.parent.parent.GetComponent<Renderer>().enabled = true;
               
                //}
            }

            //if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > .25 && !RightBumperDown && !transform.parent.gameObject.GetComponent<PlayerCharacterControl>().disableMove)
            if(Input.GetButton("MC_LEFT_STICK_CLICK"))
            {
                //RightBumperDown = true;
                CameraShifter.NextCam();
            }
            /*if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > -.1 && Input.GetAxis("XBOX_DPAD_HORIZONTAL") < .1)
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
            }*/

        }
    }
    void FixedUpdate()
    {
        if (!transform.parent.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        else
        {
            m_MouseLook.UpdateCursorLock();
            raycamera.transform.position = transform.position;
        }
    }
    private void LateUpdate()
    {
        RotateView();
    }
    private void RotateView()
    {
        //m_MouseLook.LookRotation(transform.parent.parent.transform, transform);
        Quaternion rloc = transform.rotation;
        //transform.parent.parent.transform.localRotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        //transform.parent.parent.transform.rotation = rloc;

    }
}
    /*public void Spawn()
    {
        test = (GameObject)Instantiate(
            prefabG,
            GameObject.FindGameObjectWithTag("GameController").transform.Find("LeftController").transform.position,
            GameObject.FindGameObjectWithTag("GameController").transform.Find("LeftController").transform.rotation);

    }*/
