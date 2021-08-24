using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerViolin;
    public GameObject playerBow;
    public Animator anim;
    public GameObject showInst;
    public Text tutorialText;
    public Text songText;
    public GameObject cameraRig;
    public Animator cam_anim;

    public GameObject isTutorial;
    public GameObject a, b, c;

    public AudioSource clap;
    public AudioSource spring;
    
    void Awake() {
        tutorialText.text = "";
    }

    void Start() {

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            GameStart();
        }
    }
    public void Tutorial(GameObject Button) {
        Button.SetActive(false);
        showInst.SetActive(true);
        
        tutorialText.text = "악기를 잡고 지판을 클릭해봅니다.";
    }

    public void TutorialStart() {
        showInst.SetActive(false);
        playerViolin.SetActive(true);
        playerBow.SetActive(true);
        //anim.SetTrigger("doPrepare");
        StartCoroutine(TutorialCount());
        //cam_anim.SetTrigger("doPlay");
        // mainCamera.enabled = false;
        // violinCamera.enabled = true;
    }
    
    IEnumerator TutorialCount() {
        tutorialText.text = "시작됩니다.";
        
        yield return new WaitForSeconds(1.5f);

        tutorialText.text = "3";
        yield return new WaitForSeconds(1f);
        tutorialText.text = "2";
        yield return new WaitForSeconds(1f);
        tutorialText.text = "1";
        yield return new WaitForSeconds(1f);
        tutorialText.text = "Go!";
        yield return new WaitForSeconds(1f);
        //anim.SetTrigger("doPlay");
        yield return new WaitForSeconds(0.1f);
        tutorialText.text = "지판을 클릭해보세요.";
        a.SetActive(true);
        b.SetActive(true);
        c.SetActive(true);
        isTutorial.SetActive(true);
    }
    
    public void GameStart() {
        playerViolin.SetActive(true);
        playerBow.SetActive(true);
        //anim.SetTrigger("doPrepare");
        StartCoroutine(StartScenario());
        //cam_anim.SetTrigger("doStart");
        
    }

    IEnumerator StartScenario() {
        songText.text = "";
        tutorialText.text = "무대 위로 이동했습니다!";
        yield return new WaitForSeconds(2f);
        clap.volume = 0.5f;
        clap.Play();
        yield return new WaitForSeconds(3.5f);
        
        tutorialText.text = "시작됩니다.";
        
        yield return new WaitForSeconds(1.5f);

        tutorialText.text = "3";
        yield return new WaitForSeconds(1f);
        tutorialText.text = "2";
        yield return new WaitForSeconds(1f);
        tutorialText.text = "1";
        yield return new WaitForSeconds(1f);
        tutorialText.text = "Go!";
        yield return new WaitForSeconds(1f);
        //anim.SetTrigger("doPlay");
        yield return new WaitForSeconds(0.1f);

        songText.text = "~~ 비발디 사계 '봄' 1악장 ~~";
        tutorialText.text = "지판을 클릭해보세요.";
        a.SetActive(true);
        b.SetActive(true);
        c.SetActive(true);
        
        spring.Play();

        yield return new WaitForSeconds(10f);
        cam_anim.SetTrigger("doBack");

        yield return new WaitForSeconds(0.5f);
        clap.volume = 1f;
        clap.Play();
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("doBow");
        songText.text = "~~ 공연 종료 ~~";
        tutorialText.text = "";
    }
}
