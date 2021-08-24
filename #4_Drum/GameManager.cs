using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text alertText;
    public Vector3 GamePosition;
    public GameObject rig;
    public GameObject arrow;
    public GameObject controller_l, controller_r;
    public GameObject drumstick_l, drumstick_r;
    public GameObject model_l, model_r;
    public AudioSource playlist;
    public AudioSource clap;

    //public GameObject bass, floor, snare, smalltom, largetom, highhat, cymbal, crash;
    public GameObject[] effects;

    public string hit;
    
    void Start() {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(Concert());
        }
        else
        {
            arrow.SetActive(false);
            controller_l.SetActive(false);
            controller_r.SetActive(false);
            alertText.text = "";
            StartCoroutine(StartDialogue());
        }
    }

    void Update() {
        Debug.Log(hit);
    }

    IEnumerator StartDialogue() {
        alertText.text = "밴드에서 드러머를 구인하고있네요.";
        yield return new WaitForSeconds(3f);
        alertText.text = "콘트롤러의 포인터를 이용해 지원하세요.";
        arrow.SetActive(true);
        controller_l.SetActive(true);
        controller_r.SetActive(true);
    }

    public void GameStart() {
        alertText.text = "";
        Debug.Log("게임시작");
        //rig.transform.position = new Vector3(-5.12f, 0.103f, -6.04f);
        rig.transform.position = GamePosition;
        rig.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        
        UnityEngine.XR.InputTracking.disablePositionalTracking = false;

        arrow.SetActive(false);

        model_l.SetActive(false);
        model_r.SetActive(false);
        
        drumstick_l.SetActive(true);
        drumstick_r.SetActive(true);

        StartCoroutine(GameScenario());
    }

    IEnumerator GameScenario() {
        yield return new WaitForSeconds(2f);

        alertText.text = "콘트롤러로 타이밍에 맞추어 표시되는 드럼을 클릭합니다.";
        
        yield return new WaitForSeconds(2f);

        alertText.text = "시작됩니다.";
        yield return new WaitForSeconds(1.5f);

        alertText.text = "3";
        yield return new WaitForSeconds(1f);
        alertText.text = "2";
        yield return new WaitForSeconds(1f);
        alertText.text = "1";
        yield return new WaitForSeconds(1f);
        alertText.text = "Go!";
        yield return new WaitForSeconds(1f);
        
        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial() {
        alertText.text = "베이스 드럼을 연주해보세요";
        yield return new WaitUntil(() => hitCheck("Bass Drum"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);

        alertText.text = "플로어 탐을 연주해보세요";
        yield return new WaitUntil(() => hitCheck("Floor Tom"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);

        alertText.text = "스네어를 연주해보세요";
        yield return new WaitUntil(() => hitCheck("Snare Drum"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);
        
        alertText.text = "작은 탐탐을 연주해보세요";
        yield return new WaitUntil(() => hitCheck("Rack Tom Small"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);

        alertText.text = "큰 탐탐을 연주해보세요";
        yield return new WaitUntil(() => hitCheck("Rack Tom Large"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);

        alertText.text = "하이 햇 심벌을 연주해보세요";
        yield return new WaitUntil(() => hitCheck("High Hat"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);

        alertText.text = "라이드 심벌을 연주해보세요";
        yield return new WaitUntil(() => hitCheck("Cymbal"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);

        alertText.text = "크래시 심벌를 연주해보세요";
        yield return new WaitUntil(() => hitCheck("Crash"));
        alertText.text = "좋아요. 훌륭하네요.";
        yield return new WaitForSeconds(0.5f);
        alertText.text = "";
        yield return new WaitForSeconds(0.5f);

        alertText.text = "3초 뒤 공연장으로 이동합니다!";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
    
    IEnumerator Concert() {
        alertText.text =  "공연장으로 이동했습니다!";
        clap.Play();
        yield return new WaitForSeconds(2f);
        alertText.text = "음악에 맞춰 자유롭게 드럼을 연주해보세요.";
        yield return new WaitForSeconds(2f);
        
        alertText.text = "시작됩니다.";
        model_l.SetActive(false);
        model_r.SetActive(false);
        drumstick_l.SetActive(true);
        drumstick_r.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        
        alertText.text = "3";
        yield return new WaitForSeconds(1f);
        alertText.text = "2";
        yield return new WaitForSeconds(1f);
        alertText.text = "1";
        yield return new WaitForSeconds(1f);

        alertText.text = "아이유 - 있잖아 (Rock ver.)";
        playlist.Play();
    }

    bool hitCheck(string part) {
        //GameObject temp = GameObject.Find(part);
        for (int i = 0; i<=7; i++) {
            if(effects[i].transform.parent.name == part) {
                effects[i].SetActive(true);

                if (hit == part) {
                    effects[i].SetActive(false);
                    return true;
                }
                else {
                    //alertText.text = "다시 한번 해볼까요?";
                    return false;
                }
            }
        }
        return false;
    }

    
}
