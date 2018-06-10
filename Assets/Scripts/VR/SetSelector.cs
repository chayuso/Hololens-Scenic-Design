using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSelector : MonoBehaviour {
    public List <GameObject> EnvironmentPrefabs;
    public GameObject CurrentSet;
	// Use this for initialization
	void Start () {
        if (!GameObject.FindGameObjectWithTag("Environment"))
        {
            SpawnSet(EnvironmentPrefabs[0]);
        }
        else
        {
            CurrentSet = GameObject.FindGameObjectWithTag("Environment");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SelectSetNumber(int num)
    {
        if (num < EnvironmentPrefabs.Count)
        {
            Destroy(CurrentSet);
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("BoundingBoxCorner"))
            {
                Destroy(g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Gizmo"))
            {
                Destroy(g);
            }
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Spawnable"))
            {
                Destroy(g);
            }
            SpawnSet(EnvironmentPrefabs[num]);
        }
        else
        {
            Debug.Log("Out of index: SetSpawner");
        }
    }
    void SpawnSet(GameObject prefabG)
    {
        CurrentSet = (GameObject)Instantiate(
                prefabG,
                new Vector3(0,0,0),
                prefabG.transform.rotation);
        CurrentSet.tag = "Environment";
    }
}
