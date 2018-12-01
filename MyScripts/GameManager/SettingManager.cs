using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using Menus;
using UnityEngine.UI;

namespace Gamemanager
{
    public class SettingManager : GameManager<SettingManager>
    {
        GameObject currentKeyGO;

        void OnEnable()
        {
            Transform keyControlPanel = transform.Find("KeyControlPanel");

            if (GameManager_Data.Instance.keys.Count > 0)
            {
                SetKeyBtnText(keyControlPanel, "InteractKey");
                SetKeyBtnText(keyControlPanel, "MoveRightKey");
                SetKeyBtnText(keyControlPanel, "MoveLeftKey");
                SetKeyBtnText(keyControlPanel, "Note1Key");
                SetKeyBtnText(keyControlPanel, "Note2Key");
                SetKeyBtnText(keyControlPanel, "Note3Key");
                SetKeyBtnText(keyControlPanel, "Note4Key");
            }
        }

        void SetKeyBtnText(Transform parent, string keyName)
        {
            parent.Find(keyName).GetComponentInChildren<Text>().text = GameManager_Data.Instance.keys[keyName].ToString();
        }

        void OnGUI()
        {
            if (currentKeyGO != null)
            {
                Event e = Event.current;
                KeyCode inputKeyCode = e.keyCode;
                foreach (KeyValuePair<string, KeyCode> key in GameManager_Data.Instance.keys)
                {
                    inputKeyCode = (inputKeyCode == key.Value) ? KeyCode.None : inputKeyCode;
                }
                if (e.isKey)
                {
                    GameManager_Data.Instance.keys[currentKeyGO.name] = inputKeyCode;
                    currentKeyGO.GetComponentInChildren<Text>().text = inputKeyCode.ToString();
                    currentKeyGO.GetComponent<Toggle>().isOn = false;
                    currentKeyGO = null;

                    if (inputKeyCode == KeyCode.None)
                    {
                        Menu_MainMap_Option.Instance.notificationFader.ShowNotificationPanel();
                    }
                }

            }
        }

        public void ChangeKeyTogglePressed(GameObject clickedGO)
        {
            currentKeyGO = clickedGO;
        }
    }
}