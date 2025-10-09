using System.Collections;
using System.Collections.Generic;using System.ComponentModel;using DucLV;
using UnityEngine;
using DucLV;
public class UIController : Singleton<UIController> {
    public GameObject tryAgainPanel;
    public HomeUI homeUI;
    public GameObject NotEnoughCoin;
    private Coroutine notEnoughCoinCoroutine;

    public void ActiveHomeUI(bool active)
    {
        homeUI.gameObject.SetActive(active);
    }

    public void ShowTryAgainPanel()
    {
        tryAgainPanel.SetActive(true);
    }

    public void ShowNotEnoughCoin()
    {
        if (notEnoughCoinCoroutine != null)
        {
            StopCoroutine(notEnoughCoinCoroutine);
        }
        notEnoughCoinCoroutine = StartCoroutine(ShowAndHideNotEnoughCoin());
    }

    private IEnumerator ShowAndHideNotEnoughCoin()
    {
        NotEnoughCoin.SetActive(true);
        yield return new WaitForSeconds(1f);
        NotEnoughCoin.SetActive(false);
        notEnoughCoinCoroutine = null;
    }
}
