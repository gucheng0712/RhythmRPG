using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameStarRating : MonoBehaviour
{

    [Header("Star Gain Thresholds")]
    [Space(5)]
    [Tooltip("The percentage of total notes hit before gaining half a star. These may be manipulated non-linearly to augment difficulty.")]
    [SerializeField] public float[] starThresholds = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

    private float score;

    [SerializeField] private int tempStarRatingNumber;
    [SerializeField] public static int starRatingNumber;

    private void Awake()
    {
        score = 0;
        tempStarRatingNumber = 0;
        starRatingNumber = 0;
    }

    void Update()
    {

        if (ScoreManager.overallScore > score)
        {
            score = ScoreManager.overallScore;
            // print(score);
            ShowStars();
        }

    }

    void ShowStars()
    {
        for (int i = 0; i < starThresholds.Length; i++)
        {
            if (score >= starThresholds[i])
            {
                tempStarRatingNumber = i > tempStarRatingNumber ? i : tempStarRatingNumber;
                starRatingNumber = tempStarRatingNumber + 1;
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                break;
            }
        }
    }
}
