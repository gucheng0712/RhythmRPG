using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

[System.Serializable]
public class GroupyData
{

    [SerializeField] int m_barPeopleNum;
    public int BarPeopleNum { get { return m_barPeopleNum; } set { BarPeopleNum = value; } }
    [SerializeField] int m_restaurantPeopleNum;
    public int RestaurantPeopleNum { get { return m_restaurantPeopleNum; } set { m_restaurantPeopleNum = value; } }
    [SerializeField] int m_clubPeopleNum;
    public int ClubPeopleNum { get { return m_clubPeopleNum; } set { m_clubPeopleNum = value; } }
    [SerializeField] int m_musicFestivalPeopleNum;
    public int MusicFesitivalPeopleNum { get { return m_musicFestivalPeopleNum; } set { m_musicFestivalPeopleNum = value; } }


    [SerializeField] int m_phraseOneNum;
    public int PhraseOneNum { get { return m_phraseOneNum; } set { m_phraseOneNum = value; } }
    [SerializeField] int m_phraseTwoNum;
    public int PhraseTwoNum { get { return m_phraseTwoNum; } set { m_phraseTwoNum = value; } }
    [SerializeField] int m_phraseThreeNum;
    public int PhraseThreeNum { get { return m_phraseThreeNum; } set { m_phraseThreeNum = value; } }

    public int GroupyNum { get { return PhraseOneNum + PhraseTwoNum + PhraseThreeNum; } }

    [SerializeField] float m_groupyBouns;
    public float GroupyBonus { get { return PhraseOneNum * 0.002f + PhraseTwoNum * 0.004f + PhraseThreeNum * 0.006f; } }

    [SerializeField] float m_attendGroupyPercent;
    public float AttendGroupyPercent { get { return m_attendGroupyPercent; } set { m_attendGroupyPercent = value; } }


    public GroupyData()
    {
        m_barPeopleNum = 15;
        m_restaurantPeopleNum = 20;
        m_clubPeopleNum = 30;
        m_musicFestivalPeopleNum = 35;
    }

    public void UpdateVenueRemainingPeople(int gainedGroupies)
    {
        switch (GameManager_Data.Instance.playerData.playerPerformLocation)
        {
            case "Bar":
                ScoreScreen.gainedGroupy = (gainedGroupies > m_barPeopleNum) ? m_barPeopleNum : ScoreScreen.gainedGroupy;
                m_barPeopleNum -= gainedGroupies;
                m_barPeopleNum = Mathf.Clamp(m_barPeopleNum, 0, 15);
                break;
            case "Restaurant":
                ScoreScreen.gainedGroupy = (gainedGroupies > m_restaurantPeopleNum) ? m_restaurantPeopleNum : ScoreScreen.gainedGroupy;
                m_restaurantPeopleNum -= gainedGroupies;
                m_restaurantPeopleNum = Mathf.Clamp(m_restaurantPeopleNum, 0, 20);
                break;
            case "Club":
                ScoreScreen.gainedGroupy = (gainedGroupies > m_clubPeopleNum) ? m_clubPeopleNum : ScoreScreen.gainedGroupy;
                m_clubPeopleNum -= gainedGroupies;
                m_clubPeopleNum = Mathf.Clamp(m_clubPeopleNum, 0, 30);
                break;
            case "MusicFestival":
                ScoreScreen.gainedGroupy = (gainedGroupies > m_musicFestivalPeopleNum) ? m_musicFestivalPeopleNum : ScoreScreen.gainedGroupy;
                m_musicFestivalPeopleNum -= gainedGroupies;
                m_musicFestivalPeopleNum = Mathf.Clamp(m_musicFestivalPeopleNum, 0, 35);
                break;
        }
    }




}
