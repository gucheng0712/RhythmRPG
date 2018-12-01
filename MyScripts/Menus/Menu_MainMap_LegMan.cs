using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;


namespace Menus
{
    public class Menu_MainMap_LegMan : Menu<Menu_MainMap_LegMan>
    {
        [HideInInspector] public NotificationFader notificationFader;

        GameObject m_nextBtn;

        protected override void OnEnable()
        {
            if (notificationFader != null)
                notificationFader.SetAlpha(0);

            CheckIfNeedNextBtn();

        }

        private void Start()
        {
            notificationFader = GetComponent<NotificationFader>();
            notificationFader.SetAlpha(0);
            m_nextBtn = transform.Find("LegManShopManager").Find("NextBtn").gameObject;
            CheckIfNeedNextBtn();
        }

        void CheckIfNeedNextBtn()
        {
            if (m_nextBtn != null)
            {
                if (GameManager_Data.Instance.unlockedInstruments.Count < 3)
                {
                    m_nextBtn.SetActive(false);
                }
                else
                {
                    m_nextBtn.SetActive(true);
                }
            }
        }

        public override void OnBackBtnPressed()
        {
            base.OnBackBtnPressed();
            GameManager_Menu.Instance.TransformGameState();
        }

        public void OnNextBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            LegManManager.Instance.IsLerpingToNextSelection = true;
        }
    }
}

