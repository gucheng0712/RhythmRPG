using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using UnityEngine.EventSystems;

public class Item_Store : Item
{

    private void OnBuyButtonClick()
    {
        if (instrument != null)
        {
            GameManager_Audio.Instance.PlayBtnSound();
            if (MoneyManager.CheckAffordability(instrument.price))
            {
                StoreManager.Instance.SellItem(instrument);
                instrument = null;
                StoreManager.Instance.UpdataBackgroundUI();
                StoreManager.Instance.HideDescriptionUI();
            }
            else
            {
                Debug.Log("no money");
                StoreManager.Instance.notificationFader.ShowNotificationPanel();
            }

        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (instrument != null)
        {
            GameManager_Audio.Instance.PlayBtnSound();
            StoreManager.Instance.UpdataBackgroundUI();
            StoreManager.Instance.UpdateDescriptionUI(instrument);
            StoreManager.Instance.buyButton.onClick.RemoveAllListeners();
            StoreManager.Instance.buyButton.onClick.AddListener(OnBuyButtonClick);
            background.color = Color.grey;
        }
        else
        {
            StoreManager.Instance.buyButton.onClick.RemoveAllListeners();
        }
    }


}
