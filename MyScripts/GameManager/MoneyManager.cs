using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager
{
    public class MoneyManager : GameManager<MoneyManager>
    {
        Text m_moneyText;

        #region Initialization
        void Start()
        {
            m_moneyText = GetComponentInChildren<Text>();
            UpdateUI();
        }
        #endregion


        #region MoneyControl Functions
        public void IncreaseMoney(int amount)
        {
            GameManager_Data.Instance.Money += amount;
            UpdateUI();
        }

        public void ReduceMoney(int amount)
        {
            GameManager_Data.Instance.Money -= amount;
            UpdateUI();
        }

        public static bool CheckAffordability(int amount)
        {
            return amount <= GameManager_Data.Instance.Money;
        }
        #endregion


        // todo maybe sepreate into another class as "View"
        #region UI Update
        private void UpdateUI()
        {
            m_moneyText.text = "$ " + GameManager_Data.Instance.Money;
        }
        #endregion


    }
}

