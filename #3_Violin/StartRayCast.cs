using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartRayCast : MonoBehaviour
{
    public MeshRenderer a,b,c;
    public Camera cam;
    public Text tutorialText;
    void Update()
    {
        StartCoroutine(click());
    }

    IEnumerator click() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.gameObject);
                Debug.Log(hit.collider.tag);
                if(hit.collider.tag == "areaA")
                {
                    // StopCoroutine(pop());
                    a.material.color = new Color(1,1,1,0.3f);
                    tutorialText.text = "좋아요. 훌륭하네요.";
                    // StartCoroutine(pop());
                }

                else if(hit.collider.tag == "areaB")
                {
                    // StopCoroutine(pop());
                    b.material.color = new Color(1,1,1,0.3f);
                    tutorialText.text = "좋아요. 훌륭하네요.";
                    // StartCoroutine(pop());
                }
                else if (hit.collider.tag == "areaC")
                {
                    // StopCoroutine(pop());
                    c.material.color = new Color(1,1,1,0.3f);
                    tutorialText.text = "좋아요. 훌륭하네요.";
                    // StartCoroutine(pop());
                }

                else if (hit.collider.tag == "Untagged")
                {
                    tutorialText.text = "관객들의 박수 소리가 더욱 커질 수 있도록 지판을 클릭해보세요.";
                }
                yield return new WaitForSeconds(0.5f);
                a.material.color = new Color(255f/255f,165f/255f,165f/255f,0.3f);
                b.material.color = new Color(255f/255f,253f/255f,162f/255f,0.3f);
                c.material.color = new Color(165f/255f,255f/255f,150/255f,0.3f);
                tutorialText.text = "";
            }
        }
    }

    // IEnumerator pop() {
    //     yield return new WaitForSeconds(3f);
    //     tutorialText.text = "관객들의 박수 소리가 더욱 커질 수 있도록 지판을 클릭해보세요.";
    // }
}
