using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.UI;

public class CheatMenu : MonoBehaviour {

    public InputField GroupyInput;
    public InputField GoldInput;

    public GameObject cheatMenu;

    private bool isActive;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.N))
        {
            isActive = !isActive;
            cheatMenu.SetActive(isActive);
        }
	}


    public void AddToGroupies()
    {
        GameManager_Data.Instance.groupyData.PhraseOneNum += int.Parse(GroupyInput.text);
    }

    public void AddToGold()
    {
        GameManager_Data.Instance.Money += int.Parse(GoldInput.text);
    }
}
