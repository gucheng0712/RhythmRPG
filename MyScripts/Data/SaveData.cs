using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public List<int> instrumentsID = new List<int>();
    public int Money { get; set; }
    public int PhraseOneGroupy { get; set; } // todo save and load
    public int PhraseTwoGroupy { get; set; }
    public int PhraseThreeGroupy { get; set; }

    public int BarPeopleNum { get; set; }
    public int RestaurantPeopleNum { get; set; }
    public int ClubPeopleNum { get; set; }
    public int MusicFestivalPeopleNum { get; set; }

    public int BarHighScore { get; set; }
    public int RestaurantHighScore { get; set; }
    public int ClubHighScore { get; set; }
    public int MusicFestivalHighScore { get; set; }

    public List<string> tweetHistorySaveData = new List<string>();
    public List<bool> playerPostFlagSaveData = new List<bool>();

    public Dictionary<string, KeyCode> keysData = new Dictionary<string, KeyCode>();


    public SaveData()
    {
        // Dictionary key need to match the key gameobject name in the Hierarchy
        keysData.Add("InteractKey", KeyCode.W);
        keysData.Add("MoveRightKey", KeyCode.D);
        keysData.Add("MoveLeftKey", KeyCode.A);
        keysData.Add("Note1Key", KeyCode.F);
        keysData.Add("Note2Key", KeyCode.G);
        keysData.Add("Note3Key", KeyCode.H);
        keysData.Add("Note4Key", KeyCode.J);
    }

}
