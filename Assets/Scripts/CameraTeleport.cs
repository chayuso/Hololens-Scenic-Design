using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class CameraTeleport :NetworkBehaviour
{
    public GameObject[] ScenePlayers;
    public List<GameObject> SceneCameras;
    public GameObject CPlayer;
    public int camNum = 0;
    public int playercamNum = 0;

    private float speed = 200f;
    public float journeyLength;
    private bool gradient = false;
    GameObject CanvasUI;
    Color gradientColor;
    private float fadeRate = 5f;
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
        for (int i = 0; i < SceneCameras.Count; i++)
        {
            camNum++;
            if (camNum >= SceneCameras.Count)
            {
                camNum = 0;
            }
            if (SceneCameras[camNum].gameObject != gameObject)
            {
                CPlayer.transform.position = SceneCameras[camNum].gameObject.transform.position;

                if (SceneCameras[camNum].gameObject.tag == "MainCamera")
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = SceneCameras[camNum].gameObject.transform.parent.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                else
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = SceneCameras[camNum].gameObject.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                break;
            }
        }
    }
    public void PreviousCam()
    {
        BuildCamerasArray();
        for (int i = 0; i < SceneCameras.Count; i++)
        {
            camNum--;
            if (camNum < 0)
            {
                camNum = SceneCameras.Count - 1;
            }
            if (SceneCameras[camNum].gameObject != gameObject)
            {
                CPlayer.transform.position = SceneCameras[camNum].gameObject.transform.position;

                if (SceneCameras[camNum].gameObject.tag == "MainCamera")
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = SceneCameras[camNum].gameObject.transform.parent.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                else
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = SceneCameras[camNum].gameObject.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                break;
            }
        }
    }
    public void NextCamPlayer()
    {
        BuildCamerasArray();
        for (int i = 0; i < ScenePlayers.Length; i++)
        {
            playercamNum++;
            if (playercamNum >= ScenePlayers.Length)
            {
                playercamNum = 0;
            }
            if (ScenePlayers[playercamNum].gameObject != gameObject)
            {
                CPlayer.transform.position = ScenePlayers[playercamNum].gameObject.transform.position;

                if (ScenePlayers[playercamNum].gameObject.tag == "MainCamera")
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = ScenePlayers[playercamNum].gameObject.transform.parent.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                break;
            }
        }
    }
    public void PreviousCamPlayer()
    {
        BuildCamerasArray();
        for (int i = 0; i < ScenePlayers.Length; i++)
        {
            playercamNum--;
            if (playercamNum < 0)
            {
                playercamNum = ScenePlayers.Length - 1;
            }
            if (ScenePlayers[playercamNum].gameObject != gameObject)
            {
                CPlayer.transform.position = ScenePlayers[playercamNum].gameObject.transform.position;

                if (ScenePlayers[playercamNum].gameObject.tag == "MainCamera")
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = ScenePlayers[playercamNum].gameObject.transform.parent.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                break;
            }
        }
    }
    void InitSetFirstCamera()
    {
        for (int i = 0; i < SceneCameras.Count; i++)
        {
            if (SceneCameras[i].gameObject != gameObject)
            {
                CPlayer.transform.position = SceneCameras[i].gameObject.transform.position;

                if (SceneCameras[i].gameObject.tag == "MainCamera")
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = SceneCameras[i].gameObject.transform.parent.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                else
                {
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CharacterTargetRot = SceneCameras[i].gameObject.transform.localRotation;
                    GetComponent<PlayerCameraControl>().m_MouseLook.m_CameraTargetRot = new Quaternion(0, 0, 0, 1);
                }
                camNum = i;
                break;
            }
        }
        //Turn Off All Cams but Main
        for (int i = 0; i < SceneCameras.Count; i++)
        {
            if (SceneCameras[i].gameObject != gameObject)
            {
                SceneCameras[i].GetComponent<Camera>().enabled = false;
                SceneCameras[i].GetComponent<AudioListener>().enabled = false;
            }
        }
    }

    void BuildCamerasArray()
    {

        if (SceneCameras.Count != GameObject.FindObjectsOfType<Camera>().Length)
        {
            SceneCameras.Clear();
            foreach (Camera g in GameObject.FindObjectsOfType<Camera>())
            {
                SceneCameras.Add(g.gameObject);
            }
        }
        if (ScenePlayers.Length != GameObject.FindGameObjectsWithTag("MainCamera").Length)
            ScenePlayers = GameObject.FindGameObjectsWithTag("MainCamera");

    }
}
