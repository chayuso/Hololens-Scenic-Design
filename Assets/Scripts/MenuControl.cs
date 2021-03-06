﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MenuControl : MonoBehaviour {
    NetworkManager NM;
    NetworkManager manager;
    ConnectionConfig CC;
	// Use this for initialization
	void Start () {
        NM = gameObject.GetComponent<NetworkManager>();
        CC = new ConnectionConfig();
        manager = NM;
        NM.StartHost();
    }
	
	// Update is called once per frame
	void Update () {
        //Script From
        //https://forum.unity.com/threads/networkmanagerhud-source.333482/
        if (!NetworkClient.active && !NetworkServer.active)
        {
            if (NM.matchMaker == null)
            {
                if (Input.GetButtonDown("XBOX_X"))
                {
                    NM.StartMatchMaker();
                }
                if (Input.GetButtonDown("MC_LEFT_TRIGGER_BUTTON")|| Input.GetButtonDown("MC_RIGHT_TRIGGER_BUTTON"))
                {
                    NM.StartHost();
                }
            }
            else
            {
                if (NM.matchInfo == null)
                {
                    if (NM.matches == null)
                    {
                        if (Input.GetButtonDown("XBOX_X"))
                        {
                            NM.matchMaker.CreateMatch(NM.matchName, NM.matchSize,true,"","","",0,0, manager.OnMatchCreate);
                        }
                        if (Input.GetButtonDown("XBOX_Y"))
                        {
                            manager.matchMaker.ListMatches(0, 20, "", false, 0, 0, manager.OnMatchList);
                        }
                        

                    }
                    else
                    {
                        foreach (var match in manager.matches)
                        {
                            if (Input.GetButtonDown("XBOX_A") && match.name==manager.matchName)//Change this later//Only for testing
                            {
                                manager.matchSize = (uint)match.currentSize;
                                manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
                            }
                        }
                    }
                }
            }
            
        }
            
    }
}
