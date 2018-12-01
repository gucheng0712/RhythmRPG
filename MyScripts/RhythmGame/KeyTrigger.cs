using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class KeyTrigger : MonoBehaviour
{

    public KeyCode rhythmKey;

    [SerializeField] Color inactiveColor;
    [SerializeField] Color activeColor;

    Material mat;

    bool canDestroyNote = true;
    //bool notePresent = false;
    private Collider currentNote;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        mat.color = Input.GetKey(rhythmKey) ? activeColor : inactiveColor;

        if (Input.GetKeyDown(rhythmKey))
        {

            if (GameManager.GameplayManager.isInLoudSection)
            {

                ScoreManager.AddToLoudCombo();

            }
            else
            {

                if (!currentNote)
                {
                    ScoreManager.ResetCombo();
                    if (PerformanceRating.currentNotes > 0)
                    {
                        PerformanceRating.currentNotes--;
                    }

                    //ScoreManager.AddMiss();
                }
                else if (currentNote.gameObject)
                {
                    if (canDestroyNote)
                    {

                        ScoreManager.AddToCombo();
                        //ScoreManager.AddHit();
                        StartCoroutine(DestroyNoteRoutine());
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!currentNote)
        {
            currentNote = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentNote = null;
    }

    private void LateUpdate()
    {
        currentNote = null;
    }

    IEnumerator DestroyNoteRoutine()
    {
        currentNote.gameObject.GetComponent<NoteTrigger>().Die("hit");
        canDestroyNote = false;
        yield return new WaitForEndOfFrame();
        canDestroyNote = true;
    }
}
