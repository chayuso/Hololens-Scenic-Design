using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttatchColliders : MonoBehaviour {
    //Attatch this on the EnvironmentGameObject
	// Use this for initialization
	void Start () {
        RecursiveColliderAdder(gameObject);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void RecursiveColliderAdder(GameObject gObject)
    {
        if (gObject.GetComponent<MeshRenderer>()||gObject.GetComponent<SkinnedMeshRenderer>())
        {
            if (!gObject.GetComponent<MeshCollider>())
            {
                gObject.AddComponent<MeshCollider>();
            }
        }
        if (gObject.transform.childCount > 0)
        {

            foreach (Transform childObject in gObject.transform)
            {
                RecursiveColliderAdder(childObject.gameObject);
            }
        }
    }
}
