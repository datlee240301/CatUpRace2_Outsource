using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public int ID;
    public int price;
    public BaseButton useButton;
    public BaseButton buyButton;
    public BaseButton usedButton;

    private void Awake()
    {
        useButton.button.onClick.AddListener(OnClickUseButton);
        buyButton.button.onClick.AddListener(OnClickBuyButton);
        usedButton.button.onClick.AddListener(OnClickUsedButton);
    }

    private void Start()
    {
        EventDispatcher.RegisterListener(EventID.UpdateItemInShop, OnUpdateStatusItem);
        OnUpdateStatusItem(null);
    }

    private void OnDestroy()
    {
        EventDispatcher.RemoveListener(EventID.UpdateItemInShop, OnUpdateStatusItem);
    }

    private void OnUpdateStatusItem(object data)
    {
        if(SessionPref.CurrentItemInUse == ID)
        {
            SetStatusItem(ItemShopStatus.Using);
        }
        else if(SessionPref.GetListItemsBought().Contains(ID))
        {
            SetStatusItem(ItemShopStatus.Bought);
        }
        else
        {
            SetStatusItem(ItemShopStatus.NotBuy);
        }
    }

    private void OnClickUseButton()
    {
        //SetStatusItem(ItemShopStatus.Using);
        SessionPref.CurrentItemInUse = ID;
        EventDispatcher.PostEvent(EventID.UpdateItemInShop);
    }

    private void OnClickBuyButton()
    {
        if(SessionPref.GetGoldRemaining >= price)
        {
            Debug.Log("buy item success");
            SessionPref.GetGoldRemaining -= price;
            SessionPref.AddItemBought(ID);
            SessionPref.CurrentItemInUse = ID;
            EventDispatcher.PostEvent(EventID.UpdateItemInShop);
            //SetStatusItem(ItemShopStatus.Using);
        }
        else
        {
            UIController.Instance.ShowNotEnoughCoin();
        }

    }

    private void OnClickUsedButton()
    {
    }

    public void SetStatusItem(ItemShopStatus status)
    {
        switch (status)
        {
            case ItemShopStatus.Bought:
                buyButton.gameObject.SetActive(false);
                usedButton.gameObject.SetActive(false);
                useButton.gameObject.SetActive(true);
                break;
            case ItemShopStatus.NotBuy:
                buyButton.gameObject.SetActive(true);
                usedButton.gameObject.SetActive(false);
                useButton.gameObject.SetActive(false);
                break;
            case ItemShopStatus.Using:
                buyButton.gameObject.SetActive(false);
                usedButton.gameObject.SetActive(true);
                useButton.gameObject.SetActive(false);
                break;
        }
    }
}

public enum ItemShopStatus
{
    NotBuy,
    Bought,
    Using
}
