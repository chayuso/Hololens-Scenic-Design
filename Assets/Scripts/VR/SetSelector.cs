using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Examples.InteractiveElements;
using UnityEngine;

public class SetSelector : MonoBehaviour {
    public List <GameObject> EnvironmentPrefabs;
    public GameObject CurrentSet;
    SliderGestureControl sliderG;
    public VRCameraShifter CamShifter;
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
        sliderG = GameObject.FindGameObjectWithTag("ScaleSetSlider").GetComponent<SliderGestureControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!sliderG)
        {
            sliderG = GameObject.FindGameObjectWithTag("ScaleSetSlider").GetComponent<SliderGestureControl>();
        }
        else
        {
            CurrentSet.transform.localScale = new Vector3(Mathf.Clamp(1 + .1f * sliderG.SliderValue,.1f,100), Mathf.Clamp(1 + .1f * sliderG.SliderValue, .1f, 100), Mathf.Clamp(1 + .1f * sliderG.SliderValue, .1f, 100));
        }
    }
    public void SelectSetNumber(int num)
    {
        if (num < EnvironmentPrefabs.Count)
        {
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
            Destroy(CurrentSet);
           
            SpawnSet(EnvironmentPrefabs[num]);
            StartCoroutine(recursiveDelay());
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
    IEnumerator recursiveDelay()
    {
        yield return new WaitForSeconds(.01f);
        RecursiveHotSpotAdder(CurrentSet);
        CamShifter.BuildCamerasArray();
        CamShifter.NextCam();
    }
    private void RecursiveHotSpotAdder(GameObject gObject)
    {
        if (gObject.GetComponent<Camera>() && gObject.tag!="HotSpot")
        {
            CamShifter.SpawnCameraHotSpotPosRot(gObject);
            gObject.GetComponent<Camera>().enabled = false;
        }
        if (gObject.transform.childCount > 0)
        {

            foreach (Transform childObject in gObject.transform)
            {
                RecursiveHotSpotAdder(childObject.gameObject);
            }
        }
    }
}
