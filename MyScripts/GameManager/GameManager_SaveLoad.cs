using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

namespace GameManager
{
    public class GameManager_SaveLoad : GameManager<GameManager_SaveLoad>
    {

        [SerializeField] string filePath = "/Data.json";

        protected override void Awake()
        {
            base.Awake();

            filePath = Application.streamingAssetsPath + filePath;
            DontDestroyOnLoad(gameObject);
        }

        public void NewGame()
        {
            SaveData saveData = ResetSavedData();
            string saveJsonStr = JsonMapper.ToJson(saveData);
            //if (GameManager_Data.Instance.selectedInstrument != null)
            //{
            //    GameManager_Data.Instance.unlockedInstruments.Add(GameManager_Data.Instance.selectedInstrument);
            //    GameManager_Data.Instance.storeStock.Remove(GameManager_Data.Instance.selectedInstrument);
            //}

            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            LoadGame();
        }
        SaveData ResetSavedData()
        {
            SaveData saveData = new SaveData();

            GameManager_Data.Instance.unlockedInstruments.Clear();
            return saveData;
        }
        public void CheckIfHasDatainFile()
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string loadJsonStr = sr.ReadToEnd();
                sr.Close();
                GameManager_Data.Instance.isFirstTimePlay = (loadJsonStr.Length < 10);
            }
        }


        #region Save Functions
        public void SaveGame()
        {
            SaveData saveData = OverrideSavedData();
            string saveJsonStr = JsonMapper.ToJson(saveData);

            StreamWriter sw = new StreamWriter(filePath);
            sw.Write(saveJsonStr);
            sw.Close();
            Debug.Log("Save Successfully");
        }

        SaveData OverrideSavedData()
        {
            // Create a new instance of SaveData
            SaveData saveData = new SaveData();

            // Save Key Bindings
            OverrideKeyBindingsData(saveData);
            // Save Owned Instrument
            OverrideInstrumentData(saveData);

            // Save Post History (neeed to be ordered)
            OverridePostHistoryData(saveData);

            // Save Owned Money
            saveData.Money = GameManager_Data.Instance.Money;

            // Save Owned Groupy
            saveData.PhraseOneGroupy = GameManager_Data.Instance.groupyData.PhraseOneNum;
            saveData.PhraseTwoGroupy = GameManager_Data.Instance.groupyData.PhraseTwoNum;
            saveData.PhraseThreeGroupy = GameManager_Data.Instance.groupyData.PhraseThreeNum;
            saveData.BarPeopleNum = GameManager_Data.Instance.groupyData.BarPeopleNum;
            saveData.RestaurantPeopleNum = GameManager_Data.Instance.groupyData.RestaurantPeopleNum;
            saveData.ClubPeopleNum = GameManager_Data.Instance.groupyData.ClubPeopleNum;
            saveData.MusicFestivalPeopleNum = GameManager_Data.Instance.groupyData.MusicFesitivalPeopleNum;
            saveData.BarHighScore = GameManager_Data.Instance.BarHighScore;
            saveData.RestaurantHighScore = GameManager_Data.Instance.RestaurantHighScore;
            saveData.ClubHighScore = GameManager_Data.Instance.ClubHighScore;
            saveData.MusicFestivalHighScore = GameManager_Data.Instance.MusicFestivalHighScore;

            return saveData;
        }

        // Save Owned Instrument
        void OverrideInstrumentData(SaveData _saveData)
        {
            if (_saveData.instrumentsID.Count > 0) _saveData.instrumentsID.Clear();

            foreach (Instrument i in GameManager_Data.Instance.unlockedInstruments)
            {
                _saveData.instrumentsID.Add(i.id);
            }
        }

        // Save Post History (neeed to be ordered)
        void OverridePostHistoryData(SaveData _saveData)
        {
            if (_saveData.tweetHistorySaveData.Count > 0) _saveData.tweetHistorySaveData.Clear();
            if (_saveData.playerPostFlagSaveData.Count > 0) _saveData.playerPostFlagSaveData.Clear();

            for (int i = 0; i < GameManager_Data.Instance.tweetsHistory.Count; i++)
            {
                // Add them History into the tweetHistorySaveData
                _saveData.tweetHistorySaveData.Add(GameManager_Data.Instance.tweetsHistory[i]);

                _saveData.playerPostFlagSaveData.Add(GameManager_Data.Instance.playerPostHistoryFlag[i]);

            }
        }

        void OverrideKeyBindingsData(SaveData _saveData)
        {
            if (_saveData.keysData.Count > 0) _saveData.keysData.Clear();

            foreach (KeyValuePair<string, KeyCode> key in GameManager_Data.Instance.keys)
            {
                _saveData.keysData.Add(key.Key, key.Value);
            }
        }
        #endregion

        #region Load Functions
        public void LoadGame()
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string loadJsonStr = sr.ReadToEnd();
                sr.Close();

                SaveData saveData = JsonMapper.ToObject<SaveData>(loadJsonStr);
                SetUpGameData(saveData);
                Debug.Log("Successfully Load Game");
            }
            else
            {
                Debug.Log("Fail to Load Game, didn't found the saved file");
            }

        }

        void SetUpGameData(SaveData _savedData)
        {
            SetupInstrumentsData(_savedData);
            SetupTweetsData(_savedData);
            SetupKeyControlData(_savedData);
            SetupHighScore(_savedData);
        }

        void SetupHighScore(SaveData _savedData)
        {
            GameManager_Data.Instance.BarHighScore = _savedData.BarHighScore;
            GameManager_Data.Instance.RestaurantHighScore = _savedData.RestaurantHighScore;
            GameManager_Data.Instance.ClubHighScore = _savedData.ClubHighScore;
            GameManager_Data.Instance.MusicFestivalHighScore = _savedData.MusicFestivalHighScore;
        }

        void SetupGroupyData(SaveData _savedData)
        {
            GameManager_Data.Instance.Money = _savedData.Money;
            GameManager_Data.Instance.groupyData.PhraseOneNum = _savedData.PhraseOneGroupy;
            GameManager_Data.Instance.groupyData.PhraseTwoNum = _savedData.PhraseTwoGroupy;
            GameManager_Data.Instance.groupyData.PhraseThreeNum = _savedData.PhraseThreeGroupy;
            GameManager_Data.Instance.groupyData.BarPeopleNum = _savedData.BarPeopleNum;
            GameManager_Data.Instance.groupyData.RestaurantPeopleNum = _savedData.RestaurantPeopleNum;
            GameManager_Data.Instance.groupyData.ClubPeopleNum = _savedData.ClubPeopleNum;
            GameManager_Data.Instance.groupyData.MusicFesitivalPeopleNum = _savedData.MusicFestivalPeopleNum;
        }

        void SetupInstrumentsData(SaveData _savedData)
        {
            if (GameManager_Data.Instance.storeStock.Count > 0)
                GameManager_Data.Instance.storeStock.Clear();
            for (int i = 0; i < GameManager_Data.Instance.instrumentDataBase.Count; i++)
            {
                GameManager_Data.Instance.storeStock.Add(GameManager_Data.Instance.instrumentDataBase[i]);
            }

            if (GameManager_Data.Instance.unlockedInstruments.Count > 0)
                GameManager_Data.Instance.unlockedInstruments.Clear();
            for (int i = 0; i < _savedData.instrumentsID.Count; i++)
            {
                int id = _savedData.instrumentsID[i];
                Instrument ownedInstrument = GameManager_Data.Instance.FindItemInDataBase(id);
                GameManager_Data.Instance.unlockedInstruments.Add(ownedInstrument);

                // Remove the owned item from the store
                if (GameManager_Data.Instance.storeStock.Contains(ownedInstrument))
                    GameManager_Data.Instance.storeStock.Remove(ownedInstrument);
            }
        }

        void SetupKeyControlData(SaveData _savedData)
        {
            if (GameManager_Data.Instance.keys.Count > 0) GameManager_Data.Instance.keys.Clear();

            foreach (KeyValuePair<string, KeyCode> key in _savedData.keysData)
            {
                GameManager_Data.Instance.keys.Add(key.Key, key.Value);
            }
        }

        void SetupTweetsData(SaveData _savedData)
        {
            if (GameManager_Data.Instance.tweetsHistory.Count > 0)
                GameManager_Data.Instance.tweetsHistory.Clear();
            if (GameManager_Data.Instance.playerPostHistoryFlag.Count > 0)
                GameManager_Data.Instance.playerPostHistoryFlag.Clear();




            foreach (var history in _savedData.tweetHistorySaveData)
            {
                GameManager_Data.Instance.tweetsHistory.Insert(0, history);
            }

            foreach (var flag in _savedData.playerPostFlagSaveData)
            {
                GameManager_Data.Instance.playerPostHistoryFlag.Insert(0, flag);
            }
        }

        #endregion 

    }
}