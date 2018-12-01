using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using System;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IPointerClickHandler
{
    [Space(10)]
    [Header("=====Load From Code=====")]
    [Header("=====Serialize for Testing=====")]
    public Instrument instrument;
    public Image itemImage;// todo encapsulate later
    public Image background;// todo encapsulate later

    protected virtual void Awake()
    {
        background = GetComponent<Image>();
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
    }

    public abstract void OnPointerClick(PointerEventData eventData);

}
