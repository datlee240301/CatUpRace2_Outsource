using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    // [SerializeField] private Button instaBtn;
    // [SerializeField] private Button fbBtn;
    // [SerializeField] private Button tiktokBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;
    [SerializeField] private float musicVolumeDefault = 1;
    [SerializeField] private float musicVolumeInHome = 0.6f;

    [SerializeField] private Button soundBtn;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;

    [SerializeField] private Button vibrateBtn;
    [SerializeField] private Sprite vibrateOn;
    [SerializeField] private Sprite vibrateOff;

    [SerializeField] private GameObject settingPanel;
    [SerializeField] private Button restorePurchaseBtn;

    [SerializeField] private Text currentLanguageTxt;

    [SerializeField] private string instaLink;
    [SerializeField] private string fbLink;
    [SerializeField] private string tiktokLink;

    private void Awake()
    {
        //settingBtn.onClick.AddListener(OnClickSettingBtn);
        //quitBtn.onClick.AddListener(Application.Quit);
        // restorePurchaseBtn.onClick.AddListener(() =>
        // {
        //     //IAPManager.Instance.RestorePurchase();
        // });
        // instaBtn.onClick.AddListener(() =>
        // {
        //     OnClickShareBtn(instaBtn, instaLink);
        // });
        // fbBtn.onClick.AddListener(() =>
        // {
        //     OnClickShareBtn(fbBtn, fbLink);
        // });
        // tiktokBtn.onClick.AddListener(() =>
        // {
        //     OnClickShareBtn(tiktokBtn, tiktokLink);
        // });
        // restorePurchaseBtn.onClick.AddListener(() =>
        // {
        //     //IAPManager.Instance.RestorePurchase();
        // });
    }

    // protected override void OnEnable()
    // {
    //     base.OnEnable();
    //     UpdateLanguage(null);
    // }

    private void OnClickShareBtn(Button button, string link)
    {
        Application.OpenURL(link);
        button.interactable = false;
    }

    private void TurnOffSettingPanel(object data)
    {
        settingPanel.SetActive(false);
    }
    private void Start()
    {
        InitSetting();
        //EventDispatcher.RegisterListener(EventInGame.TurnOffUI, TurnOffSettingPanel);
        //EventDispatcher.RegisterListener(EventInGame.ChangeLanguage, UpdateLanguage);
    }

    private void UpdateLanguage(object data)
    {
        // switch (SessionPref.LanguageUserType)
        // {
        //     case LanguageType.Vietnamese:
        //         currentLanguageTxt.text = "Tiếng Việt";
        //         break;
        //     case LanguageType.English:
        //         currentLanguageTxt.text = "English";
        //         break;
        //     case LanguageType.Hindi:
        //         currentLanguageTxt.text = "Hindi";
        //         break;
        //     case LanguageType.German:
        //         currentLanguageTxt.text = "Deutscht";
        //         break;
        //     case LanguageType.Indonesian:
        //         currentLanguageTxt.text = "Bahasa";
        //         break;
        //     case LanguageType.French:
        //         currentLanguageTxt.text = "Français";
        //         break;
        //     case LanguageType.Espanol:
        //         currentLanguageTxt.text = "Españoll";
        //         break;
        //     case LanguageType.Japanese:
        //         currentLanguageTxt.text = "日本語";
        //         break;
        //     case LanguageType.China:
        //         currentLanguageTxt.text = "简体中文";
        //         break;
        //     case LanguageType.Portuguese:
        //         currentLanguageTxt.text = "Português";
        //         break;
        //     case LanguageType.Arabic:
        //         currentLanguageTxt.text =  "العربية";
        //         break;
        //     case LanguageType.Korean:
        //         currentLanguageTxt.text = "+한국인";
        //         break;
        // }

        //currentLanguageTxt.text = SessionPref.LanguageUserType.ToString();
    }

    private void OnDestroy()
    {
        //EventDispatcher.RemoveListener(EventInGame.TurnOffUI, TurnOffSettingPanel);
        //EventDispatcher.RemoveListener(EventInGame.ChangeLanguage, UpdateLanguage);
    }

    private void Update()
    {
        if(!settingPanel.activeSelf) return;

        //if(LevelManager.Instance.isDrawing) settingPanel.SetActive(false);
    }

    private void OnClickRateBtn()
    {
        //Application.OpenURL("https://play.google.com/store/apps/details?id=com.migames.hidden.objects.findout");
    }

    public void OnClickSettingBtn()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);

        // if (settingPanel.activeSelf) UIController.Instance.isOpenSetting = true;
        // else
        // {
        //     UIController.Instance.isOpenSetting = false;
        // }
    }

    private void InitSetting()
    {
        //init music
        bool musicState = SessionPref.GetSettingType(SettingType.Music);
        Debug.Log("musicState " + musicState);
        if (musicState)
        {
            musicBtn.GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            musicBtn.GetComponent<Image>().sprite = musicOff;
        }

        //init sound
        bool soundState = SessionPref.GetSettingType(SettingType.Sound);
        if (soundState)
        {
            soundBtn.GetComponent<Image>().sprite = soundOn;

        }
        else
        {
            soundBtn.GetComponent<Image>().sprite = soundOff;
        }

        //init vibrate
        bool vibrateState = SessionPref.GetSettingType(SettingType.Vibration);
        if (vibrateState)
        {
            vibrateBtn.GetComponent<Image>().sprite = vibrateOn;

        }
        else
        {
            vibrateBtn.GetComponent<Image>().sprite = vibrateOff;
        }
    }

    public void OnClickSetting(int id)
    {
        switch (id)
        {
            case 0:
                bool musicState = SessionPref.GetSettingType(SettingType.Music);
                if (musicState)
                {
                    musicBtn.GetComponent<Image>().sprite = musicOff;
                    SoundController.Instance.ChangeBackgroundMusicVolume(0);
                }
                else
                {
                    musicBtn.GetComponent<Image>().sprite = musicOn;
                    SoundController.Instance.ChangeBackgroundMusicVolume(1);
                }
                SessionPref.ChangeSettingState(SettingType.Music);
                break;
            case 1:
                bool soundState = SessionPref.GetSettingType(SettingType.Sound);
                if (soundState)
                {
                    soundBtn.GetComponent<Image>().sprite = soundOff;
                }
                else
                {
                    soundBtn.GetComponent<Image>().sprite = soundOn;
                }
                SessionPref.ChangeSettingState(SettingType.Sound);
                break;
            case 2:
                bool vibrateState = SessionPref.GetSettingType(SettingType.Vibration);
                if (vibrateState)
                {
                    vibrateBtn.GetComponent<Image>().sprite = vibrateOff;

                }
                else
                {
                    vibrateBtn.GetComponent<Image>().sprite = vibrateOn;
                }
                SessionPref.ChangeSettingState(SettingType.Vibration);
                break;
        }
    }

}
