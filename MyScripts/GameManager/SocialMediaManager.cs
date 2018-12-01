using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
namespace GameManager
{
    public enum DeterminedLocation
    {
        Bar,
        Restaurant,
        NightClub,
        MusicFestival
    }

    public class SocialMediaManager : GameManager<SocialMediaManager>
    {
        [SerializeField] GameObject m_groupyNamePrefab; // todo load from resources later
        [SerializeField] GameObject m_playerPostPrefab; // todo load from resources later
        [SerializeField] GameObject m_groupyPostPrefab; // todo load from resources later

        [SerializeField] Sprite m_groupyIcon;

        [SerializeField] AudioClip postFX;

        [Space(10)]
        [Header("=====Load From Code=====")]
        [Header("=====Serialize for Testing=====")]

        /// <summary>
        /// The determined performed location to .
        /// </summary>
        public DeterminedLocation determinedLocation;



        /// <summary>
        /// The player icon needs to update when player evolve.
        /// </summary>
        [SerializeField] Sprite m_playerIcon;

        /// <summary>
        /// The tweet history scroll bar Component
        /// </summary>
        [SerializeField] Scrollbar m_historyScrollBar;

        /// <summary>
        /// The Parent of The Random Groupy Name Text Transform
        /// </summary>
        [SerializeField] Transform m_groupyNameContent;

        /// <summary>
        /// The Panel that Allow player to choose post to publish 
        /// </summary>
        [HideInInspector] public GameObject postPanel;
        /// <summary>
        /// Tweets Variables
        /// </summary>
        [SerializeField] Transform m_postParent;
        [SerializeField] Transform[] posts;
        [SerializeField] Transform m_postHistoryParent;
        [SerializeField] int maxTweetHistoryNum = 30;

        /// <summary>
        /// Random Groupy Name Animation Variables
        /// </summary>
        const float GROUPY_NAME_HEIGHT = 70f;
        [SerializeField] float m_currentGroupyNamePosY;
        [SerializeField] float m_maxGroupyNamePosY;
        [SerializeField] string m_animatedState = "Up";
        [SerializeField] List<string> nameContainer = new List<string>();

        #region Initialization

        protected override void Awake()
        {
            base.Awake();
            InitializeComponents();
        }

        private void OnEnable()
        {
            if (m_groupyNameContent != null)
                InitializeRandomGroupyName();
        }

        void Start()
        {
            InitializeComponents();
            GetAllPlayerPosts();
            InitializeRandomGroupyName();

            // Clear everything and fill it out
            InitializeTweetHistory();
        }

        void InitializeComponents()
        {
            if (m_playerIcon == null)
                m_playerIcon = transform.Find("PlayerIcon").GetComponent<Image>().sprite;

            if (m_historyScrollBar == null)
                m_historyScrollBar = transform.Find("History").GetComponentInChildren<Scrollbar>();

            if (m_groupyNameContent == null)
                m_groupyNameContent = GameObject.FindGameObjectWithTag("GroupyNameCycle").transform;
            m_historyScrollBar.value = 0; // Reset scrollbar value
            //m_playerIcon = GameManager_Data.Instance.playerData.playerIcon; // todo player evolvulation

            if (m_postParent == null)
                m_postParent = GameObject.FindGameObjectWithTag("PostContent").transform;

            if (m_postHistoryParent == null)
                m_postHistoryParent = GameObject.FindGameObjectWithTag("PostHistoryContent").transform;

            if (postPanel == null)
                postPanel = transform.Find("PostPanel").gameObject;
            postPanel.SetActive(false);
        }

        // get all the player's post in order to get the post's text in toggle group
        void GetAllPlayerPosts()
        {
            int childCount = m_postParent.childCount;
            posts = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                posts[i] = m_postParent.GetChild(i);
            }
        }

        void InitializeRandomGroupyName()
        {
            if (m_groupyNameContent.childCount > 0)
            {
                foreach (Transform child in m_groupyNameContent)
                {
                    Destroy(child.gameObject);
                }
            }

            nameContainer = GameManager_Data.Instance.socialMediaData.groupyNamesArr.ToList();
            int loopCount = 0;
            loopCount = (GameManager_Data.Instance.groupyData.GroupyNum > 10) ? nameContainer.Count : 0;
            print(loopCount);
            for (int i = 0; i < loopCount; i++)
            {
                string randomName = nameContainer[Random.Range(0, nameContainer.Count)];
                nameContainer.Remove(randomName);
                // tood assign the text to the n
                Transform newGroupyName = Instantiate(m_groupyNamePrefab, m_groupyNameContent).transform;
                newGroupyName.GetComponent<Text>().text = randomName;
                newGroupyName.GetComponent<Text>().color = new Color(1, 1, 1, 1.0f);
            }
            m_maxGroupyNamePosY = m_groupyNameContent.childCount * GROUPY_NAME_HEIGHT - 560; // 400 is the name container's height
        }

