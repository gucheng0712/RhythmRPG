using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class SoloSection : MonoBehaviour
{

    private void Start()
    {

        /*
        if (GameManager_Data.Instance.selectedInstrument.chorusChart == ChorusChart.Loud) {
            this.GetComponent<Renderer>().enabled = true;
        } else {
            this.GetComponent<Renderer>().enabled = false;
        }
        */

        this.GetComponent<Renderer>().enabled = GameManager_Data.Instance.selectedInstrument.chorusChart == ChorusChart.Loud;

    }
}
