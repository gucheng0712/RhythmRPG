using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameManager
{
    public enum StoreType
    {
        StoreOne,
        StoreTwo
    }
    public class StoreManager : GameManager<StoreManager>
    {
        const int SLOTSNUM = 8;
        [SerializeField] Transform m_shop;
        [SerializeField] Sprite m_soldIcon;  // todo load from resources

        [SerializeField] Sprite storeOneBG;
        [SerializeField] Sprite storeTwoBG;

        [Space(10)]
        [Header("=====Load From Code=====")]
        [Header("=====Serialize for Testing=====")]

        [SerializeField] Image[] m_itemImages = new Image[SLOTSNUM];
        [SerializeField] Image[] m_slots = new Image[SLOTSNUM];

        [SerializeField] Image m_bgImg;
        [SerializeField] Transform m_popUpPanel;
        [SerializeField] Image popUpItemImage;
        [SerializeField] Text m_itemName;
        [SerializeField] Text m_itemDescription;
        [SerializeField] Text m_itemStatus;
        [SerializeField] Text m_itemPrice;
        [SerializeField] Instrument[] m_items = new Instrument[SLOTSNUM];


        public Button buyButton;// todo encapsulate later
        public NotificationFader notificationFader;

        #region Initialization
        void OnEnable()
        {
            InitializeItems();
        }

        void InitializeItems()
        {
            GetComponents();
            FillTheSlots();
        }

        void GetComponents()
        {
            m_bgImg = transform.Find("BG").GetComponent<Image>();

            m_popUpPanel = transform.Find("PopUpPanel");
            popUpItemImage = m_popUpPanel.Find("PopUpImage").GetComponent<Image>();

            buyButton = m_popUpPanel.GetComponentInChildren<Button>();
            m_itemName = m_popUpPanel.Find("ItemName").GetComponent<Text>();
            m_itemDescription = m_popUpPanel.Find("DescriptionText").GetComponent<Text>();
            m_itemStatus = m_popUpPanel.Find("StatusText").GetComponent<Text>();
            m_itemPrice = m_popUpPanel.Find("PriceImg").GetComponentInChildren<Text>();

            Transform[] slots = new Transform[SLOTSNUM];
            for (int i = 0; i < m_shop.childCount; i++)
            {
                slots[i] = m_shop.GetChild(i);
                m_itemImages[i] = slots[i].Find("ItemImage").GetComponent<Image>();
                m_slots[i] = slots[i].GetComponent<Image>();
            }

            notificationFader = GetComponent<NotificationFader>();
            notificationFader.SetAlpha(0);
            m_popUpPanel.gameObject.SetActive(false);
        }

        public void FillTheSlots()
        {
            int startIndex = 0;
            int endIndex = 0;
            switch (GameManager_Data.Instance.m_storeType)
            {
                case StoreType.StoreOne:
                    startIndex = 0;
                    endIndex = SLOTSNUM;
                    m_bgImg.sprite = storeOneBG;
                    break;
                case StoreType.StoreTwo:
                    startIndex = SLOTSNUM;
                    endIndex = SLOTSNUM * 2;
                    m_bgImg.sprite = storeTwoBG;
                    break;
            }

            int slotIndex = 0;
            for (int i = startIndex; i < endIndex; i++)
            {
                Instrument itemToAdd = GameManager_Data.Instance.FindItemInDataBase(i);
                if (GameManager_Data.Instance.storeStock.Contains(itemToAdd))
                {
                    m_items[slotIndex] = itemToAdd;
                    m_itemImages[slotIndex].sprite = m_items[slotIndex].icon;
                    m_itemImages[slotIndex].GetComponentInParent<Item>().instrument = itemToAdd;
                }
                else
                {
                    m_itemImages[slotIndex].sprite = m_soldIcon;
                }
                slotIndex++;
            }


        }
        #endregion


        #region StoreFunctions
        public void SellItem(Instrument itemToSell)
        {
            // fill the item by randomly depends on the stock of instruments in the store
            if (itemToSell == null) return;
            if (MoneyManager.CheckAffordability(itemToSell.price))
            {
                for (int i = 0; i < m_items.Length; i++)
                {
                    if (m_items[i] == itemToSell)
                    {
                        // todo add animation

                        if (m_items[i] != null)
                        {
                            m_itemImages[i].sprite = m_soldIcon;
                            GameManager_Data.Instance.storeStock.Remove(m_items[i]);
                        }
                        m_itemImages[i].GetComponentInParent<Item>().instrument = itemToSell;
                        GameManager_Data.Instance.unlockedInstruments.Add(itemToSell);
                        if (GameManager_Data.Instance.selectedInstrument == null)
                            GameManager_Data.Instance.selectedInstrument = itemToSell;
                        MoneyManager.Instance.ReduceMoney(itemToSell.price);
                        return;
                    }
                }
            }
            Debug.Log("Didn't find the item");
        }

        #endregion


        // todo maybe sepreate into another class as "View"
        #region UI Update
        public void UpdataBackgroundUI()
        {
            for (int i = 0; i < SLOTSNUM; i++)
            {
                m_slots[i].color = Color.white;
                //todo m_slots[i].sprite = (Sprite)Resources.Load(Application.dataPath + "/Sprites/Item_store");
            }
        }

        public void UpdateDescriptionUI(Instrument instrument)
        {
            m_popUpPanel.gameObject.SetActive(true);
            m_itemName.text = instrument.itemName;
            m_itemDescription.text = instrument.description;
            m_itemStatus.text = GetItemStatus.TotalBonusText(instrument);
            m_itemPrice.text = GetItemStatus.PriceText(instrument);
            popUpItemImage.sprite = instrument.icon;
            notificationFader.SetAlpha(0);
        }

        public void HideDescriptionUI()
        {
            m_popUpPanel.gameObject.SetActive(false);
        }



        #endregion


        #region Butten Event
        public void OnCancelBtnClick()
        {
            UpdataBackgroundUI();
            HideDescriptionUI();
        }
        #endregion

    }
}
