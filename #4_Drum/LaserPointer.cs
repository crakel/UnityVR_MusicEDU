using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{

    public Text alertText;
    public GameObject startButton;
    public GameObject effect;

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean laserPointer;

    public bool on = false;
    public bool pointing = false;
    public Color color;
    public float thickness = 0.002f;
    public Color clickColor = Color.green;
    GameObject laser;

    private void OnEnable()
    {
        laserPointer.AddOnStateDownListener(ToggleLaserPointer, handType);
    }

    private void ToggleLaserPointer(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        on = !on;
    }

    void Start()
    {
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
        }
        else
        {
            laser.SetActive(false);
        }

        if (on)
        {
            StartCoroutine(lasercast());
            on = false;
        }
    }

    IEnumerator lasercast() {
            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.gameObject.name);
                Debug.Log(hit.transform.gameObject);

                if (hit.collider.tag == "GetDrum")
                {
                    alertText.text = "밴드의 드러머가 되었습니다!";
                    effect.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    FindObjectOfType<GameManager>().GameStart();
                }
            }

            else
            {
                laser.transform.localScale = new Vector3(thickness, thickness, 100f);
                laser.transform.localPosition = new Vector3(0f, 0f, 50f);
            } 
            yield return null;
    }

}