using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DucLV
{
    public class PackElement : MonoBehaviour
    {
        public ShopItem[] shopItems;
        [SerializeField] protected Button purchaseButton;
        public UnityEvent callBack;
        private void Start()
        {
            UpdateNoAdsButton();

        }

        public void OnSuccessPurchase()
        {
            foreach (var item in shopItems)
            {
                switch (item.type)
                {
                    case ItemPurchaseType.Gold:
                        //SessionPref.SetRemoveAds(true);
                        SessionPref.AddGoldRemaining(item.amount);
                        UpdateNoAdsButton();
                        /*AdsController.Instance.HideBaner();
                        AdsController.Instance.HideNativeAds();*/
                        break;
                    /*case ItemPurchaseType.Hint:
                        SessionPref.AddHintRemaining(item.amount);
                        //EventDispatcher.PostEvent(EventInGame.UpdateHint);
                        break;*/
                }
            }
            callBack?.Invoke();
        }

        protected void UpdateNoAdsButton()
        {
            foreach (var item in shopItems)
            {
                /*if(item.type == ItemPurchaseType.NoAds && SessionPref.IsRemoveAds())
                {
                    purchaseButton.interactable = false;
                    //UIController.Instance.noAdsBtn.interactable = false;
                }*/
            }
        }
    }

    [Serializable]
    public class ShopItem
    {
        public ItemPurchaseType type;
        public int amount;
    }

    public enum ItemPurchaseType
    {
        Gold
    }
}

