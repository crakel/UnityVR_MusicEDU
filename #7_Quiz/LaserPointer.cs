using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean laserPointer;
    public SteamVR_Action_Boolean Trigger;

    public bool on = false;
    public Color color;
    public float thickness = 0.002f;
    public Color clickColor = Color.green;
    GameObject laser;
    Transform btn;

    bool playFlag = false;

    public AudioSource marry, turkey, pomp;
    public AudioSource figaro, eine, bichang;

    private void OnEnable()
    {
        laserPointer.AddOnStateDownListener(ToggleLaserPointer, handType);
        Trigger.AddOnStateDownListener(ToggleTrigger, handType);
    }

    private void ToggleLaserPointer(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        on = !on;
    }

    private void ToggleTrigger(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        StartCoroutine(TriggerColor());
    }

    IEnumerator TriggerColor() {
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));

        newMaterial.SetColor("_Color", clickColor);
        laser.GetComponent<MeshRenderer>().material = newMaterial;

        yield return new WaitForSeconds(0.3f);

        newMaterial.SetColor("_Color", color);
        laser.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void Start()
    {
        marry.time = marry.clip.length * 0.1f;
        turkey.time = turkey.clip.length * 0.5f;
        pomp.time = pomp.clip.length * 0.5f;
        
        figaro.time = figaro.clip.length * 0.5f;
        eine.time = eine.clip.length * 0.5f;
        //bichang.time = bichang.clip.length * 0.5f;

        laser = GameObject.CreatePrimitive(PrimitiveType.Cube);
        laser.transform.parent = gameObject.transform;
        laser.transform.localScale = new Vector3(thickness, thickness, 100f);
        laser.transform.localPosition = new Vector3(0f, 0f, 50f);
        laser.transform.localRotation = Quaternion.identity;

        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        laser.GetComponent<MeshRenderer>().material = newMaterial;
        laser.layer = 2; //Add laser to ignore raycast layer
    }

    void Update()
    {
        if (on)
        {
            laser.SetActive(true);
            lasercast();
            //StartCoroutine(lasercast());
        }
        else
        {
            laser.SetActive(false);
        }
    }
        
        void lasercast() {
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.gameObject.name);
                Debug.Log(hit.transform.gameObject);

                if (hit.collider.tag == "Button")
                {
                    btn = hit.transform;
                    onButton(btn);
                    
                    if(Trigger.GetState(handType)) {
                        hit.transform.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                }
                
                // else
                // {
                //     Debug.Log("btn = " + btn);
                //     if(btn != null)
                //     {
                //         onButtonUp(btn);
                //     }
                // }
            }

            else
            {
                Debug.Log("btn = " + btn);
                if(btn != null)
                {
                    onButtonUp(btn);
                }
                laser.transform.localScale = new Vector3(thickness, thickness, 100f);
                laser.transform.localPosition = new Vector3(0f, 0f, 50f);
            }
    }

    
    void onButton(Transform btn) {
        Debug.Log("onButton");
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.green;
        btn.GetComponent<Button>().colors = cb;

        Debug.Log("btn parent name : " + btn.parent.name);
        if (playFlag) {
            return;
        }

        if (btn.parent.name == "marry") {
            marry.Play();
        }
        
        else if (btn.parent.name == "turkey") {
            turkey.Play();
        }

        else if (btn.parent.name == "pomp") {
            pomp.Play();
        }

        else if (btn.parent.name == "figaro") {
            figaro.Play();
        }

        else if (btn.parent.name == "eine") {
            eine.Play();
        }

        else if (btn.parent.name == "bichang") {
            bichang.Play();
        }
        playFlag = true;
    }

    void onButtonUp(Transform btn) {
        Debug.Log("onButtonUp");
        ColorBlock cb = btn.GetComponent<Button>().colors;
        cb.normalColor = Color.white;
        btn.GetComponent<Button>().colors = cb;

        if (btn.parent.name == "marry") {
            marry.Stop();
        }
        
        else if (btn.parent.name == "turkey") {
            turkey.Stop();
        }

        else if (btn.parent.name == "pomp") {
            pomp.Stop();
        }

        else if (btn.parent.name == "figaro") {
            figaro.Stop();
        }

        else if (btn.parent.name == "eine") {
            eine.Stop();
        }

        else if (btn.parent.name == "bichang") {
            bichang.Stop();
        }

        playFlag = false;
        // turkey.Stop();
        // pomp.Stop();
    }
}