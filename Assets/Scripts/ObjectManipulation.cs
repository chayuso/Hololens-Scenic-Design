using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ObjectManipulation : NetworkBehaviour
{
    private float distanceFromObject = 4f;
    public bool isDragging = false;
    public bool canPickup = false;

    private Quaternion originalRotation;
    private Vector3 originalScale;

    private bool SwitchYRotation = false;
    private bool SelectButtonPressed = false;

    private float rotationSpeed = 1f;
    private float scaleSpeed = .1f;

    private bool DpadHclick = false;
    private bool DpadVclick = false;

    // Use this for initialization
    void Start () {
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDragging)
        {
            if (!SelectButtonPressed)
            {
                if (Input.GetButtonDown("CONTROLLER_LEFT_MENU"))
                {
                    SelectButtonPressed = true;
                    SwitchYRotation = !SwitchYRotation;
                }
            }
            else if (Input.GetButtonUp("CONTROLLER_LEFT_MENU"))
            {
                SelectButtonPressed = false;
            }
            ObjectDrag();
            LeftRightTriggerHold();
            if (Input.GetButtonUp("XBOX_X"))
            {
                SpawnDuplicate();
            }
            if (Input.GetButton("XBOX_Y"))
            {
                isDragging = false;
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
                {
                    if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                    {
                        g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().draggingObject = false;
                    }
                }
                Destroy(gameObject);
            }
            if (Input.GetButtonUp("XBOX_A"))
            {
                isDragging = false;
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
                {
                    if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                    {
                        g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().draggingObject = false;
                    }
                }
                    
            }
        }
        else {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
            {
                if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    if (!g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().draggingObject)
                    {
                        if (isDragging)
                        {
                            isDragging = false;
                        }
                        else if (canPickup)
                        {
                            if (Input.GetButtonUp("XBOX_A"))
                            {
                                if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                                {
                                    distanceFromObject = Vector3.Distance(gameObject.transform.position, g.transform.position);
                                    canPickup = false;
                                    isDragging = true;
                                    g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().draggingObject = true;
                                    TurnOffShader();
                                }
                            }
                        }
                    }
                }
            }
        }

        /*if (!isDragging)
        {
            if (canPickup)
            {
                if (Input.GetButtonUp("XBOX_A"))
                {
                    foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
                    {
                        if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                        {
                            distanceFromObject = Vector3.Distance(gameObject.transform.position, g.transform.position);
                            canPickup = false;
                            isDragging = true;
                            TurnOffShader();
                        }
                    }
                }
            }
        }*/

    }

    void ObjectDrag()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromObject);
            if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                Vector3 objPosition = g.gameObject.GetComponent<Camera>().ScreenToWorldPoint(mousePosition);

                transform.position = objPosition;
            }
        }
    }
    void LeftRightTriggerHold()
    {
        if (Input.GetAxis("CONTROLLER_RIGHT_TRIGGER") > .55)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
            {
                if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().disableMove = true;
                }
            }

            RightTriggerRightStick();//Rotating,ResetRotaion

            float lvd = .01f * Input.GetAxis("CONTROLLER_LEFT_STICK_VERTICAL");//BringObjectCloser/Farther
            distanceFromObject += lvd;

            RightTriggerDPad();//RotateByDegree
        }
        else if (Input.GetAxis("CONTROLLER_LEFT_TRIGGER") > .55)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
            {
                if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().disableMove = true;
                }
            }

            ObjectScaling();
            if (Input.GetButton("CONTROLLER_LEFT_STICK_CLICK"))
            {
                transform.localScale = originalScale;
            }
        }
        else
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
            {
                if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().disableMove = false;
                }
            }
        }
    }
    void ObjectScaling()
    {
        if (Input.GetButton("XBOX_RIGHT_BUMPER") || Input.GetAxis("XBOX_DPAD_VERTICAL") > .25)
        {
            transform.localScale = new Vector3(transform.localScale.x + scaleSpeed, transform.localScale.y + scaleSpeed, transform.localScale.z + scaleSpeed);

        }
        else if (Input.GetButton("XBOX_LEFT_BUMPER") || Input.GetAxis("XBOX_DPAD_VERTICAL") < -.25f)
        {
            transform.localScale = new Vector3(transform.localScale.x - scaleSpeed, transform.localScale.y - scaleSpeed, transform.localScale.z - scaleSpeed);
        }
        transform.localScale = new Vector3(transform.localScale.x + Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL") / 2, transform.localScale.y + Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL") / 2, transform.localScale.z + Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL") / 2);

    }
    void RightTriggerRightStick()
    {
        float rv = rotationSpeed * Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL");
        if (SwitchYRotation)
        {
            transform.Rotate(-rv, 0, 0);
        }
        else
        {
            transform.Rotate(0, 0, -rv);
        }

        float rh = rotationSpeed * Input.GetAxis("CONTROLLER_RIGHT_STICK_HORIZONTAL");
        transform.Rotate(0, -rh, 0);
        if (Input.GetButton("CONTROLLER_RIGHT_STICK_CLICK"))
        {
            transform.rotation = originalRotation;
        }
    }
    void RightTriggerDPad()
    {
        if (!DpadVclick)
        {
            if (Input.GetAxis("XBOX_DPAD_VERTICAL") > .25)
            {
                if (SwitchYRotation) { transform.Rotate(22.5f, 0, 0); }
                else { transform.Rotate(0, 0, 22.5f); }
                DpadVclick = true;
            }
            else if (Input.GetAxis("XBOX_DPAD_VERTICAL") < -.25)
            {
                if (SwitchYRotation) { transform.Rotate(-22.5f, 0, 0); }
                else { transform.Rotate(0, 0, -22.5f); }
                DpadVclick = true;
            }
        }
        else
        {
            if (Input.GetAxis("XBOX_DPAD_VERTICAL") > -.1 && Input.GetAxis("XBOX_DPAD_VERTICAL") < .1)
            {
                DpadVclick = false;
            }
        }
        if (!DpadHclick)
        {
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > .25)
            {
                transform.Rotate(0, 22.5f, 0);
                DpadHclick = true;
            }
            else if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") < -.25)
            {
                transform.Rotate(0, -22.5f, 0);
                DpadHclick = true;
            }
        }
        else
        {
            if (Input.GetAxis("XBOX_DPAD_HORIZONTAL") > -.1 && Input.GetAxis("XBOX_DPAD_HORIZONTAL") < .1)
            {
                DpadHclick = false;
            }
        }
    }
    void OnMouseEnter()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                if (!g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().draggingObject)
                {
                    canPickup = true;
                    TurnOnShader();
                    
                }
            }
        }
                    

    }
    void OnMouseExit()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("MainCamera"))
        {
            if (g.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
            {
                if (!g.transform.parent.gameObject.GetComponent<PlayerCharacterControl>().draggingObject)
                {
                    canPickup = false;
                    TurnOffShader();
                }
            }
        }
                    

    }

    void TurnOnShader()
    {
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.cyan);
        }
        foreach (Transform tr in transform)
        {
            if (tr.GetComponent<Renderer>())
            {
                tr.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
                tr.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.cyan);
            }
                
            foreach (Transform trj in tr)
            {
                if (trj.GetComponent<Renderer>())
                {
                    trj.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
                    trj.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.cyan);
                }
                    
            }
        }
    }
    void TurnOffShader()
    {
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        }
        foreach (Transform tr in transform)
        {
            if (tr.GetComponent<Renderer>())
                tr.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            foreach (Transform trj in tr)
            {
                if (trj.GetComponent<Renderer>())
                    trj.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            }
        }
    }
    public void SpawnDuplicate()
    {
        var Dup = (GameObject)Instantiate(
            gameObject,
            transform.position,
            transform.rotation);
        Dup.GetComponent<ObjectManipulation>().canPickup = false;
        Dup.GetComponent<ObjectManipulation>().isDragging= false;

    }
}
