using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DucLV;

public class LoadingPopup : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    public float duration = 1.5f;
    private bool isShowAdsOpen = false;
    private bool isCallAds = false;
    private void Start()
    {
        StartCoroutine(FakeLoadingProcess());
    }

    IEnumerator FakeLoadingProcess()
    {
        //if(SessionPref.IsFirstOpenGame||SessionPref.IsRemoveAds) Time.timeScale = 3;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;


            if (isShowAdsOpen) Time.timeScale = 6;

            // Update the progress bar
            progressBar.DOFillAmount(progress,.1f);

            // Update loading text

            // Wait for a short time (you can adjust this value)
            yield return new WaitForSeconds(0.1f);
            // Increment the elapsed time
            elapsedTime += 0.1f;
            float roundedNumber = (float)Math.Round(progress, 1);
            // if (roundedNumber == .8f && !isShowAdsOpen && !isCallAds && !SessionPref.IsFirstOpenGame && SessionPref.GetCurrentLevel() >= 2 && FirebaseController.Instance.ShowAdsIos)
            // {
            //     Debug.Log("show open ads");
            //     // if (AdsController.Instance.IsOpenAdsAvailable())
            //     // {
            //     //     Time.timeScale = 0;
            //     //     //SessionPref.IsFirstOpenGame = false;
            //     //     isCallAds = true;
            //     //     AdsController.Instance.ShowOpenAds(() =>
            //     //     {
            //     //         Time.timeScale = 10;
            //     //         isShowAdsOpen = true;
            //     //         Debug.Log("app open close call back action");
            //     //     });
            //     // }
            //     // else
            //     // {
            //     //     Debug.Log("IsAppOpenAdAvailable = false");
            //     // }
            // }


        }

        // Ensure the progress bar is at 100% and update text
        progressBar.fillAmount = 1f;

        //if (SessionPref.IsFirstOpenGame) SessionPref.IsFirstOpenGame = false;

        // Simulate some extra processing time (optional)
        yield return new WaitForSeconds(.5f);

        // Hide the loading UI or transition to the next scene
        // For demonstration purposes, we will just disable the UI
        progressBar.gameObject.SetActive(false);
        // Your loading is complete, do further actions here
        Debug.Log("Loading complete!");

        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        // UIController.Instance.CheckInternetWrapper();
        // SoundController.Instance.InitBackgroundMusic();
        //
        // if(!FirebaseController.Instance.ShowAdsIos) yield break;
        //
        // if(SessionPref.GetCurrentLevel() > 1)
        // AdsController.Instance.ShowBaner();
    }
}
