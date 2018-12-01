using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;

public class ScoreManager : MonoBehaviour
{

    [HideInInspector] public static float notesHit = 0f;
    [HideInInspector] public static float notesMissed = 0f;

    [HideInInspector] public static float loudNotesHit = 0f;
    [HideInInspector] public static float loudNotesMissed = 0f;

    [HideInInspector] public static float comboNumber = 0f;
    [HideInInspector] public static float maxComboNumber = 0f;
    [HideInInspector] public static float loudComboNumber = 0f;

    [HideInInspector] public static float score = 0f;
    [HideInInspector] public static float loudScore = 0f;
    [HideInInspector] public static float overallScore = 0f;

    #region Old Score System

    // Old Score System
    [HideInInspector] public static float cycleNumber = 0f;
    [HideInInspector] public static float multiplierNumber = 1f;
    [HideInInspector] public static float performancePercentage = 0f;
    [HideInInspector] public static float noteAmountStatic;
    [HideInInspector] public static float cycleAmountStatic;
    [HideInInspector] public static float cycleMaxStatic;
    [HideInInspector] public static float multiplierAmountStatic;
    [HideInInspector] public static float multiplierMaxStatic;

    #endregion

    [HideInInspector] public static int goldReceived = 0;
    [HideInInspector] public static float difficultyMultiplier = 1f;
    [HideInInspector] public static float baseVenueReward = 100f;
    [HideInInspector] public static float instrumentMultiplier = 1f;

    [HideInInspector] public static int groupiesReceived = 10;

    Interactables currentVenue;

    #region Old Score System
    // Old Score System
    [Header("Score Variables")]
    [Space(5)]
    [Tooltip("Number of points a note is worth")]
    [HideInInspector] public float noteAmount = 10f;
    [Tooltip("How much a note is worth in a cycle")]
    [HideInInspector] public float cycleAmount = 1f;
    [Tooltip("The maximum number of notes in a cycle before adding to multiplier")]
    [HideInInspector] public float cycleMax = 5f;
    [Tooltip("How much a cycle is worth when completed")]
    [HideInInspector] public float multiplierAmount = 1f;
    [Tooltip("The maximum number of multipliers")]
    [HideInInspector] public float multiplierMax = 4f;

    #endregion

    // UI text
    [Header("User Interface References")]
    [Space(5)]
    public Text scoreText;
    public Text loudScoreText;
    ///public Text overallScoreText;

    //public Text percentageText;
    //public Text cycleText;
    //public Text multiplierText;

    private void Start()
    {
        notesHit = 0;
        notesMissed = 0;
        loudNotesHit = 0;
        loudNotesMissed = 0;
        score = 0;
        comboNumber = 0;
        overallScore = 0;
        performancePercentage = 0;
        cycleNumber = 0;
        multiplierNumber = 1;
        noteAmountStatic = noteAmount;
        cycleAmountStatic = cycleAmount;
        cycleMaxStatic = cycleMax;
        multiplierAmountStatic = multiplierAmount;
        multiplierMaxStatic = multiplierMax;
    }

    void Update()
    {

        scoreText.text = score.ToString();
        if (loudScore == 0 && GameplayManager.isInLoudSection)
        {
            loudScoreText.text = "Button Mash!";
        }
        else if (loudScore == 0)
        {
            loudScoreText.text = "";
        }
        else
        {
            loudScoreText.text = loudScore.ToString() + "!";
        }
        //overallScoreText.text = overallScore.ToString();
        //cycleText.text = cycleNumber.ToString() + "/" + cycleMax.ToString();
        //multiplierText.text = "x" + multiplierNumber.ToString();
        goldReceived = Mathf.RoundToInt(difficultyMultiplier * baseVenueReward * instrumentMultiplier);
        // Performance Percentage
        if (notesHit + notesMissed != 0f)
        {

            // Floored to no decimals
            performancePercentage = Mathf.Floor((notesHit / (notesHit + notesMissed)) * 100);

            // Floored to 2 decimals
            //performancePercentage = Mathf.Floor((notesHit / (notesHit + notesMissed)) * 100 * 100) / 100;

            scoreText.text = (score.ToString());
            //percentageText.text = (performancePercentage.ToString() + "%");

            overallScore = Mathf.Floor((notesHit / (ChartManager.totalNotes + notesMissed)) * 100);
            print(ChartManager.totalNotes);
        }
        else
        {
            performancePercentage = 100f;
        }

    }

    public static void AddToCombo()
    {
        notesHit += 1;
        comboNumber += 1;
        AddToScore();
    }

    public static void ResetCombo()
    {
        notesMissed += 1;
        comboNumber = 0;
        AddToScore();
    }

    public static void AddToLoudCombo()
    {
        loudNotesHit += 1;
        loudComboNumber += 1;
        AddToScore();
    }

    public static void ResetLoudCombo()
    {
        loudComboNumber = 0;
        AddToScore();
    }

    #region Old Score System

    public static void AddHit()
    {
        notesHit += 1;
        AddNoteToCycle();
        AddToScore();
    }

    public static void AddMiss()
    {
        notesMissed += 1;
        ResetCycle();
        ResetMultiplier();
    }

    public static void AddNoteToCycle()
    {
        if (cycleNumber >= cycleMaxStatic)
        {
            if (multiplierNumber < multiplierMaxStatic)
            {
                ResetCycle();
                AddCycleToMultiplier();
            }
        }
        else
        {
            cycleNumber += cycleAmountStatic;
        }
    }

    public static void ResetCycle()
    {
        cycleNumber = 0f;
    }

    public static void AddCycleToMultiplier()
    {
        if (multiplierNumber < multiplierMaxStatic)
        {
            multiplierNumber += multiplierAmountStatic;
        }
    }

    public static void ResetMultiplier()
    {
        multiplierNumber = 1f;
    }

    #endregion

    public static void AddToScore()
    {
        //score += noteAmountStatic * multiplierNumber;
        score = comboNumber;
        loudScore = loudComboNumber;
        maxComboNumber = comboNumber > maxComboNumber ? comboNumber : maxComboNumber;
    }

}
