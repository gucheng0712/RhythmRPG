using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Menus
{
    public class Menu_MainMap_Option : Menu<Menu_MainMap_Option>
    {

        [HideInInspector] public NotificationFader notificationFader;

        protected override void OnEnable()
        {
            if (notificationFader != null)
                notificationFader.SetAlpha(0);
        }

        void Start()
        {
            notificationFader = GetComponent<NotificationFader>();
            notificationFader.SetAlpha(0);
        }

        public override void OnBackBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.OpenMenu(Menu_MainMap.Instance);
            GameManager_Menu.Instance.TransformGameState();
        }

        public void OnSaveBtnPressed()
        {
            // todo Save the game and pop up info
            Debug.Log("Save the game");
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_SaveLoad.Instance.SaveGame();
        }


        public void OnQuitBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.TransformGameState();
            LevelLoader.LoadLevel("StartMenu", Menu_StartMenu.Instance, GameManager_Menu.Instance.LoadingScreen);
        }
    }

}
