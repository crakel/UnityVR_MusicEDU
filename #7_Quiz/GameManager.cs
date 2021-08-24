using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

// 패드누르면 레이저 발사
// 트리거 누르면 레이저 없애기 토글 

public class GameManager : MonoBehaviour
{
    int curIndex = 0;
    bool endFlag = false;
    public GameObject[] Quiz;
    public Text[] QuizText;
    public string[] QuizString;
    public GameObject QuizButton;

    public GameObject MusicUI;

    public GameObject playerBox;
    public GameObject mozartBox;
    Text playerText;
    Text mozartText;
    string subText;

    public AudioSource[] Musics;
    public Text MusicText;

    public Animator mozartAnim;

    bool musicFlag = false;

    public GameObject ExitButton;


    void Awake() {
        playerText = playerBox.GetComponentInChildren<Text>();
        mozartText = mozartBox.GetComponentInChildren<Text>();
    }

    void Start() {
        mozartAnim.SetTrigger("doPiano");
        StartCoroutine(Intro());
    }

    void Update() {
        if (musicFlag) {
            if (!Musics[0].isPlaying) {
                Musics[1].Play();        
                MusicText.text = "~ Piano Sonata No. 8 in A minor," +
                        System.Environment.NewLine + 
                        "K. 310 1st movement :" +
                        System.Environment.NewLine + 
                        "Allegro maestoso ~";
            }

            if (!Musics[0].isPlaying && !Musics[1].isPlaying) {
                Musics[2].Play();
                MusicText.text = "~ Piano Sonata No. 11 in A major" +
                        System.Environment.NewLine + 
                        "'Alla Turca', K. 331 3rd movement :" +
                        System.Environment.NewLine + 
                        "Rondo Alla turca. Allegretto ~";
                
            }

            if (!Musics[0].isPlaying && !Musics[1].isPlaying && !Musics[2].isPlaying) {
                Musics[3].Play();
                MusicText.text = "~ 12 Variationen in C " +
                        System.Environment.NewLine + 
                        "über das französische Lied :" +
                        System.Environment.NewLine + 
                        "Ah, vous dirai-je maman K.265 ~";
            }
        }
    }

    IEnumerator Intro() {
        yield return new WaitForSeconds(2f);

        mozartBox.SetActive(true);
        StartCoroutine(TypingAction("역시 나야! 모차르트!!" + System.Environment.NewLine + "방금 악상이 떠올랐는데 이리 와서 들어볼래?" + System.Environment.NewLine + "내가 만들었지만 이건 너무 아름다운 음악이야. ", mozartText));
        // mozartText.text = "역시 나야! 모차르트!! 방금 악상이 떠올랐는데 이리 와서 들어볼래? 내가 만들었지만 이건 너무 아름다운 음악이야.";
        yield return new WaitForSeconds(8f);
        mozartBox.SetActive(false);
        mozartText.text = "";

        yield return new WaitForSeconds(1.5f);
        
        playerBox.SetActive(true);
        StartCoroutine(TypingAction("기대돼요! ", playerText));
        // playerText.text = "기대돼요!";
        yield return new WaitForSeconds(3f);
        playerBox.SetActive(false);
        playerText.text = "";
        
        yield return new WaitForSeconds(1.5f);

        mozartBox.SetActive(true);
        mozartAnim.SetTrigger("doTalk");
        StartCoroutine(TypingAction("어쩜 이렇게 매 순간 번쩍이는 아이디어가" + System.Environment.NewLine + "나오는 건지 내가 생각해도 나는 정말.. ", mozartText));
        // mozartText.text = "어쩜 이렇게 매 순간 번쩍이는 아이디어가 나오는 건지 내가 생각해도 나는 정말..";
        yield return new WaitForSeconds(6f);
        mozartBox.SetActive(false);
        mozartText.text = "";

        yield return new WaitForSeconds(1.5f);
        
        playerBox.SetActive(true);
        StartCoroutine(TypingAction("멋져요! ", playerText));
        // playerText.text = "멋져요!";
        yield return new WaitForSeconds(3f);
        playerBox.SetActive(false);
        playerText.text = "";

        yield return new WaitForSeconds(1.5f);

        mozartBox.SetActive(true);
        mozartAnim.SetTrigger("doTalk");
        StartCoroutine(TypingAction("내 음악을 듣고 싶다면 나에 대한 퀴즈를 맞춰봐." + System.Environment.NewLine + "그럼 내가 멋있게 연주해주지. ", mozartText));
        // mozartText.text = "내 음악을 듣고 싶다면 나에 대한 퀴즈를 맞춰봐. 그럼 내가 멋있게 연주해주지.";
        yield return new WaitForSeconds(6f);
        mozartBox.SetActive(false);
        mozartText.text = "";

        QuizButton.SetActive(true);
    }

    public void QuizStart(int index) {
        if(index == 0) {
            QuizButton.SetActive(false);
        }
        
        Quiz[index].SetActive(true);
        StartCoroutine(TypingAction(QuizText[index].text, QuizText[index]));
        mozartAnim.SetTrigger("doTalk");

        if (index == 6) {
            StartCoroutine(EndScenario());
        }
    }

    public void Correct() {
        StartCoroutine(CorrectScenario());
    }
    
    public IEnumerator CorrectScenario() {
        if (!endFlag) {
            endFlag = true;
            mozartBox.SetActive(true);
            mozartAnim.SetTrigger("doTalk");
            mozartText.text = "역시 나에 대해 잘 알고 있네.";
            Quiz[curIndex].SetActive(false);
            yield return new WaitForSeconds(2f);
            mozartBox.SetActive(false);

            yield return new WaitForSeconds(2f);
            
            if(curIndex < 6) {
                curIndex++;
                QuizStart(curIndex);
            }
            endFlag = false;
        }
    }

    public void Wrong() {
        StartCoroutine(WrongScenario());
    }

    public IEnumerator WrongScenario() {
        if (!endFlag) {
            endFlag = true;
            mozartBox.SetActive(true);
            mozartAnim.SetTrigger("doTalk");
            mozartText.text = "그럴 리가 없는데.. 한번 다시 해볼래?";
            yield return new WaitForSeconds(2f);
            mozartBox.SetActive(false);
            endFlag = false;
        }
    }

    IEnumerator TypingAction(string originText, Text targetText){
        for(int i = 0; i< originText.Length; i++){

           	yield return new WaitForSeconds(0.08f);

            subText += originText.Substring(0,i);
            targetText.text = subText;
            subText= "";
        }
    }

    IEnumerator EndScenario() {

        yield return new WaitForSeconds(6f);

        mozartBox.SetActive(true);
        mozartAnim.SetTrigger("doTalk");
        StartCoroutine(TypingAction("내가 특별히 문제를 다 맞춘 것에 대한 선물로 연주를 들려줄게." + System.Environment.NewLine + "여기 와서 앉아. ", mozartText));
        yield return new WaitForSeconds(6f);
        mozartBox.SetActive(false);
        mozartText.text = "";
        
        yield return new WaitForSeconds(3f);
        
        ExitButton.SetActive(true);

        mozartAnim.SetTrigger("doPiano");
        MusicUI.SetActive(true);
    }
}

