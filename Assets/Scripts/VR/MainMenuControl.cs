﻿using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.Collections;
using UnityEngine;

public class MainMenuControl : MonoBehaviour {
    public GameObject[] MainMenuObjects;
    public GameObject[] SpawnMenuObjects;
    public GameObject[] ChairSpawnables;
    public GameObject[] TableSpawnables;
    public GameObject[] SofaSpawnables;
    public GameObject[] BookcaseSpawnables;
    public GameObject[] BedSpawnables;
    public GameObject[] SetSelectObjects;
    public GameObject[] LightObjects;
    public GameObject[] DecorationObjects;
    public GameObject SpawnZone;

    public bool ShowMainMenu = false;
    public bool ShowNone = false;
    public bool ShowSpawnMenu = false;
    public bool SpawnTestChair = false;
    private ObjectCollection OC;
    public GameObject testChairPrefab;
    public string MenuMode = "None";
    // Use this for initialization
    void Start () {
        OC = gameObject.GetComponent<ObjectCollection>();
        HideMenus();
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (SpawnTestChair)
        {
            SpawnTestChair=false;
            SpawnObject(testChairPrefab);
        }
        if (ShowMainMenu)
        {
            ShowMainMenu = false;
            ShowMenu("Main");
        }
        if (ShowSpawnMenu)
        {
            ShowSpawnMenu = false;
            ShowMenu("Spawn");
        }
        if (ShowNone)
        {
            ShowNone = false;
            ShowMenu("None");
        }
        */
        if (Input.GetButtonUp("MC_LEFT_MENU") || Input.GetButtonUp("MC_RIGHT_MENU"))
        {
            if (MenuMode == "None")
            {
                ShowMenu("Main");
            }
            else
            {
                ShowMenu("None");
            }
            
        }
    }
    void HideMenus()
    {
        foreach (GameObject g in MainMenuObjects)
        {
            if (g.tag == "HeightSlider")
            {
                RecursiveRendererEnabler(g, false);
                RecursiveColliderEnabler(g, false);
            }
            else
            {
                g.SetActive(false);
            }
        }
        foreach (GameObject g in SpawnMenuObjects)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in ChairSpawnables)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in SofaSpawnables)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in TableSpawnables)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in BookcaseSpawnables)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in BedSpawnables)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in DecorationObjects)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in SetSelectObjects)
        {
            if (g.tag == "ScaleSetSlider")
            {
                RecursiveRendererEnabler(g, false);
                RecursiveColliderEnabler(g, false);
            }
            else
            {
                g.SetActive(false);
            }
        }
        foreach (GameObject g in LightObjects)
        {
            g.SetActive(false);
        }
        MenuMode = "None";
    }
    public void SpawnObject(GameObject prefabG)
    {
        var Spawn = (GameObject)Instantiate(
                prefabG,
                SpawnZone.transform.position,
                prefabG.transform.rotation);
        Spawn.transform.parent = GameObject.FindGameObjectWithTag("Environment").transform;
    }
    private void RecursiveRendererEnabler(GameObject gObject, bool setEnable)
    {
        if (gObject.GetComponent<MeshRenderer>())
        {
            gObject.GetComponent<MeshRenderer>().enabled = setEnable;
        }
        if (gObject.transform.childCount > 0)
        {

            foreach (Transform childObject in gObject.transform)
            {
                RecursiveRendererEnabler(childObject.gameObject, setEnable);
            }
        }
    }
    private void RecursiveColliderEnabler(GameObject gObject, bool setEnable)
    {
        if (gObject.GetComponent<BoxCollider>())
        {
            gObject.GetComponent<BoxCollider>().enabled = setEnable;
        }
        if (gObject.GetComponent<MeshCollider>())
        {
            gObject.GetComponent<MeshCollider>().enabled = setEnable;
        }
        if (gObject.GetComponent<SphereCollider>())
        {
            gObject.GetComponent<SphereCollider>().enabled = setEnable;
        }
        if (gObject.transform.childCount > 0)
        {

            foreach (Transform childObject in gObject.transform)
            {
                RecursiveColliderEnabler(childObject.gameObject, setEnable);
            }
        }
    }
    public void ShowMenu(string menu)
    {
        if (menu == "Main")
        {
            HideMenus();
            foreach (GameObject g in MainMenuObjects)
            {
                if (g.tag == "HeightSlider")
                {
                    RecursiveRendererEnabler(g, true);
                    RecursiveColliderEnabler(g, true);
                }
                else
                {
                    g.SetActive(true);
                }
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Spawn")
        {
            HideMenus();
            foreach (GameObject g in SpawnMenuObjects)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Beds")
        {
            HideMenus();
            foreach (GameObject g in BedSpawnables)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Chairs")
        {
            HideMenus();
            foreach (GameObject g in ChairSpawnables)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Tables")
        {
            HideMenus();
            foreach (GameObject g in TableSpawnables)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Sofas")
        {
            HideMenus();
            foreach (GameObject g in SofaSpawnables)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Bookcases")
        {
            HideMenus();
            foreach (GameObject g in BookcaseSpawnables)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Sets")
        {
            HideMenus();
            foreach (GameObject g in SetSelectObjects)
            {
                if (g.tag == "ScaleSetSlider")
                {
                    RecursiveRendererEnabler(g, true);
                    RecursiveColliderEnabler(g, true);
                }
                else
                {
                    g.SetActive(true);
                }
            }
        }
        else if (menu == "Light")
        {
            HideMenus();
            foreach (GameObject g in LightObjects)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "Decorations")
        {
            HideMenus();
            foreach (GameObject g in DecorationObjects)
            {
                g.SetActive(true);
            }
            //OC.UpdateCollection();
        }
        else if (menu == "None")
        {
            HideMenus();
        }
        MenuMode = menu;
    }
}
