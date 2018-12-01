using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.UI;


namespace Menus
{
    public abstract class Menu : MonoBehaviour
    {
        Canvas m_canvas;

        protected virtual void OnEnable()
        {
            m_canvas = GetComponent<Canvas>();
            m_canvas.renderMode = RenderMode.ScreenSpaceCamera;
            m_canvas.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        public virtual void OnBackBtnPressed()
        {
            GameManager_Audio.Instance.PlayBtnSound();
            GameManager_Menu.Instance.CloseMenu();
        }

        public virtual void OpenMainMapMenuByKey()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager_Menu.Instance.OpenMenu(Menu_MainMap.Instance);
                GameManager_Menu.Instance.TransformGameState();
            }
        }


    }


    // Reference Unite Europe 2017 - Building an easy to use menu system
    //https://www.youtube.com/watch?v=wbmjturGbAQ
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = (T)this;
            }
        }
        protected virtual void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}

