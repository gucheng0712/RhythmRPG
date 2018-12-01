using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardForTheLoveOfGod : MonoBehaviour {


    void PleaseFastForwardSong()
    {
        GameObject.Find("Bar_Layout").transform.GetChild(1).GetChild(3).GetComponent<AudioSource>().time = GameObject.Find("Bar_Layout").transform.GetChild(1).GetChild(3).GetComponent<AudioSource>().clip.length-1;
    }




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
