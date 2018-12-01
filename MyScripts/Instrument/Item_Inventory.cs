using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.EventSystems;

public class Item_Inventory : Item
{

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (instrument != null && itemImage.sprite == instrument.unlockedIcon)
        {
            GameManager_Audio.Instance.PlayBtnSound();
            InventoryManager.Instance.UpdateDescriptionUI(instrument);
        }
    }


}
