using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerCharacterControl : NetworkBehaviour
{
    private float speedRiseFall = .05f;
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
            return;
        }
        GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.green);
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        if (Input.GetButton("XBOX_RIGHT_BUMPER"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (speedRiseFall), transform.localPosition.z);
        }
        if (Input.GetButton("XBOX_LEFT_BUMPER"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (speedRiseFall), transform.localPosition.z);
        }
        //transform.Rotate(0, x, 0);
        transform.Translate(x, 0, 0);
        transform.Translate(0, 0, z);
    }
}
