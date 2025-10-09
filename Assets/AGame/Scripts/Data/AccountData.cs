using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AccountData
{
    public int goldWallet;
    public int watchInterAdsCount;
    public int watchRewardAdsCount;
    public bool isFirstOpenGame;
    public string appVersion;
    public int currentLevel;

    public int lastDayGetReward;
    public int lastDayGetRewardX2;
    public int countDayGetReward;
    public int PlayerID;

    public AccountData()
    {
        this.goldWallet = 0;

        this.watchInterAdsCount = 0;
        this.watchRewardAdsCount = 0;
        this.isFirstOpenGame = true;
        this.appVersion = "";
        currentLevel = 1;
        lastDayGetReward = 0;
        lastDayGetRewardX2 = 0;
        countDayGetReward = 0;
        PlayerID = 1;
    }
}
