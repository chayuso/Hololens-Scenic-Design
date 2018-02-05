using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour {
    private Text Interact;
    private GameState GS;
    public GameObject C;
    bool canPickup = false;
    public bool draggingthis = false;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        foreach (Transform tr in GameObject.Find("Canvas").transform)
        {
            if (tr.name=="Interact")
            {
                Interact = tr.GetComponent<Text>();
                break;
            }
        }
        Interact.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {


        if (draggingthis)
        {
            if (GS.translate)
            {
                //code from https://www.youtube.com/watch?v=pK1CbnE2VsI
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                transform.position = objPosition;
            }
            if (Input.GetAxis("CONTROLLER_RIGHT_TRIGGER") > .25)
            {
                GS.FPController.enabled = false;
                float ih = 5f * Input.GetAxis("CONTROLLER_RIGHT_STICK_HORIZONTAL");
                transform.Rotate(0, -ih, 0);
                float iv = 5f * Input.GetAxis("CONTROLLER_LEFT_STICK_VERTICAL");
                transform.Rotate(0, 0, -iv);
            }
            else { GS.FPController.enabled = true; }
        }
        if(!GS.dragging){
            if (draggingthis)
            {
                draggingthis = false;
            }
            if (canPickup)
            {
                if (Input.GetButtonUp("XBOX_A"))
                {
                    OnMouseDown();
                }
            }
        }
    }
    void OnMouseDown()
    {
        if (!GS.dragging)
        {
            //GS.dragging = true;
            Interact.enabled = false;
            canPickup = false;
            GS.currentState = true;
            draggingthis = true;
        }

    }
    /*void OnMouseDown()
    {
        GS.setHolding(true,gameObject);
    }*/
    void OnMouseDrag()
    {
        if (GS.translate)
        {
            //code from https://www.youtube.com/watch?v=pK1CbnE2VsI
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            transform.position = objPosition;
        } else if (GS.rotateX)
        {
            //code from https://answers.unity.com/questions/207239/click-and-drag-to-rotate-an-object-without-changin.html
            float h = 5f * Input.GetAxis("Mouse X");

            transform.Rotate(0,h,0);

            if (Input.GetAxis("CONTROLLER_RIGHT_TRIGGER")>.25)
            {
                float ih = 5f * Input.GetAxis("CONTROLLER_RIGHT_STICK_HORIZONTAL");
                transform.Rotate(0, ih, 0);
            }
            
            
        }
        else if (GS.rotateY)
        {
            //code from https://answers.unity.com/questions/207239/click-and-drag-to-rotate-an-object-without-changin.html
            float h = 5f * Input.GetAxis("Mouse X");

            transform.Rotate(h, 0, 0);
        }
        else if (GS.rotateZ)
        {
            //code from https://answers.unity.com/questions/207239/click-and-drag-to-rotate-an-object-without-changin.html
            float h = 5f * Input.GetAxis("Mouse X");

            transform.Rotate(0, 0, h);
        }
        else if (GS.scale)
        {
            //code from https://answers.unity.com/questions/207239/click-and-drag-to-rotate-an-object-without-changin.html
            float h = 5f * Input.GetAxis("Mouse X");

            transform.localScale=new Vector3(transform.localScale.x+h, transform.localScale.y + h, transform.localScale.z + h);
        }

    }
    void OnMouseEnter()
    {
        if (!GS.dragging)
        {
            Interact.text = transform.name;
            //Interact.color = Color.cyan;
            Interact.enabled = true;
            canPickup = true;
            
        }
    }
    void OnMouseExit()
    {
        if (!GS.dragging)
        {
            Interact.enabled = false;
            canPickup = false;
        }

    }
}
