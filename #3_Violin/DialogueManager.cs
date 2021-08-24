using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Vector3[] origin;
    public GameObject[] performers;
    public Animator[] anims;
    public GameObject conductor;
    public Animator c_anim;
    public Transform target;
    
    public GameObject talkButton;
    public GameObject tutorialButton;
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    private Queue<string> sentences;

    void Awake()
    {
        talkButton.SetActive(false);
        dialogueBox.SetActive(false);
        StartCoroutine(Hello());
        StartCoroutine(Talk());
    }

    void Update()
    {
        
    }

    IEnumerator Talk() {
        c_anim.SetTrigger("doTalk");

        yield return new WaitForSeconds(4f);
        dialogueBox.GetComponent<BoxCollider>().enabled = false;
        nameText.text = "지휘자";
        dialogueText.text = "지휘자가 말을 걸어온다. 대화하시겠습니까?";
        talkButton.SetActive(true);
        dialogueBox.SetActive(true);
        animator.SetBool("isOpen", true);
    }

    IEnumerator Hello() {
        foreach (GameObject performer in performers) {
            //originPosition[i] = performer.transform;
            performer.transform.LookAt(target.position);
            //Vector3 lookvec = target.transform.position - performer.transform.position;
            //performer.transform.rotation = Quaternion.Euler(lookvec);
            //performer.transform.rotation = Quaternion.LookRotation(lookvec).normalized;
            //anims[0].SetTrigger("doHello");
            //yield return new WaitForSeconds(1f);
            //performer.transform.rotation = Quaternion.Euler(lookvec);
        }

        foreach (Animator anim in anims) {
            anim.SetTrigger("doHello");
        }

        yield return new WaitForSeconds(3.5f);

        int i = 0;
       foreach (GameObject performer in performers) {
           //performer.transform.position = target.position + offset[j];
            performer.transform.rotation = Quaternion.Euler(origin[i]);
            i++;
        }

        yield return new WaitForSeconds(4f);
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue) {        
        //animator.SetBool("isOpen", true);
        talkButton.SetActive(false);
        dialogueBox.GetComponent<BoxCollider>().enabled = true;
        nameText.text = dialogue.name;
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        Debug.Log(sentence);
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        tutorialButton.SetActive(true);
    }
}
