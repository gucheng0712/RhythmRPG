using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Menus
{
    public class Menu_StartMenu : Menu<Menu_StartMenu>
    {

        [Space(10)]
        [Header("=====Load From Code=====")]
        [Header("=====Serialize for Testing=====")]
        [SerializeField] GameObject loadGameBtn;
        [SerializeField] GameObject confirmPanel;

        protected override void Awake()
        {
            base.Awake();

            loadGameBtn = transform.Find("BtnGroup").Find("LoadGameBtn").gameObject;
            confirmPanel = transform.Find("ConfirmPanel").gameObject;
            confirmPanel.SetActive(false);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            GameManager_SaveLoad.Instance.CheckIfHasDatainFile();

            if (GameManager_Data.Instance.isFirstTimePlay)
            {
                loadGameBtn.SetActive(false);
            }
            else
            {
                loadGameBtn.SetActive(true);
            }
        }

        public void OnLoadGameBtnPressed()
        {
            LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, false);
            GameManager_SaveLoad.Instance.LoadGame();
            GameManager_Audio.Instance.PlayBtnSound();
        }

        public void OnNewGameBtnPressed()
        {
            if (GameManager_Data.Instance.isFirstTimePlay)
            {
                GameManager_Menu.Instance.OpenMenu(Menu_StartInstrumentSelect.Instance);
                //GameManager_SaveLoad.Instance.NewGame();
            }
            else
            {
                confirmPanel.SetActive(true);
            }

            GameManager_Audio.Instance.PlayBtnSound();
        }

        public void OnConfirmedNoBtnPressed()
        {
            confirmPanel.SetActive(false);
            GameManager_Audio.Instance.PlayBtnSound();
        }

        public void OnConfirmedYesBtnPressed()
        {
            GameManager_Menu.Instance.OpenMenu(Menu_StartInstrumentSelect.Instance);
            confirmPanel.SetActive(false);
            PlayerController.s_RecordedPlayerPos = new Vector2(-31.4f, -2.48f);
            GameManager_Data.Instance.IsDayTime = true;
            GameManager_Audio.Instance.PlayBtnSound();
        }


        public void OnQuitGameBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            Application.Quit();
        }
    }

}
