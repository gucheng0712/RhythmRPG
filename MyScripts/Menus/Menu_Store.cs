using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Menus
{
    public class Menu_Store : Menu<Menu_Store>
    {
        public override void OnBackBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, false);
        }

    }
}
