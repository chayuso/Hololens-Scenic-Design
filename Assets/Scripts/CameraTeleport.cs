using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CameraTeleport :NetworkBehaviour
{
    public GameObject[] ScenePlayers;
    public GameObject CPlayer;
    public int camNum = 0;
    // Use this for initialization
    void Start()
    {
        if (!transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            return;
        }
        if (!CPlayer)
        { CPlayer = gameObject; }

        BuildCamerasArray();
        InitSetFirstCamera();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextCam()
    {
        BuildCamerasArray();
        for (int i = 0; i < ScenePlayers.Length; i++)
        {
            camNum++;
            if (camNum >= ScenePlayers.Length)
            {
                camNum = 0;
            }
            if (ScenePlayers[camNum].gameObject != gameObject)
            {
                CPlayer.transform.position = ScenePlayers[camNum].gameObject.transform.position;
                GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = ScenePlayers[camNum].gameObject.transform.parent.transform.localRotation;
                GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                break;
            }
        }
    }
    public void PreviousCam()
    {
        BuildCamerasArray();
        for (int i = 0; i < ScenePlayers.Length; i++)
        {
            camNum--;
            if (camNum < 0)
            {
                camNum = ScenePlayers.Length - 1;
            }
            if (ScenePlayers[camNum].gameObject != gameObject)
            {
                CPlayer.transform.position = ScenePlayers[camNum].gameObject.transform.position;
                GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = ScenePlayers[camNum].gameObject.transform.parent.transform.localRotation;
                GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                break;
            }
        }
    }
    void InitSetFirstCamera()
    {
        for (int i = 0; i < ScenePlayers.Length; i++)
        {
            if (ScenePlayers[i].gameObject != gameObject)
            {
                CPlayer.transform.position = ScenePlayers[i].gameObject.transform.position;
                GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = ScenePlayers[i].gameObject.transform.parent.transform.localRotation;
                GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                camNum = i;
                break;
            }
        }
        //Turn Off All Cams but Main
        for (int i = 0; i < ScenePlayers.Length; i++)
        {
            if (ScenePlayers[i].gameObject != gameObject)
            {
                ScenePlayers[i].GetComponent<Camera>().enabled = false;
                ScenePlayers[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    void BuildCamerasArray()
    {
        if (ScenePlayers.Length!= GameObject.FindGameObjectsWithTag("MainCamera").Length)
            ScenePlayers = GameObject.FindGameObjectsWithTag("MainCamera");
    }
}
