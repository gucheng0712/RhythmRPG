using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.UI;

namespace Menus
{
    public class Menu_Lobby : Menu<Menu_Lobby>
    {
        [HideInInspector] public string venueToLoad;

        public Image UI;
        public Image[] btns;

        public void OnEasyBtnPressed()
        {
            GameManager_Data.Instance.curDifficulty = Difficulty.Easy;
            GameManager_Audio.Instance.PlayBtnSound();
            LevelLoader.LoadLevel(venueToLoad, Menu_GamePlay.Instance, GameManager_Menu.Instance.LoadingScreen);

        }
        public void OnNormalBtnPressed()
        {
            GameManager_Data.Instance.curDifficulty = Difficulty.Normal;
            GameManager_Audio.Instance.PlayBtnSound();

            LevelLoader.LoadLevel(venueToLoad, Menu_GamePlay.Instance, GameManager_Menu.Instance.LoadingScreen);
        }
        public void OnHardBtnPressed()
        {
            GameManager_Data.Instance.curDifficulty = Difficulty.Hard;
            GameManager_Audio.Instance.PlayBtnSound();

            LevelLoader.LoadLevel(venueToLoad, Menu_GamePlay.Instance, GameManager_Menu.Instance.LoadingScreen);

        }
        public void OnSelectNextBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            LobbyManager.Instance.IsLerpingToNextSelection = true;
        }
        public override void OnBackBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, false);
        }
    }

}
