using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public enum Difficulty
{
    Easy,
    Normal,
    Hard
}


namespace GameManager
{

    public class GameManager_Data : GameManager<GameManager_Data>
    {
        [Space(10)]
        [Header("Other Data")]
        public bool isFirstTimePlay = true;
        public Difficulty curDifficulty;

        [Space(10)]
        [Header("Instrument Data")]
        // todo load data from file
        public List<Instrument> instrumentDataBase = new List<Instrument>();
        public List<Instrument> starterInstruments = new List<Instrument>();
        public Instrument selectedInstrument;

        [Space(10)]
        [Header("Song Data")]
        // todo maybe discard later since no more song selection
        public List<SongData> songDataBase = new List<SongData>();
        public SongData selectedSong;

        //Andres Did This dont delete
        public List<SongDataBusking> songDataBaseBusking = new List<SongDataBusking>();
        public SongDataBusking selectedSongBusking;

        public List<SongDataGhostBusking> songDataBaseGhostBusking = new List<SongDataGhostBusking>();
        public SongDataGhostBusking selectedSongGhostBusking;


        [Space(20)]
        [Header("=====Load From Code=====")]
        [Header("=====Serialize for Testing=====")]

        [Space(10)]
        [Header("Instrument Data")]
        public List<Instrument> unlockedInstruments = new List<Instrument>();
        public List<Instrument> lockedInstruments = new List<Instrument>();
        public List<Instrument> storeStock = new List<Instrument>();
        public StoreType m_storeType;

        [Space(5)]
        [Header("Social Media Data")]
        public List<string> tweetsHistory = new List<string>();
        public List<bool> playerPostHistoryFlag = new List<bool>();

        public StaticData_SocialMedia socialMediaData;
        [SerializeField] bool m_hasPublishedPost; //  To Check if player has published post today
        public bool HasPublishedPost { get { return m_hasPublishedPost; } set { m_hasPublishedPost = value; } }

        public const float SOCIALMEDIA_ATTEND_PERCENT = 0.2f;
        public const float FLYERS_ADS_ATTEND_PERCENT = 0.4f;
        public const float BILLBOARD_ADS_ATTEND_PERCENT = 0.6f;
        public const float TV_ADS_ATTEND_PERCENT = 0.8f;

        public const int SOCIAL_MEDIA_COST = 0;
        public const int FLYERS_ADS_COST = 50;
        public const int BILLBOARD_ADS_COST = 75;
        public const int TV_ADS_COST = 100;

        [Space(5)]
        public PlayerData playerData;

        [Space(5)]
        [Header("Groupy Data")]
        public GroupyData groupyData;

        [Space(5)]
        [Header("RhythmGame Data")]
        [SerializeField] int m_barHighScore;
        public int BarHighScore { get { return m_barHighScore; } set { m_barHighScore = value; } }

        [SerializeField] int m_restaurantHighScore;
        public int RestaurantHighScore { get { return m_restaurantHighScore; } set { m_restaurantHighScore = value; } }

        [SerializeField] int m_clubHighScore;
        public int ClubHighScore { get { return m_clubHighScore; } set { m_clubHighScore = value; } }

        [SerializeField] int m_musicFestivalHighScore;
        public int MusicFestivalHighScore { get { return m_musicFestivalHighScore; } set { m_musicFestivalHighScore = value; } }



        [Space(10)]
        [Header("Other Data")]
        [SerializeField] bool m_isDayTime; // true --> day time; false --> night time
        public bool IsDayTime { get { return m_isDayTime; } set { m_isDayTime = value; } }

        [SerializeField] int m_money;
        public int Money { get { return m_money; } set { m_money = value; } }


        [Space(10)]
        [Header("Settings Data")]
        public SDictionaryOfStringAndKeyCode keys = new SDictionaryOfStringAndKeyCode();

        #region MonoBehaviour Methods

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            socialMediaData = new StaticData_SocialMedia();

            playerData = new PlayerData();
        }

        private void Update()
        {

        }

        #endregion



        #region Player Functions
        public void UpdatePlayerLevel(ref Animator anim)
        {
            if (groupyData.GroupyNum < 30)
            {
                anim.SetInteger("PlayerLevel", 0);
                print("level 1");
            }
            else if (groupyData.GroupyNum >= 30 && groupyData.GroupyNum < 70)
            {
                anim.SetInteger("PlayerLevel", 1);
                print("level 2");
            }
            else if (groupyData.GroupyNum >= 70)
            {
                anim.SetInteger("PlayerLevel", 2);
            }
        }

        public void UpdatePlayerLevel(Sprite player)
        {
            if (groupyData.GroupyNum < 30)
            {
                player = playerData.beginPlayerSprite;
            }
            else if (groupyData.GroupyNum >= 1000 && groupyData.GroupyNum < 2000)
            {
                player = playerData.middlePlayerSprite;
            }
            else if (groupyData.GroupyNum >= 2000)
            {
                player = playerData.endPlayerSprite;
            }
        }
        #endregion


        #region Instrument ObtainMethods

        // Find the Item in ItemDataBase through its ID
        public Instrument FindItemInDataBase(int id)
        {
            return instrumentDataBase.Find(i => i.id == id);
        }

        // Overload Method for finding item through its name
        public Instrument FindItemInDataBase(string itemName)
        {
            return instrumentDataBase.Find(i => i.itemName == itemName);
        }

        // todo prepare to discard 
        // random return a instrument
        public Instrument RandomizeInstrumentCreator()
        {
            return (storeStock.Count > 0) ? storeStock[UnityEngine.Random.Range(0, storeStock.Count)] : null;
        }
        #endregion
    }




}
