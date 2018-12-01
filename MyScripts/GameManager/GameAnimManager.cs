using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using Menus;
using UnityEngine.UI;

public class GameAnimManager : GameManager<LobbyManager>
{
    public GameObject instrumentSelectSlotPrefab; // todo encapsulate load from resource

    [Space(10)]
    [Header("=====Load From Code=====")]
    [Header("=====Serialize for Testing=====")]

    [SerializeField] Image InstrumentIconWithPlayer;

    [SerializeField] Transform m_slotsParent; // todo encapsulate later
    [SerializeField] Transform m_head;
    [SerializeField] float m_moveDist;


    Queue<Transform> slots = new Queue<Transform>();

    public Text lobbyName;
    public Image lobbyBG;
    public Transform playerWithSelectedInstrument;
    public bool IsLerpingToNextSelection { get; set; }

    protected override void Awake()
    {
        base.Awake();
        m_slotsParent = GameObject.FindGameObjectWithTag("ItemSelectionSlotParent").transform;
        lobbyName = transform.Find("LobbyName").GetComponent<Text>();
        lobbyBG = transform.Find("Envt").GetComponent<Image>();
        playerWithSelectedInstrument = transform.Find("PlayerWithSelectedInstrument");
    }

    public void OnEnable()
    {
        InitializeInstrumentSelectionSlots();
    }

    public void InitializeInstrumentSelectionSlots()
    {
        InstrumentIconWithPlayer = transform.Find("PlayerWithSelectedInstrument").GetComponent<Image>();

        UpdatePlayerIcon();

        // Clear the children of the parent 
        foreach (Transform child in m_slotsParent)
        {
            Destroy(child.gameObject);
        }
        slots.Clear();

        IsLerpingToNextSelection = false;
        foreach (var i in GameManager_Data.Instance.unlockedInstruments)
        {
            GameObject newPrefab = Instantiate(instrumentSelectSlotPrefab, m_slotsParent);
            Image itemImage = newPrefab.GetComponent<Image>();
            itemImage.sprite = i.icon;
            itemImage.transform.GetComponent<Item>().instrument = i;
            slots.Enqueue(newPrefab.transform);
        }

        if (GameManager_Data.Instance.selectedInstrument != null)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Transform slot = slots.Peek();
                if (slot.GetComponent<Item>().instrument == GameManager_Data.Instance.selectedInstrument)
                {
                    break;
                }
                slots.Dequeue();
                slot.SetParent(null);
                slot.localPosition = Vector3.zero;
                slots.Enqueue(slot);
                slot.SetParent(m_slotsParent);
                slot.localScale = Vector3.one;
            }
        }
        if (slots.Count > 0)
        {
            m_head = slots.Peek();
            m_moveDist = m_head.GetComponent<RectTransform>().rect.width + m_slotsParent.GetComponent<HorizontalLayoutGroup>().spacing;

            ScaleSelectedInstrument();
        }

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
                float newX = Mathf.Lerp(s.localPosition.x, targetX, Time.deltaTime * 10f);
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
                ScaleSelectedInstrument();
            }
            UpdatePlayerIcon();
        }
    }

    public void ScaleSelectedInstrument()
    {
        m_head = slots.Peek();
        m_head.transform.localScale = Vector3.one * 1.3f;
        GameManager_Data.Instance.selectedInstrument = m_head.GetComponent<Item>().instrument;
    }

    private void UpdatePlayerIcon()
    {
        if (GameManager_Data.Instance.selectedInstrument != null)
        {
            InstrumentIconWithPlayer.sprite = GameManager_Data.Instance.selectedInstrument.iconWithPlayer;
        }
        else
        {
            InstrumentIconWithPlayer = null;
        }
    }

}
