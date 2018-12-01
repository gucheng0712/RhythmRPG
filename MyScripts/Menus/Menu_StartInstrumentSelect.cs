using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.UI;

namespace Menus
{
    public class Menu_StartInstrumentSelect : Menu<Menu_StartInstrumentSelect>
    {

        Image m_image;
        Text m_instrumentText;
        int m_selectedIndex;

        void Start()
        {
            Transform selected = transform.Find("SelectedInstrument");

            m_image = selected.GetComponent<Image>();
            m_instrumentText = selected.GetComponentInChildren<Text>();

            GameManager_Data.Instance.selectedInstrument = GameManager_Data.Instance.starterInstruments[m_selectedIndex];
            UpdateInstrumentUI();
        }

        void SwitchToNextInstrument()
        {
            int starterInstrumentCount = GameManager_Data.Instance.starterInstruments.Count;
            m_selectedIndex = (++m_selectedIndex) % starterInstrumentCount;

            GameManager_Data.Instance.selectedInstrument = GameManager_Data.Instance.starterInstruments[m_selectedIndex];
            UpdateInstrumentUI();
        }

        void SwitchToLastInstrument()
        {
            int starterInstrumentCount = GameManager_Data.Instance.starterInstruments.Count - 1;
            m_selectedIndex--;
            if (m_selectedIndex < 0)
            {
                m_selectedIndex = starterInstrumentCount;
            }

            GameManager_Data.Instance.selectedInstrument = GameManager_Data.Instance.starterInstruments[m_selectedIndex];
            UpdateInstrumentUI();
        }

        void GetInstrumentFromStoreStock()
        {
            GameManager_Data.Instance.unlockedInstruments.Add(GameManager_Data.Instance.selectedInstrument);
            GameManager_Data.Instance.storeStock.Remove(GameManager_Data.Instance.selectedInstrument);
        }

        void UpdateInstrumentUI()
        {
            m_image.sprite = GameManager_Data.Instance.selectedInstrument.icon;
            m_instrumentText.text = GameManager_Data.Instance.selectedInstrument.itemName;
        }


        public void OnStartBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_SaveLoad.Instance.NewGame();
            GetInstrumentFromStoreStock();
            GameManager_SaveLoad.Instance.SaveGame();
            LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, false);
        }
        public void OnRightBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            SwitchToNextInstrument();
        }

        public void OnLeftBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            SwitchToLastInstrument();
        }
    }

}
