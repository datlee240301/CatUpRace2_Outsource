using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DataView : MonoBehaviour
{
#if UNITY_EDITOR
    public GamePlayData gamplayData;
    [FormerlySerializedAs("accountData")] public SettingData settingData;
    public static DataView Instance;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            gamplayData = SessionPref.GetGamePlayData();
            settingData = SessionPref.GetAccountData();
            DontDestroyOnLoad(gameObject);
        }
    }
#endif
}