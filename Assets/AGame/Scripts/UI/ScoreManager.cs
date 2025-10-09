using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreTxt;

    void Start()
    {
        StartCountScore();
        EventDispatcher.RegisterListener(EventID.AddCoin, AddScore);
        EventDispatcher.RegisterListener(EventID.ResetGame, OnResetGame);
        EventDispatcher.RegisterListener(EventID.EndGame, OnEndGame);
    }

    

    public void StartCountScore()
    {
        InvokeRepeating(nameof(IncrementScore), 0.5f, 0.5f);
    }

    public void OnEndGame(object data)
    {
        StopCountScore();
    }

    public void StopCountScore()
    {
        CancelInvoke(nameof(IncrementScore));
        EventDispatcher.PostEvent(EventID.SendScore, score);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.AddCoin, AddScore);
        EventDispatcher.RemoveListener(EventID.ResetGame, OnResetGame);
        EventDispatcher.RemoveListener(EventID.EndGame, OnEndGame);
    }

    void IncrementScore()
    {
        score += 1;
        scoreTxt.text = score.ToString();
    }

    private void OnResetGame(object data)
    {
        score = 0;
        scoreTxt.text = score.ToString();
        StartCountScore();
    }

    public void AddScore(object data)
    {
        if(data is not int) return;

        score += (int)data;
        scoreTxt.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}