        public void InitializeTweetHistory()
        {
            foreach (Transform child in m_postHistoryParent)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < GameManager_Data.Instance.tweetsHistory.Count - 1; i++)
            {
                GameObject prefab = GameManager_Data.Instance.playerPostHistoryFlag[i] ? m_playerPostPrefab : m_groupyPostPrefab;

                Transform newPostHistory = Instantiate(prefab, m_postHistoryParent).transform;
                newPostHistory.Find("Text").GetComponent<Text>().text = GameManager_Data.Instance.tweetsHistory[i];
                if (GameManager_Data.Instance.playerPostHistoryFlag[i] == true)
                    newPostHistory.Find("Image").GetComponent<Image>().sprite = m_playerIcon;
            }
            m_historyScrollBar.value = 0.0f; // rest the slider value, make sure, every time post new tweet, the slider will move to the newest tweet post history
        }

        #endregion


        void Update()
        {
            GroupyListAnimationCycle();
        }

        #region Tweets Functions

        // Player post new tweet and add it into the TweetHistory
        public void PostNewTweet()
        {
            GameManager_Audio.Instance.PlaySingle(postFX);
            GameManager_Data.Instance.HasPublishedPost = true;
            Transform newPostHistory = Instantiate(m_playerPostPrefab, m_postHistoryParent).transform;

            string playerPostText = "";
            // todo maybe have bug
            // Set the toggle on's post as player post text
            foreach (var post in posts)
            {
                if (post.GetComponent<Toggle>().isOn)
                {
                    playerPostText = post.GetComponentInChildren<Text>().text;
                }
            }

            newPostHistory.Find("Text").GetComponent<Text>().text = playerPostText; // Update the post msg to the selected msg
            newPostHistory.Find("Image").GetComponent<Image>().sprite = m_playerIcon; // Update the messager's icon here is the player (player can evolve)
            m_historyScrollBar.value = 0.0f; // rest the slider value, make sure, every time post new tweet, the slider will move to the newest tweet post history

            // Add the newTweet into the data for future saving 
            AddHistoryIntoData(playerPostText, true);
            GroupiesResponses();
        }

        public void GroupiesResponses()
        {
            if (GameManager_Data.Instance.groupyData.GroupyNum < 0) return;

            int responseNum = GameManager_Data.Instance.groupyData.GroupyNum > 10 ? 10 : GameManager_Data.Instance.groupyData.GroupyNum;

            StartCoroutine(GroupyResponseRountine(responseNum));
        }

        // waitforseconds does't work when timescale is 0
        IEnumerator GroupyResponseRountine(int loopNum)
        {
            StaticData_SocialMedia tweetData = new StaticData_SocialMedia();

            int i = 0;

            while (i < loopNum) // todo change to variable
            {
                i++;
                yield return new WaitForSecondsRealtime(Random.Range(1.0f, 3.0f));
                Transform newPostHistory = Instantiate(m_groupyPostPrefab, m_postHistoryParent).transform;
                List<string> tweetsList = tweetData.groupyTweetsData["tweetDialog"];
                string groupyPostText = tweetsList[Random.Range(0, tweetsList.Count)];
                newPostHistory.Find("Text").GetComponent<Text>().text = groupyPostText;
                newPostHistory.Find("Image").GetComponent<Image>().sprite = m_groupyIcon;
                m_historyScrollBar.value = 0.0f;
                GameManager_Audio.Instance.PlaySingle(postFX);
                AddHistoryIntoData(groupyPostText, false);
            }
        }

        // Groupys nextday feedbacks
        public void GroupiesFeedbacksNextDay(bool isGoodFeedback)
        {
            for (int i = 0; i < 10; i++)
            {
                StaticData_SocialMedia tweetData = new StaticData_SocialMedia();
                List<string> tweetsList = isGoodFeedback ? tweetData.groupyTweetsData["goodResponse"] : tweetData.groupyTweetsData["badResponse"];
                string groupyPostText = tweetsList[Random.Range(0, tweetsList.Count)];
                AddHistoryIntoData(groupyPostText, false);
            }
        }


        // Add the newTweet into the data for future saving
        void AddHistoryIntoData(string _data, bool isPlayerPost)
        {
            GameManager_Data.Instance.tweetsHistory.Add(_data);
            GameManager_Data.Instance.playerPostHistoryFlag.Add(isPlayerPost);
            if (GameManager_Data.Instance.tweetsHistory.Count > maxTweetHistoryNum)
            {
                Destroy(m_postHistoryParent.GetChild(0).gameObject);
                GameManager_Data.Instance.tweetsHistory.RemoveAt(0);
                GameManager_Data.Instance.playerPostHistoryFlag.RemoveAt(0);
            }
        }
        #endregion



        #region Random Groupy Name Functions
        // Groupy List Animation Cycle
        public void GroupyListAnimationCycle()
        {
            if (m_maxGroupyNamePosY <= 0) return;

            if (m_animatedState == "Up")
            {
                m_currentGroupyNamePosY += 0.3f;
                if (m_currentGroupyNamePosY > m_maxGroupyNamePosY)
                {
                    m_animatedState = "Down";
                }
            }
            else if (m_animatedState == "Down")
            {
                m_currentGroupyNamePosY -= 0.3f;
                if (m_currentGroupyNamePosY < -20f) //  add a little space
                {
                    m_animatedState = "Up";
                }
            }

            m_groupyNameContent.localPosition = new Vector3(m_groupyNameContent.localPosition.x, m_currentGroupyNamePosY, m_groupyNameContent.localPosition.z);

        }
        #endregion

    }
}
