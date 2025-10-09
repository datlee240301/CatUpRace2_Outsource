using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingData
{
    public bool IsOnMusic;
    public bool IsOnSound;
    public bool IsOnVibration;

    public bool IsRemoveAds;
    public bool IsFirstOpenGame;

    public SettingData()
    {
        IsOnMusic = true;
        IsOnSound = true;
        IsOnVibration = true;

        IsRemoveAds = false;
        IsFirstOpenGame = true;
    }
}

public enum SettingType
{
    Music,
    Sound,
    Vibration
}
