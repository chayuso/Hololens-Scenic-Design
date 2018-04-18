using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
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
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
            return;
        }
        GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.green);

    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetButton("MC_LEFT_GRIP"))
        {
            transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableStrafe = false;
            transform.Find("MixedRealityCameraParent").GetComponent<MixedRealityTeleport>().EnableRotation = false;
        }
        else
        {
            if (!forwardtick && Input.GetAxis("MC_LEFT_STICK_VERTICAL") > .19)
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
        
        var x = Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") * Time.deltaTime * 3.0f;
        var z = Input.GetAxis("MC_LEFT_STICK_VERTICAL") * Time.deltaTime * 3.0f;
        if (Input.GetButton("MC_LEFT_GRIP"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + z, transform.localPosition.z);
        }
        else
        {
            if (!disableMove && forwardtick)
                transform.Translate(new Vector3(0, 0, z), transform.Find("MixedRealityCameraParent").Find("PlaneStabilizer").transform);
        }
        if (!disableMove && forwardtick)
        {
            
            transform.Translate(new Vector3(x, 0, 0), transform.Find("MixedRealityCameraParent").Find("PlaneStabilizer").transform);
            if (Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") <= .19 && Input.GetAxis("MC_LEFT_STICK_HORIZONTAL") >= -.19
                && Input.GetAxis("MC_LEFT_STICK_VERTICAL") <= .19 && Input.GetAxis("MC_LEFT_STICK_VERTICAL") >= -.19
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
