using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class PerfomAnimation : MonoBehaviour
{

    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("ID", GameManager_Data.Instance.selectedInstrument.id);
    }

}
