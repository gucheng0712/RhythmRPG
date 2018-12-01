using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManager;
using Menus;
using System;

public class LevelLoader : MonoBehaviour
{

    public static void LoadLevel(string levelName, Menu menuInstance)
    {
        if (levelName != null)
        {
            SceneManager.LoadScene(levelName);
            GameManager_Menu.Instance.OpenMenu(menuInstance);
        }
        else
        {
            Debug.LogError("Load Level Error: Can't Find the Scene Name to Load");
        }
    }

    public static void LoadLevel(int levelIndex, Menu menuInstance)
    {
        SceneManager.LoadScene(levelIndex);
        GameManager_Menu.Instance.OpenMenu(menuInstance);
    }

    public static void LoadLevel(string sceneName, Menu menuInstance, LoadingScreen loadingScreen, bool shouldOpenMenu = true)
    {
        if (loadingScreen != null)
        {
            LoadingScreen instance = Instantiate(loadingScreen);
            if (sceneName != null)
            {
                instance.PlayTransition(sceneName);

                if (shouldOpenMenu == true)
                    GameManager_Menu.Instance.OpenMenu(menuInstance);
            }
            else
            {
                Debug.LogError("Load Level Error: Can't Find the Scene Name to Load");
            }
        }
        else
        {
            Debug.LogError("Load Level Error: Need LoadingScreen Component");
        }

    }

    public static void LoadMainMapLevel(bool _isDayNightSwitch)
    {
        if (_isDayNightSwitch == true)
        {
            DayNightSwitch();
        }

        if (GameManager_Data.Instance.IsDayTime)
        {
            if (_isDayNightSwitch == true)
            {
                GameManager_Data.Instance.HasPublishedPost = false;
            }
            SceneManager.LoadScene("MainMap_Day");
        }
        else
        {
            SceneManager.LoadScene("MainMap_Night");
        }
        GameManager_Menu.Instance.OpenMenu(Menu_MainMap.Instance);
    }

    public static void LoadMainMapLevel(LoadingScreen loadingScreen, bool _isDayNightSwitch)
    {
        if (loadingScreen == null)
        {
            Debug.LogError("Load Level Error: Need LoadingScreen Component");
            return;
        }

        if (_isDayNightSwitch == true)
        {
            DayNightSwitch();
        }
        LoadingScreen instance = Instantiate(loadingScreen);
        if (GameManager_Data.Instance.IsDayTime)
        {
            if (_isDayNightSwitch == true)
            {
                GameManager_Data.Instance.HasPublishedPost = false;
            }
            instance.PlayTransition("MainMap_Day");
        }
        else
        {
            instance.PlayTransition("MainMap_Night");
        }

        GameManager_Menu.Instance.OpenMenu(Menu_MainMap.Instance);
    }

    public static void BackToMainMapFromMusicFestival(LoadingScreen loadingScreen)
    {
        if (loadingScreen == null)
        {
            Debug.LogError("Load Level Error: Need LoadingScreen Component");
            return;
        }
        LoadingScreen instance = Instantiate(loadingScreen);
        instance.PlayTransition("MainMap_Night");
        GameManager_Menu.Instance.OpenMenu(Menu_MainMap.Instance);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(214f, -14.82f, -0.13f);
    }


    public static void DayNightSwitch()
    {
        GameManager_Data.Instance.IsDayTime = !GameManager_Data.Instance.IsDayTime;
    }




}
