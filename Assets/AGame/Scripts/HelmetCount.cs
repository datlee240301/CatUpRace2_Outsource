using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelmetCount : MonoBehaviour
{
    private float helmetCount = 0;
    public Image countImage;
    public TMP_Text countText;

    private void Start()
    {
        EventDispatcher.RegisterListener(EventID.ResetGame, OnResetGame);
    }

    private void Update()
    {
        Debug.Log("time scale: "+Time.timeScale);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.ResetGame, OnResetGame);
    }

    private void OnResetGame(object obj)
    {
        this.countImage.gameObject.SetActive(false);
        this.countText.gameObject.SetActive(false);
        Debug.Log("turn off count down");
        this.gameObject.SetActive(false);
    }

    public float CurrentHelmetCount
    {
        get => helmetCount;
        set
        {
            helmetCount = value;
            Debug.Log("Helmet count: " + helmetCount);
            UpdateTimeCount();
        }
    }

    public void UpdateTimeCount()
    {
        if (helmetCount <= 0)
        {
            this.countImage.gameObject.SetActive(false);
            this.countText.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            this.countImage.gameObject.SetActive(true);
            this.countText.gameObject.SetActive(true);
        }
        countImage.fillAmount = helmetCount/10;
        countText.text = helmetCount.ToString();
    }
}
