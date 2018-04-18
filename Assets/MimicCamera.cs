using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicCamera : MonoBehaviour {
    GameObject myCamera;
	// Use this for initialization
	void Start () {
        myCamera = transform.parent.Find("MixedRealityCamera").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localEulerAngles.Set(0,myCamera.transform.localEulerAngles.y,0);
	}
}
