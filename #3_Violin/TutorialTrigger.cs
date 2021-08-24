using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public void OnMouseDown() {
        Debug.Log("클릭");
        TriggerTutorial();
    }
    public void TriggerTutorial () {
        FindObjectOfType<GameManager>().TutorialStart();
    }
}
