using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameManager;
using UnityEngine.UI;

public class Item_Selection : Item
{
    protected override void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //LobbyManager.Instance.UpdateBackgroundImage();
        //background.color = Color.grey;
        //GameManager_Data.Instance.selectedInstrument = instrument;

    }




}
