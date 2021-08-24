using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Raycast : MonoBehaviour
{
    public MeshRenderer a,b,c;
    public Camera cam;

    public Text tutorialText;
    public GameObject startButton;

    void Awake() 
    {
        // a = GameObject.FindWithTag("areaA").GetComponent<MeshRenderer>();
        // b = GameObject.FindWithTag("areaB").GetComponent<MeshRenderer>();
        // c = GameObject.FindWithTag("areaC").GetComponent<MeshRenderer>();
    }
    void Update()
    {
        StartCoroutine(click());
    }

    IEnumerator click() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && tutorialText.text == "지판을 클릭해보세요.")
            {
                //Debug.Log(hit.transform.gameObject);
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
    }
}
