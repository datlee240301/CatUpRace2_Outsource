using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TryAgain : MonoBehaviour
{
    public TMP_Text highScoreTxt;
    public TMP_Text currentScoreTxt;
    public TMP_Text goldTxt;

    public BaseButton homeBtn;
    public BaseButton tryAgainBtn;

    private void Awake()
    {
        homeBtn.button.onClick.AddListener(() =>
        {
            UIController.Instance.ActiveHomeUI(true);
            EventDispatcher.PostEvent(EventID.OnChangeMusicVolumeInHome);
            this.gameObject.SetActive(false);
        });

        tryAgainBtn.button.onClick.AddListener(() =>
        {
            EventDispatcher.PostEvent(EventID.OnChangeMusicVolumeInGame);
        });
    }

    public void Start()
    {
        //EventDispatcher.RegisterListener(EventID.SendScore, InitPanelInfo);
    }

    private void OnEnable()
    {
        SoundController.Instance.PlaySound(SoundType.GameOver);
        InitPanelInfo();
        EventDispatcher.PostEvent(EventID.OnChangeMusicVolumeInHome);
    }

    private void OnDisable()
    {
        //EventDispatcher.PostEvent(EventID.On);
    }

    private void InitPanelInfo()
    {
        int score = GameController.Instance.GetScore();
        SessionPref.HighScore = score;
        currentScoreTxt.text = score.ToString();
        goldTxt.text = SessionPref.GetGoldRemaining.ToString();
        Debug.Log("gold in session pref: " + SessionPref.GetGoldRemaining);
        highScoreTxt.text = SessionPref.HighScore.ToString();
    }
}
