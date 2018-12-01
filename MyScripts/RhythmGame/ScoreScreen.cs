using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameManager;

public class ScoreScreen : MonoBehaviour
{
    public string m_scoreMessage = "Score: ";
    public string m_percentageMessage = "Percent hit: ";
    public string m_goldReceivedMessage = "Gold: ";
    public string m_groupiesReceivedMessage = "Groupies added: ";

    public Text m_maxComboText;
    public Text m_percentageText;
    public Text m_goldText;
    public Text m_groupiesReceivedText;


    [SerializeField] Transform highscoreStarTrans;
    [SerializeField] Transform currentScoreTrans;
    //public Vector3 m_starOffset;

    private GameObject m_venueStats;
    public float venueMuliplier = 1f;

    public static int gainedGroupy;

    void Start()
    {
        gameObject.SetActive(false);
        m_venueStats = GameObject.Find("Venue Properties");
    }

    public void ShowScore()
    {
        if (m_venueStats.GetComponent<VenueProperties>().venuePreference == VenuePreference.Ambient)
        {
            venueMuliplier += GameManager_Data.Instance.selectedInstrument.ambientBonus / 10f;

        }
        else if (m_venueStats.GetComponent<VenueProperties>().venuePreference == VenuePreference.Experimental)
        {
            venueMuliplier += (GameManager_Data.Instance.selectedInstrument.experimentalBonus / 10f);
        }
        else if (m_venueStats.GetComponent<VenueProperties>().venuePreference == VenuePreference.Loud)
        {
            venueMuliplier += GameManager_Data.Instance.selectedInstrument.loudBonus / 10f;
        }
        else if (m_venueStats.GetComponent<VenueProperties>().venuePreference == VenuePreference.Hip)
        {
            venueMuliplier += (GameManager_Data.Instance.selectedInstrument.hipBonus / 10f);
        }

        ScoreCalculation(ScoreManager.performancePercentage > 50f);
    }

    void ScoreCalculation(bool performedWell)
    {

        int gainedMoney = Mathf.RoundToInt(ScoreManager.goldReceived * (ScoreManager.performancePercentage / 100) * venueMuliplier);
        gainedGroupy = Mathf.RoundToInt(ScoreManager.groupiesReceived * venueMuliplier);
        GameManager_Data.Instance.Money += gainedMoney;

        if (SocialMediaManager.Instance != null)
            SocialMediaManager.Instance.GroupiesFeedbacksNextDay(performedWell);

        //        float score = ScoreManager.groupiesReceived * ScoreManager.performancePercentage * venueMuliplier;

        int phraseOneNum = GameManager_Data.Instance.groupyData.PhraseOneNum;
        int phraseTwoNum = GameManager_Data.Instance.groupyData.PhraseTwoNum;
        int phraseThreeNum = GameManager_Data.Instance.groupyData.PhraseThreeNum;
        float attendPercent = GameManager_Data.Instance.groupyData.AttendGroupyPercent;


        if (performedWell == true)
        {
            // the order of assign these variables are significant
            GameManager_Data.Instance.groupyData.UpdateVenueRemainingPeople(gainedGroupy);
            GameManager_Data.Instance.groupyData.PhraseThreeNum += Mathf.RoundToInt(phraseTwoNum * attendPercent);
            GameManager_Data.Instance.groupyData.PhraseTwoNum = Mathf.RoundToInt(phraseTwoNum * (1 - attendPercent)
                                                                      + (phraseOneNum * attendPercent));
            GameManager_Data.Instance.groupyData.PhraseOneNum = Mathf.RoundToInt(phraseOneNum * (1 - attendPercent)) + gainedGroupy;

            m_groupiesReceivedText.text = m_groupiesReceivedMessage + gainedGroupy.ToString();
        }
        else
        {
            // the order of assign these variables are significant
            GameManager_Data.Instance.groupyData.UpdateVenueRemainingPeople(-Mathf.RoundToInt(phraseOneNum * attendPercent));
            GameManager_Data.Instance.groupyData.PhraseOneNum = Mathf.RoundToInt(phraseOneNum * (1 - attendPercent) + phraseTwoNum * attendPercent);
            GameManager_Data.Instance.groupyData.PhraseTwoNum = Mathf.RoundToInt(phraseTwoNum * (1 - attendPercent) + phraseThreeNum * attendPercent);
            GameManager_Data.Instance.groupyData.PhraseThreeNum *= Mathf.RoundToInt(1 - attendPercent);


            m_groupiesReceivedText.text = m_groupiesReceivedMessage + "0";
        }

        //m_scoreText.text = m_scoreMessage + ScoreManager.score.ToString();
        m_percentageText.text = m_percentageMessage + ScoreManager.performancePercentage.ToString();
        m_goldText.text = m_goldReceivedMessage + gainedMoney.ToString();
        m_maxComboText.text = ScoreManager.maxComboNumber.ToString();

        StarCalculation();

        gameObject.SetActive(true);
    }

    void StarCalculation()
    {
        // if Update current high score, if bigger than older high score it will show the current high score
        SetActiveStars(RhythmGameStarRating.starRatingNumber, highscoreStarTrans);
        SetActiveStars(RhythmGameStarRating.starRatingNumber, currentScoreTrans);
        UpdateHighScore();

    }

    void UpdateHighScore()
    {
        switch (GameManager_Data.Instance.playerData.playerPerformLocation)
        {
            case "Bar":
                GameManager_Data.Instance.BarHighScore = GetHighScore(GameManager_Data.Instance.BarHighScore);
                SetActiveStars(GameManager_Data.Instance.BarHighScore, highscoreStarTrans);
                break;
            case "Restaurant":
                GameManager_Data.Instance.RestaurantHighScore = GetHighScore(GameManager_Data.Instance.RestaurantHighScore);
                SetActiveStars(GameManager_Data.Instance.RestaurantHighScore, highscoreStarTrans);
                break;
            case "Club":
                GameManager_Data.Instance.ClubHighScore = GetHighScore(GameManager_Data.Instance.ClubHighScore);
                SetActiveStars(GameManager_Data.Instance.ClubHighScore, highscoreStarTrans);
                break;
            case "MusicFestival":
                GameManager_Data.Instance.MusicFestivalHighScore = GetHighScore(GameManager_Data.Instance.MusicFestivalHighScore);
                SetActiveStars(GameManager_Data.Instance.MusicFestivalHighScore, highscoreStarTrans);
                break;
        }
    }
    int GetHighScore(int highScore)
    {
        return highScore < RhythmGameStarRating.starRatingNumber ? RhythmGameStarRating.starRatingNumber : highScore;
    }

    void SetActiveStars(int starNum, Transform parent)
    {
        for (int i = 0; i < starNum; i++)
        {
            parent.GetChild(i).gameObject.SetActive(true);
        }
    }


    // Button Event
    public void ReturnToMap()
    {
        LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, true);
    }
}
