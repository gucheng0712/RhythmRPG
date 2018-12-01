
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.UI;

namespace Menus
{
    public class Menu_MainMap : Menu<Menu_MainMap>
    {
        [HideInInspector] public NotificationFader notificationFader;

        [SerializeField] Text goldText;
        [SerializeField] Text groupyText;


        protected override void OnEnable()
        {
            if (notificationFader != null)
                notificationFader.SetAlpha(0);

            UpdatePropertyInfo();
        }

        private void Start()
        {
            notificationFader = GetComponent<NotificationFader>();
            notificationFader.SetAlpha(0);
        }

        public void UpdatePropertyInfo()
        {
            goldText.text = GameManager_Data.Instance.Money.ToString();
            groupyText.text = GameManager_Data.Instance.groupyData.GroupyNum.ToString();
        }

        public void OnSocialMediaBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.OpenMenu(Menu_MainMap_SocialMedia.Instance);
            GameManager_Menu.Instance.TransformGameState();
        }

        public void OnInventoryBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.OpenMenu(Menu_MainMap_Inventory.Instance);
            InventoryManager.Instance.UpdateInventoryItem();
            GameManager_Menu.Instance.TransformGameState();
        }

        public void OnAdvertisementBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.OpenMenu(Menu_MainMap_Ads.Instance);
            GameManager_Menu.Instance.TransformGameState();
        }

        public void OnOptionsBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.OpenMenu(Menu_MainMap_Option.Instance);
            GameManager_Menu.Instance.TransformGameState();
        }


        /// <summary>
        /// Below is the btn for testing purpose; 
        /// todo delete later
        /// </summary>

        public void OnLegManShopBtnPressed()
        {
            GameManager_Menu.Instance.OpenMenu(Menu_MainMap_LegMan.Instance);
            LegManManager.Instance.UpdateOwnedItem();
            GameManager_Menu.Instance.TransformGameState();
        }

        public void OnStoreOneBtnPressed()
        {
            LevelLoader.LoadLevel("Store", Menu_Store.Instance, GameManager_Menu.Instance.LoadingScreen);
            GameManager_Data.Instance.m_storeType = StoreType.StoreOne;
            StoreManager.Instance.FillTheSlots();
        }

        public void OnStoreTwoBtnPressed()
        {
            LevelLoader.LoadLevel("Store", Menu_Store.Instance, GameManager_Menu.Instance.LoadingScreen);
            GameManager_Data.Instance.m_storeType = StoreType.StoreTwo;
            StoreManager.Instance.FillTheSlots();
        }

        public void OnLeaveGameBtnPressed()
        {
            LevelLoader.LoadLevel("StartMenu", Menu_StartMenu.Instance, GameManager_Menu.Instance.LoadingScreen);
            // todo also need to save the game data
        }

        public void OnTestLobbyBtnPressed()
        {
            LevelLoader.LoadLevel("Lobby", Menu_Lobby.Instance);
        }

        public void OnDayNightSwitchBtnPressed()
        {
            LevelLoader.LoadMainMapLevel(true);
        }
    }
}
