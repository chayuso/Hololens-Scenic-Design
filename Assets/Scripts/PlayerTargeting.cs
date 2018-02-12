using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour {
    //private Text Interact;
    private Image uiAButton;
    private GameState GS;
    public bool canPickup = false;
    public bool draggingthis = false;
    private bool DpadHclick = false;
    private bool DpadVclick = false;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private bool SwitchYRotation = false;
    private float distanceFromObject = 4f;
    private bool SelectButtonPressed = false;
    private float rotationSpeed = 1f;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        foreach (Transform tr in GameObject.Find("Canvas").transform)
        {
            if (tr.name=="AButton")
            {
                uiAButton = tr.GetComponent<Image>();
                break;
            }
        }
        uiAButton.enabled = false;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {


        if (draggingthis)
        {
            if (GS.translate)
            {
                //code from https://www.youtube.com/watch?v=pK1CbnE2VsI
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromObject);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                transform.position = objPosition;
            }
            if (!SelectButtonPressed) {
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
            /*if (Input.GetButton("CONTROLLER_RIGHT_STICK_CLICK"))
            {
                transform.rotation = originalRotation;
            }
            if (Input.GetButton("CONTROLLER_LEFT_STICK_CLICK"))
            {
                transform.localScale = originalScale;
            }*/
            
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
            if (Input.GetButtonUp("XBOX_X"))
            {
                SpawnDuplicate();
            }
            if (Input.GetAxis("CONTROLLER_RIGHT_TRIGGER") > .55)
            {
                GS.FPController.enabled = false;
                GS.BallController.GetComponent<FirstPersonController>().enabled = false;
                float rv = rotationSpeed * Input.GetAxis("CONTROLLER_RIGHT_STICK_VERTICAL");
                if (SwitchYRotation) { transform.Rotate(-rv, 0, 0); }
                else { transform.Rotate(0, 0, -rv); }
                
                float rh = rotationSpeed * Input.GetAxis("CONTROLLER_RIGHT_STICK_HORIZONTAL");
                transform.Rotate(0, -rh, 0);

                /*float lh = rotationSpeed * Input.GetAxis("CONTROLLER_LEFT_STICK_HORIZONTAL");
                if (SwitchYRotation) { transform.Rotate(0, 0, -lh); }
                else { transform.Rotate(-lh, 0, 0); }*/


                float lvd = .01f * Input.GetAxis("CONTROLLER_LEFT_STICK_VERTICAL");
                distanceFromObject += lvd;
                /*float lv = 5f * Input.GetAxis("CONTROLLER_LEFT_STICK_VERTICAL");
                if (SwitchYRotation) { transform.Rotate(-lv, 0, 0); }
                else { transform.Rotate(0, 0, -lv); }*/
                if (Input.GetButton("CONTROLLER_RIGHT_STICK_CLICK"))
                {
                    transform.rotation = originalRotation;
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
            //else { GS.FPController.enabled = true; }
            else if (Input.GetAxis("CONTROLLER_LEFT_TRIGGER") > .55)
            {
                GS.disableRiseFall = true;
                GS.FPController.enabled = false;
                GS.BallController.GetComponent<FirstPersonController>().enabled = false;
                //float lvd = .01f*Input.GetAxis("CONTROLLER_LEFT_STICK_VERTICAL");
                //distanceFromObject += lvd;

                if (Input.GetButton("XBOX_RIGHT_BUMPER"))
                {
                    transform.localScale = new Vector3(transform.localScale.x + GS.scaleSpeed, transform.localScale.y + GS.scaleSpeed, transform.localScale.z + GS.scaleSpeed);

                }
                else if (Input.GetButton("XBOX_LEFT_BUMPER"))
                {
                    transform.localScale = new Vector3(transform.localScale.x - GS.scaleSpeed, transform.localScale.y - GS.scaleSpeed, transform.localScale.z - GS.scaleSpeed);
                }
                if (Input.GetButton("CONTROLLER_LEFT_STICK_CLICK"))
                {
                    transform.localScale = originalScale;
                }
            }
            else {
                GS.FPController.enabled = true;
                GS.BallController.GetComponent<FirstPersonController>().enabled = true;
                GS.disableRiseFall= false; }

            if (Input.GetButton("XBOX_Y"))
            {
                draggingthis = false;
                GS.dragging = false;
                Destroy(gameObject);
            }
        }
        if(!GS.dragging){
            if (draggingthis)
            {
                draggingthis = false;
            }
            else if (canPickup)
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
            distanceFromObject = Vector3.Distance(gameObject.transform.position, GS.FPController.gameObject.transform.position);
            uiAButton.enabled = false;
            canPickup = false;
            GS.currentState = true;
            draggingthis = true;
            TurnOffShader();
        }

    }
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
            //Interact.text = transform.name;
            //Interact.color = Color.cyan;
            uiAButton.enabled = true;
            canPickup = true;
            TurnOnShader();


        }
        
    }
    void OnMouseExit()
    {
        if (!GS.dragging)
        {
            uiAButton.enabled = false;
            canPickup = false;
            TurnOffShader();
        }

    }
    void TurnOnShader()
    {
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
        }
        foreach (Transform tr in transform)
        {
            if (tr.GetComponent<Renderer>())
                tr.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
            foreach (Transform trj in tr)
            {
                if (trj.GetComponent<Renderer>())
                    trj.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
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
        Dup.GetComponent<PlayerTargeting>().canPickup = false;
        Dup.GetComponent<PlayerTargeting>().draggingthis = false;

    }
}
