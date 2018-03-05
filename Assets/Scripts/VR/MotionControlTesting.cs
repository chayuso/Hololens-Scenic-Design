using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA.Input;
using UnityEngine;

public class MotionControlTesting : MonoBehaviour {
    InteractionSourceState[] interactionSourceStates;
    public GameObject test;

	// Use this for initialization
	void Start () {

        interactionSourceStates = InteractionManager.GetCurrentReading();
        test.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        test.SetActive(false);
        if (Input.GetButton("MC_LEFT_STICK_CLICK"))
        {
            test.SetActive(true);
        }
        /*interactionSourceStates = InteractionManager.GetCurrentReading();
        foreach (InteractionSourceState interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.selectPressed)
            {
                test.SetActive(true);
            }
        }*/

    }
}
