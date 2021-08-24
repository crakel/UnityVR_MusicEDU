using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TypingClass : MonoBehaviour {

	// Showing UI text
	public Text DialogText;
	
    //Main Text
    string originText;
    
    //SubString Variable
	string subText;


	private void start(){

		 originText = GetComponent<Text>().text;
         
         StartCoroutine("TypingAction");

	}





  // Used Corutine
  IEnumerator TypingAction(){
            for(int i = 0; i< originText.Length; i++){

              	yield return new WaitForSeconds(0.1f);

              subText += originText.Substring(0,i);
              DialogText.text = subText;
              subText= "";
          }
  }
}
