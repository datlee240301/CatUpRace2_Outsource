using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SessionPref
{
    public static GamePlayData _gamePlayData;
    public static SettingData SettingData;

    public static void ReadData()
    {
        if (PlayerPrefs.HasKey(DataConstance.GAMEPLAY_DRAW_DATA_KEY) && PlayerPrefs.HasKey(DataConstance.ACCOUNT_DRAW_DATA_KEY))
        {
            string gamePlayDataString = PlayerPrefs.GetString(DataConstance.GAMEPLAY_DRAW_DATA_KEY);
            _gamePlayData = JsonUtility.FromJson<GamePlayData>(gamePlayDataString);

            string accountDataString = PlayerPrefs.GetString(DataConstance.ACCOUNT_DRAW_DATA_KEY);
            SettingData = JsonUtility.FromJson<SettingData>(accountDataString);

            if(AppVerSion == "")
            {
                AppVerSion = Application.version;
            }
            else
            {
                if (AppVerSion != Application.version)
                {
                    //FirebaseController.Instance.topicNeedUnsubscribed = AppVerSion;
                }
            }
        }
        else
        {
            _gamePlayData = new();
            SettingData = new();
            SaveData();
        }
    }

    public static int GetGoldRemaining
    {
        get => _gamePlayData.Gold;
        set
        {
            _gamePlayData.Gold = value;
            EventDispatcher.PostEvent(EventID.AddCoin, value);
            SaveData();
        }
    }

    public static void AddGoldRemaining(int number)
    {
        _gamePlayData.Gold += number;
        EventDispatcher.PostEvent(EventID.AddCoin,number);
        SaveData();
    }

    public static int CurrentItemInUse
    {
        get => _gamePlayData.currentItemInUse;
        set
        {
            _gamePlayData.currentItemInUse = value;
            SaveData();
        }
    }

    public static List<int> GetListItemsBought()
    {
        return _gamePlayData.lstItemBought;
    }

    public static void AddItemBought(int id)
    {
        if (!_gamePlayData.lstItemBought.Contains(id))
        {
            _gamePlayData.lstItemBought.Add(id);
            SaveData();
        }
    }

    public static bool IsOpenFollowUs
    {
        get => _gamePlayData.isOpenFollowUs;
        set
        {
            _gamePlayData.isOpenFollowUs = value;
            SaveData();
        }
    }

    public static bool IsCallFirstVideo
    {
        get => _gamePlayData.isCallFirstVideo;
        set
        {
            _gamePlayData.isCallFirstVideo = value;
            SaveData();
        }
    }

    public static string AppVerSion
    {
        get => _gamePlayData.appVersion;
        set
        {
            _gamePlayData.appVersion = value;
            SaveData();
        }
    }

    public static int HighScore
    {
        get => _gamePlayData.HighScore;
        set
        {
            if (value > _gamePlayData.HighScore)
            {
                _gamePlayData.HighScore = value;
                EventDispatcher.PostEvent(EventID.ChangeHighScore, value);
                Debug.Log("set high score: " + value);
            }

            SaveData();
        }
    }

    public static bool GetStateSetting(SettingType type)
    {
        if (type == SettingType.Music)
            return _gamePlayData.SettingDataPref.IsOnMusic;
        else if (type == SettingType.Sound)
            return _gamePlayData.SettingDataPref.IsOnSound;
        else if (type == SettingType.Vibration)
            return _gamePlayData.SettingDataPref.IsOnVibration;
        else
        {
            return false;
        }
    }

    public static void AddDayPlayedChallenge(int day)
    {
        if (!_gamePlayData.lstDayPlayedChallenge.Contains(day))
        {
            _gamePlayData.lstDayPlayedChallenge.Add(day);
            SaveData();
        }
    }

    public static List<int> GetListDayPlayedChallenge()
    {
        return _gamePlayData.lstDayPlayedChallenge;
    }


    private static void SaveData()
    {
        string gamePlayDataString = JsonUtility.ToJson(_gamePlayData);
        PlayerPrefs.SetString(DataConstance.GAMEPLAY_DRAW_DATA_KEY, gamePlayDataString);

        string accountString = JsonUtility.ToJson(SettingData);
        PlayerPrefs.SetString(DataConstance.ACCOUNT_DRAW_DATA_KEY, accountString);
    }

    public static bool IsFirstOpenGame
    {
        get => SettingData.IsFirstOpenGame;
        set
        {
            SettingData.IsFirstOpenGame = value;
            SaveData();
        }
    }

    // public static LanguageType LanguageUserType
    // {
    //     get => _gamePlayData.AccountDataPref.languageType;
    //     set
    //     {
    //         _gamePlayData.AccountDataPref.languageType = value;
    //         EventDispatcher.PostEvent(EventID.UpdateLanguage);
    //         SaveData();
    //     }
    // }

    public static int LastDayGetReward
    {
        get => _gamePlayData.AccountDataPref.lastDayGetReward;
        set
        {
            _gamePlayData.AccountDataPref.lastDayGetReward = value;
            SaveData();
        }
    }

    public static int PlayerID
    {
        get => _gamePlayData.AccountDataPref.PlayerID;
        set
        {
            _gamePlayData.AccountDataPref.PlayerID = value;
            SaveData();
        }
    }

    public static int LastDayGetRewardX2
    {
        get => _gamePlayData.AccountDataPref.lastDayGetRewardX2;
        set
        {
            _gamePlayData.AccountDataPref.lastDayGetRewardX2 = value;
            SaveData();
        }
    }

    public static int CountDayGetReward
    {
        get => _gamePlayData.AccountDataPref.countDayGetReward;
        set
        {
            _gamePlayData.AccountDataPref.countDayGetReward = value;
            SaveData();
        }
    }

    public static SettingData GetAccountData()
    {
        return SettingData;
    }

    public static int GetHintRemaining()
    {
        return _gamePlayData.HintRemaining;
    }

    public static int StarRemaining
    {
        get => _gamePlayData.StarRemaining;
        set
        {
            _gamePlayData.StarRemaining = value;
            SaveData();
        }
    }

    public static void SetHintRemaining(int number)
    {
        _gamePlayData.HintRemaining = number;
        SaveData();
    }

    public static void AddHintRemaining(int number)
    {
        _gamePlayData.HintRemaining += number;
        SaveData();
    }

    public static bool GetSettingType(SettingType type)
    {
        if (type == SettingType.Music)
        {
            return SettingData.IsOnMusic;
        }
        else if (type == SettingType.Sound)
        {
            return SettingData.IsOnSound;
        }
        else
        {
            return SettingData.IsOnVibration;
        }
    }

    public static void ChangeSettingState(SettingType type)
    {
        switch (type)
        {
            case SettingType.Music:
                SettingData.IsOnMusic = !SettingData.IsOnMusic;
                break;
            case SettingType.Sound:
                SettingData.IsOnSound = ! SettingData.IsOnSound;
                break;
            case SettingType.Vibration:
                SettingData.IsOnVibration = !SettingData.IsOnVibration;
                break;
        }

        SaveData();
    }

    public static GamePlayData GetGamePlayData()
    {
        return _gamePlayData;
    }

    public static bool IsRemoveAds()
    {
        return SettingData.IsRemoveAds;
    }

    public static void SetRemoveAds(bool isRemoveAds)
    {
        SettingData.IsRemoveAds = isRemoveAds;
        SaveData();
    }

    public static int GetCurrentLevel()
    {
        return _gamePlayData.CurrentLevel;
    }

    public static int MaxLevelHasPassed()
    {
        return _gamePlayData.maxLevelHasPassed;
    }

    public static void SetCurrentLevel(int level)
    {
        _gamePlayData.CurrentLevel = level;
        if (level > _gamePlayData.maxLevelHasPassed)
        {
            _gamePlayData.maxLevelHasPassed = level;
        }
        SaveData();
    }
}