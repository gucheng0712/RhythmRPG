using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameShredder : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        ScoreManager.ResetCombo();
        other.gameObject.GetComponent<NoteTrigger>().Die("miss");
    }
}
