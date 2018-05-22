using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using HoloToolkit.Unity;
using UnityEngine;


public class VRCameraShifter : NetworkBehaviour
{
    public GameObject[] SceneCameras;
    public GameObject CPlayer;
    public GameObject hotSpotPrefab;
    public int camNum = 0;
    public bool spawnCam = false;
    // Use this for initialization
    void Start()
    {
        if (!CPlayer)
        { CPlayer = gameObject; }

        BuildCamerasArray();
        InitSetFirstCamera();
        //StartCoroutine(NextCamDelay());//Testing Only
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("MC_RIGHT_STICK_CLICK"))
        {
            NextCam();
        }
        if (Input.GetButtonUp("MC_LEFT_STICK_CLICK"))
        {
            PreviousCam();
        }
        if (spawnCam)
        {
            spawnCam = false;
            SpawnCameraHotSpot();
        }
    }
    IEnumerator NextCamDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            NextCam();
        }
    }
    public void SpawnCameraHotSpot()
    {
        var Spawn = (GameObject)Instantiate(
                hotSpotPrefab,
                transform.position,
               new Quaternion(1,1,1,1));
        Spawn.transform.eulerAngles = new Vector3(0, CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.eulerAngles.y, 0);
        BuildCamerasArray();
    }
    public void NextCam()
    {
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            camNum++;
            if (camNum >= SceneCameras.Length)
            {
                camNum = 0;
            }
            if (SceneCameras[camNum].gameObject != gameObject)
            {
                CPlayer.transform.position = SceneCameras[camNum].gameObject.transform.position;
                //CPlayer.transform.eulerAngles = new Vector3(CPlayer.transform.eulerAngles.x, SceneCameras[camNum].gameObject.transform.eulerAngles.y, CPlayer.transform.eulerAngles.z);
                CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.eulerAngles = new Vector3(CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.eulerAngles.x, SceneCameras[camNum].gameObject.transform.eulerAngles.y, CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.eulerAngles.z);
                break;
            }
        }
    }
    public void PreviousCam()
    {
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            camNum--;
            if (camNum < 0)
            {
                camNum = SceneCameras.Length - 1;
            }
            if (SceneCameras[camNum].gameObject != gameObject)
            {
                CPlayer.transform.position = SceneCameras[camNum].gameObject.transform.position;
               // CPlayer.transform.eulerAngles = new Vector3(CPlayer.transform.eulerAngles.x, SceneCameras[camNum].gameObject.transform.eulerAngles.y, CPlayer.transform.eulerAngles.z);
                CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.eulerAngles = new Vector3(CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.eulerAngles.x, SceneCameras[camNum].gameObject.transform.eulerAngles.y, CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.eulerAngles.z);
                break;
            }
        }
    }
    void InitSetFirstCamera()
    {
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            if (SceneCameras[i].gameObject != gameObject)
            {
                CPlayer.transform.position = SceneCameras[i].gameObject.transform.position;
                CPlayer.transform.eulerAngles = new Vector3(CPlayer.transform.eulerAngles.x, SceneCameras[i].gameObject.transform.eulerAngles.y, CPlayer.transform.eulerAngles.z);
                camNum = i;
                break;
            }
        }
        //Turn Off All Cams but Main
        for (int i = 0; i < SceneCameras.Length; i++)
        {
            if (SceneCameras[i].gameObject != gameObject)
            {
                if (SceneCameras[i].GetComponent<Camera>())
                {
                    SceneCameras[i].GetComponent<Camera>().enabled = false;
                    SceneCameras[i].GetComponent<AudioListener>().enabled = false;
                }
            }
        }
    }

    public void BuildCamerasArray()
    {
        SceneCameras = GameObject.FindGameObjectsWithTag("HotSpot");
    }
}
