using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ObjectPointerManipulation>())
        {
            other.gameObject.GetComponent<ObjectPointerManipulation>().OnPointerEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ObjectPointerManipulation>())
        {
            other.gameObject.GetComponent<ObjectPointerManipulation>().OnPointerExit();
        }
    }
}
