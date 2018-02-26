using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerCharacterControl : NetworkBehaviour
{
    public bool disableMove = false;
    public bool draggingObject = false;
    private float speedRiseFall = .05f;
    private GameObject CanvasMenu;
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
            return;
        }
        GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.green);
        CanvasMenu = GameObject.Find("Canvas");
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CurrentCanvasViewController();
        if (!disableMove)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
            transform.Translate(x, 0, 0);
            transform.Translate(0, 0, z);

            if (Input.GetButton("XBOX_RIGHT_BUMPER")|| Input.GetAxis("XBOX_DPAD_VERTICAL") > .25)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (speedRiseFall), transform.localPosition.z);
            }
            if (Input.GetButton("XBOX_LEFT_BUMPER") || Input.GetAxis("XBOX_DPAD_VERTICAL") < -.25f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (speedRiseFall), transform.localPosition.z);
            }
        }
        
    }
    void CurrentCanvasViewController()
    {
        if (draggingObject)
        {
            if (Input.GetAxis("CONTROLLER_RIGHT_TRIGGER") > .55)
            {
                foreach (Transform tr in CanvasMenu.transform)
                {
                    if (tr.name == "FreeRoam")
                    {
                        tr.gameObject.SetActive(false);
                    }
                    else if (tr.name == "Holding Object")
                    {
                        tr.gameObject.SetActive(false);
                    }
                    else if (tr.name == "RightTriggerHolding")
                    {
                        tr.gameObject.SetActive(true);
                    }
                    else if (tr.name == "LeftTriggerHolding")
                    {
                        tr.gameObject.SetActive(false);
                    }
                }
            }
            else if (Input.GetAxis("CONTROLLER_LEFT_TRIGGER") > .55)
            {
                foreach (Transform tr in CanvasMenu.transform)
                {
                    if (tr.name == "FreeRoam")
                    {
                        tr.gameObject.SetActive(false);
                    }
                    else if (tr.name == "Holding Object")
                    {
                        tr.gameObject.SetActive(false);
                    }
                    else if (tr.name == "RightTriggerHolding")
                    {
                        tr.gameObject.SetActive(false);
                    }
                    else if (tr.name == "LeftTriggerHolding")
                    {
                        tr.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                foreach (Transform tr in CanvasMenu.transform)
                {
                    if (tr.name == "FreeRoam")
                    {
                        tr.gameObject.SetActive(false);
                    }
                    else if (tr.name == "Holding Object")
                    {
                        tr.gameObject.SetActive(true);
                    }
                    else if (tr.name == "RightTriggerHolding")
                    {
                        tr.gameObject.SetActive(false);
                    }
                    else if (tr.name == "LeftTriggerHolding")
                    {
                        tr.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            foreach (Transform tr in CanvasMenu.transform)
            {
                if (tr.name == "FreeRoam")
                {
                    tr.gameObject.SetActive(true);
                }
                else if (tr.name == "Holding Object")
                {
                    tr.gameObject.SetActive(false);
                }
                else if (tr.name == "RightTriggerHolding")
                {
                    tr.gameObject.SetActive(false);
                }
                else if (tr.name == "LeftTriggerHolding")
                {
                    tr.gameObject.SetActive(false);
                }
            }
        }
    }
}
