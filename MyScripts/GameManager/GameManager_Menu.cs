using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Menus;
namespace GameManager
{
    public enum GameState
    {
        Running = 0,
        Pause = 1
    }
    public class GameManager_Menu : GameManager<GameManager_Menu>
    {
        [SerializeField] LoadingScreen m_loadingScreen;
        public LoadingScreen LoadingScreen { get { return m_loadingScreen; } }
        [SerializeField] Menu[] m_menuPrefabs;// todo load from resources later

        [Space(10)]
        [Header("=====Load From Code=====")]
        [Header("=====Serialize for Testing=====")]
        [SerializeField] Transform m_menuParent; // todo encapsulate later
        [SerializeField] Stack<Menu> m_menuStack = new Stack<Menu>();

        public GameState gameState = GameState.Running;

        #region Initialization

        void Start()
        {
            InitializeMenus();
            DontDestroyOnLoad(gameObject);
        }

        private void InitializeMenus()
        {


            if (m_menuParent == null)
            {
                GameObject menuParent = new GameObject("Menus");
                m_menuParent = menuParent.transform;
            }
            DontDestroyOnLoad(m_menuParent.gameObject);

            foreach (var p in m_menuPrefabs)
            {
                if (p != null)
                {
                    Menu menuInstance = Instantiate(p, m_menuParent);
                    if (p != m_menuPrefabs[0])
                    {
                        menuInstance.gameObject.SetActive(false);
                    }
                    else
                    {
                        OpenMenu(menuInstance);
                    }
                }
            }
        }
        #endregion

        #region Game State Control
        // Switch GameState between Running and Pause
        public void TransformGameState()
        {
            switch (gameState)
            {
                case GameState.Running:
                    Time.timeScale = 0;
                    gameState = GameState.Pause;
                    break;
                case GameState.Pause:
                    Time.timeScale = 1;
                    gameState = GameState.Running;
                    break;
                default:
                    break;
            }
        }
        #endregion


        #region Menu Control
        public void OpenMenu(Menu menuInstance)
        {
            if (m_menuStack.Count > 0)
            {
                foreach (var m in m_menuStack)
                {
                    m.gameObject.SetActive(false);
                }
            }
            menuInstance.gameObject.SetActive(true);
            m_menuStack.Push(menuInstance);
        }

        //public void OpenProfileMenu()
        //{
        //    OpenMenu(Menu_MainMap_Profile.Instance);
        //    Menu_MainMap_Profile.Instance.UpdateInfo();
        //}

        public void CloseMenu()
        {
            if (m_menuStack.Count == 0) { return; }
            Menu topMenu = m_menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if (m_menuStack.Count > 0)
            {
                Menu nextMenu = m_menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
        #endregion
    }
}

