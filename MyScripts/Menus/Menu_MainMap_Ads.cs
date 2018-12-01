using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Menus
{
    public class Menu_MainMap_Ads : Menu<Menu_MainMap_Ads>
    {
        NotificationFader m_notificationFader;

        protected override void OnEnable()
        {
            if (m_notificationFader != null)
                m_notificationFader.SetAlpha(0);
        }

        void Start()
        {
            m_notificationFader = GetComponent<NotificationFader>();
            m_notificationFader.SetAlpha(0);
        }

        void Update()
        {
            OpenMainMapMenuByKey();
        }

        public override void OnBackBtnPressed()
        {
            base.OnBackBtnPressed();
            GameManager_Menu.Instance.TransformGameState();
        }


        void PostAds(float attendPercent, int cost)
        {
            GameManager_Audio.Instance.PlayBtnSound();

            if (GameManager_Data.Instance.IsDayTime)
            {
                if (MoneyManager.CheckAffordability(cost))
                {
                    GameManager_Data.Instance.groupyData.AttendGroupyPercent = attendPercent;
                    GameManager_Data.Instance.Money -= cost;
                }
                else
                {
                    m_notificationFader.msg.text = "You don't Have Enough Money to Pay For the Ads!";
                    m_notificationFader.ShowNotificationPanel();
                }
            }
            else
            {
                m_notificationFader.msg.text = "You can't Publish Advertisement at Night Time!";
                m_notificationFader.ShowNotificationPanel();
            }
        }


        // todo refactor later
        public void OnFlyersBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            PostAds(GameManager_Data.FLYERS_ADS_ATTEND_PERCENT, GameManager_Data.FLYERS_ADS_COST);
        }

        public void OnBillboardBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            PostAds(GameManager_Data.BILLBOARD_ADS_ATTEND_PERCENT, GameManager_Data.BILLBOARD_ADS_COST);
        }
        public void OnTelevisionBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            PostAds(GameManager_Data.TV_ADS_ATTEND_PERCENT, GameManager_Data.TV_ADS_COST);
        }

    }

}