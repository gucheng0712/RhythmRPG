using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;

public class RhythmGameKeys : MonoBehaviour {

    public Text key1;
    public Text key2;
    public Text key3;
    public Text key4;


    // Use this for initialization
    void Start () {
        key1.text = GameManager_Data.Instance.keys["Note1Key"].ToString();
        key2.text = GameManager_Data.Instance.keys["Note2Key"].ToString();
        key3.text = GameManager_Data.Instance.keys["Note3Key"].ToString();
        key4.text = GameManager_Data.Instance.keys["Note4Key"].ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
