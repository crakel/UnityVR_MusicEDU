using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class PlaySound : MonoBehaviour
{
    private AudioSource source;
    public bool playOnButtonPress = false;
    public string button;
    public string part;
    public HandRole handRoleL = HandRole.LeftHand;
    public HandRole handRoleR = HandRole.RightHand;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(playOnButtonPress)
        {
            CheckButtonPress();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DrumStickHead")
        {
            if (other.name == "HeadColiderL")
            {
                ViveInput.TriggerHapticVibrationEx<HandRole>(handRoleL, 1, 60, 1, 0);
            }   

            else if (other.name == "HeadColiderR")
            {
                ViveInput.TriggerHapticVibrationEx<HandRole>(handRoleR, 1, 60, 1, 0);
            }

            else if (other.name == "HeadCollider")
            {
                ViveInput.TriggerHapticVibrationEx<HandRole>(handRoleL, 1, 60, 1, 0);
                ViveInput.TriggerHapticVibrationEx<HandRole>(handRoleR, 1, 60, 1, 0);
            }

            // 칠 때 마다 약간씩 다른 소리
            //source.pitch = Random.RandomRange(0.8f, 1.2f);
            // 강도에 따라 다른 볼륨
            source.volume = other.gameObject.GetComponent<TrackSpeed>().speed;
            if (other.name == "HeadCollider") {
                source.volume = 1f;
            }
            source.Play();
            GameObject.Find("GameManager").GetComponent<GameManager>().hit = gameObject.transform.parent.name;
        }
    }

    void CheckButtonPress()
    {  
        
    }
}
