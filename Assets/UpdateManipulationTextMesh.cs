using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class UpdateManipulationTextMesh : MonoBehaviour {
    private GameObject MainPlayer;
    private string CurrentMode = "YRotation";


    // Use this for initialization
    void Start () {
		
	}
    void ManipulationModeTextUpdate()
    {
        if (CurrentMode != MainPlayer.GetComponent<PlayerState>().ManipulationMode)
        {
            CurrentMode = MainPlayer.GetComponent<PlayerState>().ManipulationMode;
            if (CurrentMode == "YRotation")
            {
                GetComponent<TextMesh>().text = "Rotate Only\nY axis Constraint";
            }
            else if (CurrentMode == "XRotation")
            {
                GetComponent<TextMesh>().text = "Rotate Only\nX axis Constraint";
            }
            else if (CurrentMode == "ZRotation")
            {
                GetComponent<TextMesh>().text = "Rotate Only\nZ axis Constraint";
            }
            else if (CurrentMode == "XYZScale")
            {
                GetComponent<TextMesh>().text = "Scale Only\nXYZ axis Constraint";
            }
            else if (CurrentMode == "MoveRotateScale")
            {
                GetComponent<TextMesh>().text = "Move Rotate Scale";
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "RightCameraUI")
        {
            if (transform.parent.Find("RightController"))
            {
                transform.parent = transform.parent.Find("RightController");
            }
        }
        if (!MainPlayer)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (g.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    MainPlayer = g;
                    break;
                }
            }
        }
        if (MainPlayer)
        {
            ManipulationModeTextUpdate();
        }
    }
}
