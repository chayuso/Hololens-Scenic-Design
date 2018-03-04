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
    }
	
	// Update is called once per frame
	void Update () {
        test.SetActive(false);
        interactionSourceStates = InteractionManager.GetCurrentReading();
        foreach (InteractionSourceState interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.selectPressed)
            {
                test.SetActive(true);
            }
        }

    }
}
