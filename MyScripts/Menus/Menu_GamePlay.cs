using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Menus
{
    public class Menu_GamePlay : Menu<Menu_GamePlay>
    {
        //this is one
        public void OnMainMapBtnPress()
        {
            LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, true);
        }

        // todo other gameplay menu button such as pausemenu

        public void OnPauseBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.OpenMenu(Menu_PauseMenu.Instance);
            GameManager_Menu.Instance.TransformGameState();
        }
    }

}
