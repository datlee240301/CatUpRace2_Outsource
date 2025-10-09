using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayData
{
    public int CurrentLevel;
    public int HintRemaining;
    public int maxLevelHasPassed;
    public string appVersion;
    public AccountData AccountDataPref;
    public int StarRemaining;
    public List<int> lstDayPlayedChallenge;
    public bool isOpenFollowUs;
    public bool isCallFirstVideo;
    public int HighScore;
    public int Gold;
    public SettingData SettingDataPref;
    public List<int> lstItemBought;
    public int currentItemInUse;
    
    public GamePlayData()
    {
        CurrentLevel = 0;
        HintRemaining = 1;
        StarRemaining = 0;
        maxLevelHasPassed = 0;
        this.appVersion = "";
        isOpenFollowUs = false;
        isCallFirstVideo = false;
        lstDayPlayedChallenge = new List<int>();
        AccountDataPref = new AccountData();
        HighScore = 0;
        Gold = 0;
        SettingDataPref = new SettingData();

        lstItemBought = new List<int>{0};
        currentItemInUse = 0;
    }
}
