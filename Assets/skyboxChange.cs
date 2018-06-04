using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxChange : MonoBehaviour {
    public Material Day;
    public GameObject textDay;
    public GameObject textNight;
    public GameObject lightDay;
    public GameObject lightNight;
    public Material Night;
    public bool isDayTime = true;
	// Use this for initialization
	void Start () {
        //Day = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () {
        if (isDayTime)
        {
            RenderSettings.skybox = Day;
            textDay.SetActive(true);
            textNight.SetActive(false);
            lightDay.SetActive(true);
            lightNight.SetActive(false);
        }
        else
        {
            RenderSettings.skybox = Night;
            textDay.SetActive(false);
            textNight.SetActive(true);
            lightDay.SetActive(false);
            lightNight.SetActive(true);
        }
	}
    public void toggleDayNightCycle()
    {
        isDayTime = !isDayTime;
    }
}
