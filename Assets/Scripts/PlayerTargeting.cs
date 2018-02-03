using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour {
    private Text Interact;
    private GameState GS;
    public GameObject C;


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
        Interact.text = transform.name;
        //Interact.color = Color.cyan;
        Interact.enabled = true;
    }
    void OnMouseExit()
    {
        Interact.enabled = false;
    }
}
