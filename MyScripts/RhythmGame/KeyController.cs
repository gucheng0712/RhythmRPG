using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class KeyController : MonoBehaviour
{
    [SerializeField] KeyTrigger key1;
    [SerializeField] KeyTrigger key2;
    [SerializeField] KeyTrigger key3;
    [SerializeField] KeyTrigger key4;

    void Start()
    {
        key1 = transform.Find("Key 1").GetComponent<KeyTrigger>();
        key2 = transform.Find("Key 2").GetComponent<KeyTrigger>();
        key3 = transform.Find("Key 3").GetComponent<KeyTrigger>();
        key4 = transform.Find("Key 4").GetComponent<KeyTrigger>();

        if (GameManager_Data.Instance.keys != null)
        {
            key1.rhythmKey = GameManager_Data.Instance.keys["Note1Key"];
            key2.rhythmKey = GameManager_Data.Instance.keys["Note2Key"];
            key3.rhythmKey = GameManager_Data.Instance.keys["Note3Key"];
            key4.rhythmKey = GameManager_Data.Instance.keys["Note4Key"];
        }
        else
        {
            key1.rhythmKey = KeyCode.D;
            key2.rhythmKey = KeyCode.F;
            key3.rhythmKey = KeyCode.J;
            key4.rhythmKey = KeyCode.K;
        }

    }

    void Update()
    {

    }
}
