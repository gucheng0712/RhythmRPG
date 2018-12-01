using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameManager;
using Menus;
using UnityEngine.UI;

public class Item_ToSell : Item
{
    protected override void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (instrument != null)
        {
            GameManager_Audio.Instance.PlayBtnSound();
            LegManManager.Instance.SetBtnsActive(true);
            int sellPrice = (int)(instrument.price * 0.5f);
            LegManManager.Instance.ShowAndUpdateLegManMsgUI(sellPrice.ToString());
            LegManManager.Instance.yesBtn.onClick.RemoveAllListeners();
            LegManManager.Instance.yesBtn.onClick.AddListener(OnYesBtnPressed);
        }
        else
        {
            LegManManager.Instance.yesBtn.onClick.RemoveAllListeners();
        }
    }

    public void OnYesBtnPressed()
    {
        GameManager_Audio.Instance.PlayBtnSound();
        if (GameManager_Data.Instance.unlockedInstruments.Count <= 1)
        {
            Menu_MainMap_LegMan.Instance.notificationFader.ShowNotificationPanel();
            return;
        }
        GameManager_Data.Instance.storeStock.Add(instrument);
        GameManager_Data.Instance.Money += instrument.price / 2;
        LegManManager.Instance.ShowSoldItemMsgUI(instrument);
        LegManManager.Instance.RemoveItem(instrument);
        LegManManager.Instance.UpdateOwnedItem();

    }


}
