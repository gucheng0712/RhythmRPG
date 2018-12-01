using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using Menus;
using UnityEngine.SceneManagement;

public enum OpenPeriod
{
    Day,
    Night,
    AlwaysOpen
}

[CreateAssetMenu(fileName = "New Interactable", menuName = "Interactable")]
public class Interactables : ScriptableObject
{
    public new string name;
    public string type;
    public string destination;
    public string interaction;
    public SongData venueSongData;
    public SongDataBusking buskingSongData;
    public SongDataGhostBusking ghostBuskingSongData;
    public int requiredGroupyNum;
    public int baseGoldReward;
    public string closedNotification;
    public string prerequisiteNotification = "Optional";  // Optional
    public Sprite bg;
    public Vector3 playerIconPos;
    public Color venueColor;
    public OpenPeriod openPeriod;



    private Animator anim;

    public void LoadLobby()
    {
        if (HasQualifiedToPerform())
        {
            LevelLoader.LoadLevel("Lobby", Menu_Lobby.Instance);
            Menu_Lobby.Instance.venueToLoad = destination;
            Menu_Lobby.Instance.UI.color = venueColor;
            foreach (var btn in Menu_Lobby.Instance.btns)
            {
                btn.color = venueColor;
            }

            LobbyManager.Instance.lobbyName.text = name;
            LobbyManager.Instance.lobbyBG.sprite = bg;
            ScoreManager.baseVenueReward = baseGoldReward;
            LobbyManager.Instance.playerWithSelectedInstrument.localPosition = playerIconPos;
            GameManager_Data.Instance.selectedSong = venueSongData;
            GameManager_Data.Instance.playerData.playerPerformLocation = name;
        }
        else
        {
            if (GameManager_Data.Instance.groupyData.GroupyNum < requiredGroupyNum && !GameManager_Data.Instance.IsDayTime)
            {
                Menu_MainMap.Instance.notificationFader.msg.text = prerequisiteNotification;
            }
            else if (GameManager_Data.Instance.IsDayTime)
            {
                Menu_MainMap.Instance.notificationFader.msg.text = closedNotification;
            }

            Menu_MainMap.Instance.notificationFader.ShowNotificationPanel();
        }
    }

    public void LoadBusking()
    {
        /*		//LevelLoader.LoadLevel("Lobby", Menu_Lobby.Instance);
                //Menu_Lobby.Instance.venueToLoad = destination;
                LobbyManager.Instance.lobbyName.text = name;
                LobbyManager.Instance.lobbyBG.sprite = bg;
                LobbyManager.Instance.playerWithSelectedInstrument.localPosition = playerIconPos;
        */
        //GameManager_Data.Instance.selectedSong = venueSongData;
        GameManager_Data.Instance.selectedSongBusking = buskingSongData;
        GameManager_Data.Instance.selectedSongGhostBusking = ghostBuskingSongData;
        LevelLoader.LoadLevel(destination, Menu_GamePlay.Instance, GameManager_Menu.Instance.LoadingScreen);
    }


    public void LoadBusking2()
    {
        /*		//LevelLoader.LoadLevel("Lobby", Menu_Lobby.Instance);
		//Menu_Lobby.Instance.venueToLoad = destination;
		LobbyManager.Instance.lobbyName.text = name;
		LobbyManager.Instance.lobbyBG.sprite = bg;
		LobbyManager.Instance.playerWithSelectedInstrument.localPosition = playerIconPos;
		*/
        //GameManager_Data.Instance.selectedSong = venueSongData;
        GameManager_Data.Instance.selectedSongBusking = buskingSongData;
        GameManager_Data.Instance.selectedSongGhostBusking = ghostBuskingSongData;
        LevelLoader.LoadLevel(destination, Menu_GamePlay.Instance, GameManager_Menu.Instance.LoadingScreen);
        GameManager_Data.Instance.selectedSong = venueSongData;
    }



    public void SpeakToNPC(GameObject NPC)
    {
        //NPC.GetComponent<NPCSpeaking>().IsSpeaking();
        //Debug.Log("Talking To NPC");

    }

    public void LoadShop1()
    {
        if (GameManager_Data.Instance.IsDayTime)
        {
            LevelLoader.LoadLevel("Store", Menu_Store.Instance, GameManager_Menu.Instance.LoadingScreen);
            GameManager_Data.Instance.m_storeType = StoreType.StoreOne;
            StoreManager.Instance.FillTheSlots();
        }
        else
        {
            Menu_MainMap.Instance.notificationFader.msg.text = closedNotification;
            Menu_MainMap.Instance.notificationFader.ShowNotificationPanel();
        }
    }

    public void LoadShop2()
    {
        if (GameManager_Data.Instance.IsDayTime)
        {
            LevelLoader.LoadLevel("Store", Menu_Store.Instance, GameManager_Menu.Instance.LoadingScreen);
            GameManager_Data.Instance.m_storeType = StoreType.StoreTwo;
            StoreManager.Instance.FillTheSlots();
        }
        else
        {
            Menu_MainMap.Instance.notificationFader.msg.text = closedNotification;
            Menu_MainMap.Instance.notificationFader.ShowNotificationPanel();
        }
    }

    public void LoadLMShop()
    {
        GameManager_Menu.Instance.OpenMenu(Menu_MainMap_LegMan.Instance);
        LegManManager.Instance.UpdateOwnedItem();
        GameManager_Menu.Instance.TransformGameState();
    }

    public void LoadDrink()
    {
        anim.SetBool("Drink", true);
    }

    // todo Only for testing, remove later
    public void LoadNight()
    {
        LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, true);
    }


    bool HasQualifiedToPerform()
    {
        return GameManager_Data.Instance.groupyData.GroupyNum >= requiredGroupyNum && GameManager_Data.Instance.IsDayTime == false;
    }

    public void LoadFestival()
    {
        //LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, true);
        SceneManager.LoadScene("MusicFestivalMap");
    }
    public void LoadMainMapNight()
    {
        Debug.Log("LoadMainMap");
        LevelLoader.LoadMainMapLevel(GameManager_Menu.Instance.LoadingScreen, false);
        //SceneManager.LoadScene("MainMap_Night");
    }


}
