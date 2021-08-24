using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    public MeshRenderer a,b,c;
    public Camera cam;

    public Text tutorialText;
    public GameObject startButton;
    public GameObject isTutorial;

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean laserPointer;

    public bool on = false;
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

                if (hit.collider.tag == "Inst")
                {
                    FindObjectOfType<GameManager>().TutorialStart();
                }

                if (hit.collider.tag == "Button")
                {
                    hit.transform.gameObject.GetComponent<Button>().onClick.Invoke();
                }

                if (isTutorial.activeSelf)
                {
                    laser.transform.localScale = new Vector3(thickness, thickness, hit.distance);
                    laser.transform.localPosition = new Vector3(0f, 0f, hit.distance / 2f);
                    Debug.Log(hit.collider.tag);
                    if(hit.collider.tag == "areaA")
                    {
                        a.material.color = new Color(1,1,1,0.3f);
                        tutorialText.text = "좋아요. 훌륭하네요.";
                        startButton.SetActive(true);
                    }

                    else if(hit.collider.tag == "areaB")
                    {
                        b.material.color = new Color(1,1,1,0.3f);
                        tutorialText.text = "좋아요. 훌륭하네요.";
                        startButton.SetActive(true);
                    }
                    else if (hit.collider.tag == "areaC")
                    {
                        c.material.color = new Color(1,1,1,0.3f);
                        tutorialText.text = "좋아요. 훌륭하네요.";
                        startButton.SetActive(true);
                    }

                    else if (hit.collider.tag == "Untagged")
                    {
                        yield return new WaitForSeconds(0.1f);
                        tutorialText.text = "다시 한번 해볼까요?";
                    }
                    yield return new WaitForSeconds(0.5f);
                    tutorialText.text = "지판을 클릭해보세요.";
                    a.material.color = new Color(255f/255f,165f/255f,165f/255f,0.3f);
                    b.material.color = new Color(255f/255f,253f/255f,162f/255f,0.3f);
                    c.material.color = new Color(165f/255f,255f/255f,150/255f,0.3f);
                }

            }

            else
            {
                laser.transform.localScale = new Vector3(thickness, thickness, 100f);
                laser.transform.localPosition = new Vector3(0f, 0f, 50f);
            }   
    }
}