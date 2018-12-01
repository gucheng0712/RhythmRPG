using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Menus
{
    public class Menu_MainMap_Inventory : Menu<Menu_MainMap_Inventory>
    {

        protected override void OnEnable()
        {

        }

        void Update()
        {
            OpenMainMapMenuByKey();
        }

        public override void OnBackBtnPressed()
        {
            base.OnBackBtnPressed();
            GameManager_Menu.Instance.TransformGameState();
        }

    }
}
