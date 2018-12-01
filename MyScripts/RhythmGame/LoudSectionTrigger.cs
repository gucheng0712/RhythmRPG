using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudSectionTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        GameManager.GameplayManager.SetLoudBool(true);
    }

    private void OnTriggerExit(Collider other) {
        ScoreManager.ResetLoudCombo();
        GameManager.GameplayManager.SetLoudBool(false);
    }
}
