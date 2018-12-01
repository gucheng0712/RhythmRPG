using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeaking : MonoBehaviour {

    public GameObject speechBubble;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IsSpeaking()
    {
        if(speechBubble.activeSelf == false)
        {
            speechBubble.SetActive(true);
			StartCoroutine("StopText");
        }
        else
        {
            speechBubble.SetActive(false);
        }
    }
	IEnumerator StopText()
	{
		yield return new WaitForSeconds (5.0f);
		speechBubble.SetActive(false);
	}
}
