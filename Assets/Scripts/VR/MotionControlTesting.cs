using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.WSA.Input;
using UnityEngine;

public class MotionControlTesting : MonoBehaviour {
    InteractionSourceState[] interactionSourceStates;
    public GameObject test;
    public GameObject prefabG;
	// Use this for initialization
	void Start () {

        interactionSourceStates = InteractionManager.GetCurrentReading();
        
    }
	
	// Update is called once per frame
	void Update () {
        /*if (test)
        {
            test.SetActive(false);
        }
       
        if (Input.GetButton("MC_LEFT_STICK_CLICK"))
        {
            if (!test)
            {
                Spawn();
                test.transform.parent = GameObject.FindGameObjectWithTag("GameController").transform.Find("LeftController").transform;
                test.SetActive(false);
            }
            test.SetActive(true);
        }*/
        /*interactionSourceStates = InteractionManager.GetCurrentReading();
        foreach (InteractionSourceState interactionSourceState in interactionSourceStates)
        {
            if (interactionSourceState.selectPressed)
            {
                test.SetActive(true);
            }
        }*/

    }
    public void Spawn()
    {
        test = (GameObject)Instantiate(
            prefabG,
            GameObject.FindGameObjectWithTag("GameController").transform.Find("LeftController").transform.position,
            GameObject.FindGameObjectWithTag("GameController").transform.Find("LeftController").transform.rotation);

    }
}
