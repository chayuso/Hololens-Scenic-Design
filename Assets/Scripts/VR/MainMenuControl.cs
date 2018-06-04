using System.Collections;
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
    public GameObject SpawnZone;

    public bool ShowMainMenu = false;
    public bool ShowNone = false;
    public bool ShowSpawnMenu = false;
    public bool SpawnTestChair = false;
    private ObjectCollection OC;
    public GameObject testChairPrefab;
    public string MenuMode = "None";

    // Use manager to control canvas
    Manager manager;

    // Use this for initialization
    void Start () {
        OC = gameObject.GetComponent<ObjectCollection>();
        HideMenus();

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        manager.canvas.SetActive(false);
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
            g.SetActive(false);
        }
        foreach (GameObject g in SpawnMenuObjects)
        {
            g.SetActive(false);
        }
<<<<<<< HEAD
                

=======
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
>>>>>>> 8da2c0ae0471983cf86a833c35de4995be53e09b
        MenuMode = "None";
    }
    public void SpawnObject(GameObject prefabG)
    {
        var Spawn = (GameObject)Instantiate(
                prefabG,
                SpawnZone.transform.position,
                prefabG.transform.rotation);
    }
    public void ShowMenu(string menu)
    {
        if (menu == "Main")
        {
            HideMenus();
            foreach (GameObject g in MainMenuObjects)
            {
                g.SetActive(true);
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
<<<<<<< HEAD

        else if (menu == "Note")
        {
            HideMenus();
            manager.ShowCanvas();
            manager.curState = Manager.State.note;

            //OC.UpdateCollection();
        }

        else if (menu == "Project")
        {
            HideMenus();
            manager.ShowCanvas();
            manager.curState = Manager.State.selectProject;
        }

=======
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
>>>>>>> 8da2c0ae0471983cf86a833c35de4995be53e09b
        else if (menu == "None")
        {
            HideMenus();
        }
        MenuMode = menu;
    }

    
}
