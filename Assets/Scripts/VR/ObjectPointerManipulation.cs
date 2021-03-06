﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPointerManipulation : MonoBehaviour {
    private float distanceFromObject = 4f;
    public bool isDragging = false;
    public bool canPickup = false;
    public string heldbyHand = "None";
    private Quaternion ObjectRotation;
    private float rotationSpeed = 1f;
    private float distanceSpeed = -10f;
    // Use this for initialization
    void Start () {
        ObjectRotation = transform.rotation; 
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = ObjectRotation;
        //LeftControl();
        RightControl();
       
    }
    void LeftControl()
    {
        if (isDragging)
        {
            if (heldbyHand == "Left")
            {
                LeftObjectDrag();
                if (Input.GetButtonUp("MC_LEFT_MENU"))
                {
                    SpawnDuplicate();
                }
                if (Input.GetButton("MC_LEFT_STICK_CLICK"))
                {
                    isDragging = false;
                    GameObject g = GameObject.FindGameObjectWithTag("Player");
                    g.GetComponent<PlayerState>().LeftHandHolding = false;
                    Destroy(gameObject);
                }
                if (Input.GetButton("MC_LEFT_TOUCHPAD_CLICK"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.parent.transform.position, Input.GetAxis("MC_LEFT_TOUCHPAD_VERTICAL")*distanceSpeed * Time.deltaTime);

                }
                else
                {
                    float lh = rotationSpeed * Input.GetAxis("MC_LEFT_TOUCHPAD_HORIZONTAL");
                    transform.Rotate(0, -lh, 0);
                    ObjectRotation = transform.rotation;
                }
            }
            
            if ((Input.GetButtonDown("MC_LEFT_TRIGGER_BUTTON") || Input.GetButtonDown("MC_LEFT_GRIP")) && heldbyHand == "Left")
            {
                GameObject g = GameObject.FindGameObjectWithTag("Player");
                isDragging = false;
                transform.parent = null;
                g.GetComponent<PlayerState>().LeftHandHolding = false;
                heldbyHand = "None";
            }
        }
        else
        {
            GameObject g = GameObject.FindGameObjectWithTag("Player");
            //foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            //{
                if (!g.GetComponent<PlayerState>().LeftHandHolding)
                {
                    if (isDragging)
                    {
                        isDragging = false;
                    }
                    else if (canPickup)
                    {
                        if ((Input.GetButtonDown("MC_LEFT_TRIGGER_BUTTON")|| Input.GetButtonDown("MC_LEFT_GRIP")) && g.GetComponent<PlayerState>().CurrentHand=="Left")
                        {
                            GameObject MC = GameObject.FindGameObjectWithTag("GameController");
                            Vector3 LPosition = MC.transform.Find("LeftController").transform.position;
                            distanceFromObject = Vector3.Distance(gameObject.transform.position, LPosition);
                            canPickup = false;
                            isDragging = true;
                            g.GetComponent<PlayerState>().LeftHandHolding=true;
                            heldbyHand = "Left";
                            TurnOffShader();
                        }
                    }
                 }            
            //}
        }
    }
    void RightControl()
    {
        if (isDragging)
        {
            if (heldbyHand == "Right")
            {
                RightObjectDrag();
                if (Input.GetButtonUp("MC_RIGHT_MENU"))
                {
                    SpawnDuplicate();
                }
                if (Input.GetButton("MC_RIGHT_STICK_CLICK"))
                {
                    isDragging = false;
                    GameObject g = GameObject.FindGameObjectWithTag("Player");
                    g.GetComponent<PlayerState>().RightHandHolding = false;
                    Destroy(gameObject);
                }
                if (Input.GetButton("MC_RIGHT_TOUCHPAD_CLICK"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.parent.transform.position, Input.GetAxis("MC_RIGHT_TOUCHPAD_VERTICAL") * distanceSpeed * Time.deltaTime);

                }
                else
                {
                    float rh = rotationSpeed * Input.GetAxis("MC_RIGHT_TOUCHPAD_HORIZONTAL");
                    transform.Rotate(0, -rh, 0);
                    ObjectRotation = transform.rotation;
                }
            }
            if ((Input.GetButtonDown("MC_RIGHT_TRIGGER_BUTTON") || Input.GetButtonDown("MC_RIGHT_GRIP")) && heldbyHand == "Right")
            {
                GameObject g = GameObject.FindGameObjectWithTag("Player");
                isDragging = false;
                transform.parent = null;
                g.GetComponent<PlayerState>().RightHandHolding = false;
                heldbyHand = "None";
            }
        }
        else
        {
            GameObject g = GameObject.FindGameObjectWithTag("Player");
            //foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            //{
            if (!g.GetComponent<PlayerState>().RightHandHolding)
            {
                if (isDragging)
                {
                    isDragging = false;
                }
                else if (canPickup)
                {
                    if ((Input.GetButtonDown("MC_RIGHT_TRIGGER_BUTTON") || Input.GetButtonDown("MC_RIGHT_GRIP")) && g.GetComponent<PlayerState>().CurrentHand == "Right")
                    {
                        GameObject MC = GameObject.FindGameObjectWithTag("GameController");
                        Vector3 RPosition = g.transform.Find("MixedRealityCameraParent").Find("MotionControllers").Find("RightController").transform.position;//MC.transform.Find("RightController").transform.position;
                        distanceFromObject = Vector3.Distance(gameObject.transform.position, RPosition);
                        canPickup = false;
                        isDragging = true;
                        g.GetComponent<PlayerState>().RightHandHolding = true;
                        heldbyHand = "Right";
                        TurnOffShader();
                    }
                }
            }
            //}
        }
    }
    void LeftObjectDrag()
    {
        /*Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromObject);
        Vector3 objPosition = g.gameObject.GetComponent<Camera>().ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;*/
        transform.parent = GameObject.FindGameObjectWithTag("GameController").transform.Find("LeftController").transform;

    }
    void RightObjectDrag()
    {
        /*Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromObject);
        Vector3 objPosition = g.gameObject.GetComponent<Camera>().ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;*/
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform.Find("MixedRealityCameraParent").Find("MotionControllers").Find("RightController").transform;

    }
    public void OnPointerEnter()
    {
        if (!isDragging)
        {
            GameObject g = GameObject.FindGameObjectWithTag("Player");
            /*if (g.GetComponent<PlayerState>().CurrentHand == "Left")
            {
                if (!g.GetComponent<PlayerState>().LeftHandHolding)
                {
                    canPickup = true;
                    TurnOnShader();
                }
            }*/
            if (g.GetComponent<PlayerState>().CurrentHand == "Right")
            {
                if (!g.GetComponent<PlayerState>().RightHandHolding)
                {
                    canPickup = true;
                    TurnOnShader();
                }
            }
            else
            {
                canPickup = true;
                TurnOnShader();
            }
        }
    }
    public void OnPointerExit()
    {
        canPickup = false;
        TurnOffShader();
    }
    public void TurnOnShader()
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
    public void TurnOffShader()
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
        Dup.GetComponent<ObjectPointerManipulation>().canPickup = false;
        Dup.GetComponent<ObjectPointerManipulation>().isDragging = false;

    }
}
