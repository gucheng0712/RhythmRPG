using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Menus;
using System;

namespace GameManager
{
    public class InventoryManager : GameManager<InventoryManager>
    {
        const int SLOTSNUM = 16;
        [SerializeField] Text m_itemDescription;
        [SerializeField] Text m_itemStatus;

        [Space(10)]
        [Header("=====Load From Code=====")]
        [Header("=====Serialize for Testing=====")]
        [SerializeField] Transform m_bag;
        [SerializeField] Image[] m_itemImages = new Image[SLOTSNUM];
        [SerializeField] Instrument[] m_items = new Instrument[SLOTSNUM];

        #region MonoBehavoirMethods

        // Initialize the Inventory at OnEnable() Method to prevent the NullReferenceException
        private void OnEnable()
        {
            m_bag = transform.Find("Bag");
            for (int i = 0; i < SLOTSNUM; i++)
            {
                m_items[i] = GameManager_Data.Instance.instrumentDataBase[i];
                m_itemImages[i] = m_bag.GetChild(i).GetChild(0).GetComponent<Image>();
                m_itemImages[i].transform.parent.GetComponent<Item>().instrument =
                                   GameManager_Data.Instance.instrumentDataBase[i];
            }
        }
        #endregion


        #region Inventory Functions

        public void UpdateInventoryItem()
        {
            for (int i = 0; i < SLOTSNUM; i++)
            {

                m_itemImages[i].sprite = GameManager_Data.Instance.unlockedInstruments.Contains(m_items[i]) ?
                    GameManager_Data.Instance.instrumentDataBase[i].unlockedIcon
                    :
                    m_itemImages[i].sprite = GameManager_Data.Instance.instrumentDataBase[i].lockedIcon;

            }
        }

        // TODO discard (Delete later) Has changed to LockandUnlock Style Inventory Based on the Sprites
        //
        //// Clear the Inventory before you add item into it
        //// Called in UpdateInventoryItem()
        //public void ClearItemByIndex(int i)
        //{
        //    m_items[i] = null;
        //    m_itemImages[i].sprite = null;
        //    m_itemImages[i].enabled = false;
        //    m_itemImages[i].transform.parent.GetComponent<Item>().instrument = null;
        //}

        //// Add item into the inventory
        //public void AddItem(Instrument itemToAdd)
        //{
        //    for (int i = 0; i < m_items.Length; i++)
        //    {
        //        if (m_items[i] == null)
        //        {
        //            m_items[i] = itemToAdd;
        //            m_itemImages[i].sprite = itemToAdd.icon;
        //            m_itemImages[i].enabled = true;
        //            m_itemImages[i].transform.parent.GetComponent<Item>().instrument = itemToAdd;
        //            // todo add animation
        //            return;
        //        }
        //    }
        //}

        #endregion


        // todo maybe sepreate into another class as "View"
        #region UI Update  
        // Update the DescriptionUI when you click the item
        // Be called in other script
        public void UpdateDescriptionUI(Instrument instrument)
        {
            m_itemDescription.text = instrument.itemName + "\n\n" + instrument.description;
            m_itemStatus.text = GetItemStatus.TotalBonusText(instrument);
        }
        #endregion
    }

}

