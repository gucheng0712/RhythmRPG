using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;


namespace Menus
{
    public class Menu_MainMap_SocialMedia : Menu<Menu_MainMap_SocialMedia>
    {

        NotificationFader m_notificationFader;

        protected override void OnEnable()
        {
            if (m_notificationFader != null)
                m_notificationFader.SetAlpha(0);
        }

        private void Start()
        {
            m_notificationFader = GetComponent<NotificationFader>();
            m_notificationFader.SetAlpha(0);
        }

        private void Update()
        {
            OpenMainMapMenuByKey();
        }

        public override void OnBackBtnPressed()
        {
            if (ShouldLoadToNight())
            {
                GameManager_Audio.Instance.PlayBtnSound();
                LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, true);
            }
            else
            {
                base.OnBackBtnPressed();
            }
            GameManager_Menu.Instance.TransformGameState();
        }

        public override void OpenMainMapMenuByKey()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (ShouldLoadToNight())
                {
                    LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, true);
                }
                else
                {
                    GameManager_Menu.Instance.OpenMenu(Menu_MainMap.Instance);
                }
                GameManager_Menu.Instance.TransformGameState();
            }
        }

        bool ShouldLoadToNight()
        {
            return GameManager_Data.Instance.HasPublishedPost && GameManager_Data.Instance.IsDayTime
                    && GameManager_Menu.Instance.gameState == GameState.Pause;
        }

        public void OnWritePostBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();

            if (GameManager_Data.Instance.groupyData.GroupyNum == 0)
            {
                m_notificationFader.msg.text = "You don't have any groupies, so why you want to post????";
                m_notificationFader.ShowNotificationPanel();
                return;
            }

            if (!GameManager_Data.Instance.HasPublishedPost && GameManager_Data.Instance.IsDayTime)
            {
                SocialMediaManager.Instance.postPanel.SetActive(true);
            }
            else
            {
                m_notificationFader.msg.text = GameManager_Data.Instance.IsDayTime ?
                                        "You Have Already Published a Post Today!"
                                        :
                                        "You can't Publish Post at Night Time!";
                m_notificationFader.ShowNotificationPanel();
            }
        }

        public void OnPostBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            SocialMediaManager.Instance.PostNewTweet();
            SocialMediaManager.Instance.postPanel.SetActive(false);
            GameManager_Data.Instance.groupyData.AttendGroupyPercent = GameManager_Data.SOCIALMEDIA_ATTEND_PERCENT;
        }


        // maybe delete if not categories
        public void OnGoBarPostToggleOn()
        {
            SocialMediaManager.Instance.determinedLocation = DeterminedLocation.Bar;
        }

        public void OnGoRestaurantPostToggleOn()
        {
            SocialMediaManager.Instance.determinedLocation = DeterminedLocation.Restaurant;
        }

        public void OnGoNightClubPostToggleOn()
        {
            SocialMediaManager.Instance.determinedLocation = DeterminedLocation.NightClub;
        }

        public void OnGoMusicFestivalPostToggleOn()
        {
            SocialMediaManager.Instance.determinedLocation = DeterminedLocation.MusicFestival;
        }

    }

}
