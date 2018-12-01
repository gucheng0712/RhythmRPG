using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using Menus;
using UnityEngine.UI;
using System;

public class LegManManager : GameManager<LegManManager>
{
    public GameObject instrumentToSellPrefab; // todo encapsulate load from resource

    [Space(10)]
    [Header("=====Load From Code=====")]
    [Header("=====Serialize for Testing=====")]

    [SerializeField] Transform m_slotsParent; // todo encapsulate later
    [SerializeField] Transform m_head;
    [SerializeField] float m_moveDist;

    [SerializeField] Transform m_legManMsgPanel;
    [SerializeField] Text m_legManMsg;
    [SerializeField] string m_legManInitialMsg;

    Queue<Transform> slots = new Queue<Transform>();

    public bool IsLerpingToNextSelection { get; set; }

    public Button yesBtn;
    public Button noBtn;

    protected override void Awake()
    {
        base.Awake();
        m_slotsParent = GameObject.FindGameObjectWithTag("ItemToSellSlotParent").transform;
        m_legManMsgPanel = transform.Find("LegManMsg");
        m_legManMsg = m_legManMsgPanel.GetComponentInChildren<Text>();

    }

    public void OnEnable()
    {
        UpdateOwnedItem();

        if (m_legManMsg != null)
        {
            m_legManMsg.text = m_legManInitialMsg;
        }
        yesBtn = m_legManMsgPanel.Find("YesBtn").GetComponent<Button>();
        noBtn = m_legManMsgPanel.Find("NoBtn").GetComponent<Button>();
        SetBtnsActive(false);
    }


    private void Update()
    {
        MoveToNextSelection();
    }

    void MoveToNextSelection()
    {
        if (IsLerpingToNextSelection == true)
        {
            foreach (var s in slots)
            {
                float targetX = s.localPosition.x - m_moveDist;
                float newX = Mathf.Lerp(s.localPosition.x, targetX, Time.unscaledDeltaTime * 10f);
                s.localPosition = new Vector3(newX, 0, 0);
            }

            m_head = slots.Peek();

            // if the x position <= snapDistance, snap it to the correct position
            float snapDistance = -m_moveDist - 10;
            if (m_head.localPosition.x <= snapDistance)
            {
                IsLerpingToNextSelection = false;
                slots.Dequeue();
                m_head.SetParent(null);
                m_head.localPosition = Vector3.zero;
                slots.Enqueue(m_head);
                m_head.SetParent(m_slotsParent);
                m_head.localScale = Vector3.one;
            }
        }
    }

    public void UpdateOwnedItem()
    {
        // Clear the children of the parent 
        foreach (Transform child in m_slotsParent)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();

        IsLerpingToNextSelection = false;
        foreach (var i in GameManager_Data.Instance.unlockedInstruments)
        {
            GameObject newPrefab = Instantiate(instrumentToSellPrefab, m_slotsParent);
            Image itemImage = newPrefab.GetComponent<Image>();
            itemImage.sprite = i.icon;
            itemImage.transform.GetComponent<Item>().instrument = i;
            slots.Enqueue(newPrefab.transform);
        }

        if (slots.Count > 0)
        {
            m_head = slots.Peek();
            m_moveDist = m_head.GetComponent<RectTransform>().rect.width + m_slotsParent.GetComponent<HorizontalLayoutGroup>().spacing;
        }
    }

    // Remove the item from the inventory 
    public void RemoveItem(Instrument itemToRemove)
    {
        GameManager_Data.Instance.unlockedInstruments.Remove(itemToRemove);
        if (GameManager_Data.Instance.selectedInstrument == itemToRemove)
        {
            GameManager_Data.Instance.selectedInstrument = GameManager_Data.Instance.unlockedInstruments[0];
        }
    }

    #region UpdateUI

    public void ShowAndUpdateLegManMsgUI(string price)
    {
        m_legManMsgPanel.gameObject.SetActive(true);
        m_legManMsg.text = "This Instrument only worth " + price +
            "$. Do you still want to sell it?";
    }

    public void ShowSoldItemMsgUI(Instrument instrument)
    {
        m_legManMsg.text = instrument.soldMsg;
        SetBtnsActive(false);
    }

    public void HideLegManMsgUI()
    {
        m_legManMsgPanel.gameObject.SetActive(false);
    }

    public void SetBtnsActive(bool _active)
    {
        yesBtn.gameObject.SetActive(_active);
        noBtn.gameObject.SetActive(_active);
    }
    #endregion

    public void OnNoBtnPressed()
    {
        HideLegManMsgUI();
    }

}
