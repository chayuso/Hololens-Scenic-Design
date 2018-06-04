using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine.XR.WSA.Input;
using UnityEngine;

public class VRMenuControls : MonoBehaviour {
    public GameObject MainMenu;
    public MotionControllerVisualizer MCV;
    public GameObject ControllerText;
    public bool ControllerSpawned = false;
    public float ControllerOffsetX = 0;
    public float ControllerOffsetY = 0;
    public float ControllerOffsetZ = -0.05f;
    public Vector3 ControllerPos = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start () {
        
    }


    // Update is called once per frame
    void Update () {
        /*if (Input.GetButtonUp("MC_LEFT_MENU")|| Input.GetButtonUp("MC_RIGHT_MENU"))
        {
            MainMenu.SetActive(!MainMenu.active);
        }*/
        /*if (!ControllerSpawned)
        {
            if (MCV.rightControllerModel.ControllerParent)
            {
                ControllerPos = MCV.rightControllerModel.ControllerParent.transform.position;
                ControllerText.transform.position =
                    new Vector3(
                        ControllerPos.x + ControllerOffsetX,
                        ControllerPos.y + ControllerOffsetY,
                        ControllerPos.z + ControllerOffsetZ);
                ControllerSpawned = true;
            }
        }
        if (ControllerSpawned)
        {
            ControllerPos = MCV.rightControllerModel.ControllerParent.transform.position;
            ControllerText.transform.position =
                       new Vector3(
                           ControllerPos.x + ControllerOffsetX,
                           ControllerPos.y + ControllerOffsetY,
                           ControllerPos.z + ControllerOffsetZ);
        }*/


    }
}
