using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BassPedal : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean pedalAction;
    public Animator anim;
    
    void Awake()
    {
    }

    void Update()
    {
        if(GetTriggerDown()) {
            anim.SetTrigger("doPedal");
        }
    }

    public bool GetTriggerDown() {
        return pedalAction.GetStateDown(handType);
    }
}
