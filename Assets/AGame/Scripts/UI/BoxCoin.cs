using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxCoin : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private int currentCoinCount;

    void Start()
    {
        currentCoinCount = SessionPref.GetGoldRemaining;
        UpdateCoinText();

        EventDispatcher.RegisterListener(EventID.AddCoin, OnAddCoins);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.AddCoin, OnAddCoins);
    }

    void UpdateCoinText()
    {
        coinText.text = currentCoinCount.ToString();
    }

    private void OnAddCoins(object data)
    {
        if (data is not int) return;

        int amount = (int)data;
        Debug.Log("add coins: " + amount);
        //Debug.
        AddCoins(amount);
    }

    public void AddCoins(int amount)
    {
        //StartCoroutine(AnimateCoinCount(currentCoinCount, currentCoinCount + amount));
        currentCoinCount = SessionPref.GetGoldRemaining;
        UpdateCoinText();
    }

    private IEnumerator AnimateCoinCount(int startCount, int endCount)
    {
        // float duration = 1.0f; // Duration of the animation
        // float elapsedTime = 0f;
        //
        // while (elapsedTime < duration)
        // {
        //     currentCoinCount = (int)Mathf.Lerp(startCount, endCount, elapsedTime / duration);
        //     UpdateCoinText();
        //     elapsedTime += Time.deltaTime;
        //     yield return null;
        // }
        //
        // currentCoinCount = endCount;

        UpdateCoinText();
        yield return null;
    }
}
