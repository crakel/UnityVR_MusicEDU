using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject playBtn, pauseBtn, stopBtn, subBtn, exitButton;
    public GameObject musicBtn1, musicBtn2, musicBtn3, musicBtn4;
    public GameObject subON, subOFF;
    public GameObject MusicText;
    Text MusicInfo;

    public AudioSource[] Musics;

    public AudioSource currentPlay;

    void Awake() {
        MusicInfo = MusicText.gameObject.GetComponent<Text>();
    }

    public void TriggerWrong()
    {
        FindObjectOfType<GameManager>().Wrong();
    }

    public void TriggerCorrect()
    {
        FindObjectOfType<GameManager>().Correct();
    }

    public void TriggerQuiz(int index)
    {
        FindObjectOfType<GameManager>().QuizStart(index);
    }

    public void Pause(AudioSource current) {
        current.Pause();
        currentPlay.Pause();
        pauseBtn.SetActive(false);
        playBtn.SetActive(true);
    }

    public void Play(AudioSource current) {
        current.Play();
        currentPlay.Play();
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
    }

    public void Stop(AudioSource current) {
        current.Stop();
        currentPlay.Stop();
        playBtn.SetActive(false);
        pauseBtn.SetActive(false);
        stopBtn.SetActive(false);
        subBtn.SetActive(false);
        subOFF.SetActive(false);

        musicBtn1.SetActive(true);
        musicBtn2.SetActive(true);
        musicBtn3.SetActive(true);
        musicBtn4.SetActive(true);
        MusicText.SetActive(false);
        exitButton.SetActive(true);
    }

    public void MusicEnter(int index) {
        pauseBtn.SetActive(true);
        stopBtn.SetActive(true);
        subOFF.SetActive(true);
        exitButton.SetActive(false);
        musicBtn1.SetActive(false);
        musicBtn2.SetActive(false);
        musicBtn3.SetActive(false);
        musicBtn4.SetActive(false);
        MusicText.SetActive(true);

        currentPlay = Musics[index];
        currentPlay.Play();
        
        if (index == 0) {
            MusicInfo.text = "~ Piano Sonata No. 5 in G major," +
                    System.Environment.NewLine + 
                    "K. 1st movement :" +
                    System.Environment.NewLine + 
                    "Allegro ~";
        }

        else if (index == 1) {
            MusicInfo.text = "~ Piano Sonata No. 8 in A minor," +
                        System.Environment.NewLine + 
                        "K. 310 1st movement :" +
                        System.Environment.NewLine + 
                        "Allegro maestoso ~";
        }

        else if (index == 2) {
            MusicInfo.text = "~ Piano Sonata No. 11 in A major" +
                        System.Environment.NewLine + 
                        "'Alla Turca', K. 331 3rd movement :" +
                        System.Environment.NewLine + 
                        "Rondo Alla turca. Allegretto ~";
        }

        else if (index == 3) {
            MusicInfo.text = "~ 12 Variationen in C " +
                        System.Environment.NewLine + 
                        "über das französische Lied :" +
                        System.Environment.NewLine + 
                        "Ah, vous dirai-je maman K.265 ~";
        }
    }

    public void SubON() {
        subON.SetActive(false);
        subOFF.SetActive(true);
        MusicText.SetActive(true);
    }

    public void SubOFF() {
        subOFF.SetActive(false);
        subON.SetActive(true);
        MusicText.SetActive(false);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
