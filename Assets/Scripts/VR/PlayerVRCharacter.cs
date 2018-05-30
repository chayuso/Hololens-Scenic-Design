using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR;
#if UNITY_WSA
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Input;
#endif
#else
using UnityEngine.VR;
#if UNITY_WSA
using UnityEngine.VR.WSA.Input;
#endif
#endif

public class PlayerVRCharacter : NetworkBehaviour
{
    public bool disableMove = false;
    public bool draggingObject = false;
    private float speedRiseFall = .05f;
    public bool forwardtick = false;
    MixedRealityTeleport TeleportComponent;
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
            return;
        }
        GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.green);
        TeleportComponent = transform.Find("MixedRealityCameraParent").gameObject.GetComponent<MixedRealityTeleport>();

    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>())
            if (Input.GetButton("MC_LEFT_GRIP") || Input.GetButton("MC_RIGHT_GRIP"))
            {
                if (!TeleportComponent.EnableTeleport)
                {
                    transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableStrafe = false;
                    transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableRotation = false;
                }
                    
            }
            else
            {
                if (!TeleportComponent.EnableTeleport)
                {
                    if (!forwardtick && (Input.GetAxis("MC_LEFT_STICK_VERTICAL") > .19 || Input.GetAxis("MC_RIGHT_STICK_VERTICAL") > .19))
                    {
                        forwardtick = true;
                    }
                    if (forwardtick)
                    {
                        transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableStrafe = false;
                        transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableRotation = false;
                    }
                    else
                    {

                        transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableStrafe = true;
                        transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableRotation = true;
                    }
                }
                else { forwardtick = false; }
            }
        
        var x = (Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") * Time.deltaTime * 3.0f)+ (Input.GetAxis("MC_RIGHT_STICK_HORIZONTAL") * Time.deltaTime * 3.0f);
        var z = (Input.GetAxis("MC_LEFT_STICK_VERTICAL") * Time.deltaTime * 3.0f) + (Input.GetAxis("MC_RIGHT_STICK_VERTICAL") * Time.deltaTime * 3.0f);
        if (!TeleportComponent.EnableTeleport)
        {
            if (Input.GetButton("MC_LEFT_GRIP")|| Input.GetButton("MC_RIGHT_GRIP"))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + z, transform.localPosition.z);
            }
            else
            {
                if (!disableMove && forwardtick)
                    transform.Translate(new Vector3(0, 0, z), CameraCache.Main.transform);
            }
        }
        
        if (!disableMove && forwardtick && !TeleportComponent.EnableTeleport)
        {
            //Transform transformToRotate = Camera.main.transform;
            transform.Translate(new Vector3(x, 0, 0), CameraCache.Main.transform);
            if (Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") <= .19 && Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") >= -.19
                && Input.GetAxis("MC_LEFT_STICK_VERTICAL") <= .19 && Input.GetAxis("MC_LEFT_STICK_VERTICAL") >= -.19
                && Input.GetAxis("MC_RIGHT_STICK_HORIZONTAL") <= .19 && Input.GetAxis("MC_RIGHT_STICK_HORIZONTAL") >= -.19
                && Input.GetAxis("MC_RIGHT_STICK_VERTICAL") <= .19 && Input.GetAxis("MC_RIGHT_STICK_VERTICAL") >= -.19
                )
            {
                forwardtick = false;
            }
            /*if (Input.GetButton("XBOX_LEFT_BUMPER"))
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (speedRiseFall), transform.localPosition.z);
            }*/
        }

    }
    
}
