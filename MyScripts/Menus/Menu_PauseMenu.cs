using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Menus
{
    public class Menu_PauseMenu : Menu<Menu_PauseMenu>
    {

        public override void OnBackBtnPressed()
        {
            GameManager_Menu.Instance.TransformGameState();
        }

        // todo add other btn events 

    }

}
