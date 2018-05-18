using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using HoloToolkit.Unity;
using UnityEngine;


public class VRCameraShifter : NetworkBehaviour
{
    public GameObject[] SceneCameras;
    public GameObject CPlayer;
    public int camNum = 0;
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
    }
    IEnumerator NextCamDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            NextCam();
        }
    }
    public void SpawnCameraHotSpot(GameObject prefabG)
    {
        var Spawn = (GameObject)Instantiate(
                prefabG,
                transform.position,
                CameraCache.Main.transform.rotation);
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
                CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.rotation = SceneCameras[camNum].gameObject.transform.rotation;
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
                CPlayer.transform.Find("MixedRealityCameraParent").transform.Find("MixedRealityCamera").transform.rotation = SceneCameras[camNum].gameObject.transform.rotation;
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
                CPlayer.transform.rotation = SceneCameras[i].gameObject.transform.rotation;
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
