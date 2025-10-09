using DucLV;

namespace Remi.InApp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Purchasing;
    using UnityEngine.UI;

    public static class PurchaseID
    {

    }

    public class UIProduct : MonoBehaviour
    {
        [SerializeField]
        private string _purchaseID;
        public string purchaseID => _purchaseID;
        [SerializeField]
        private Button _purchaseButton;
        public Button purchaseButton => _purchaseButton;
        [SerializeField]
        private TextMeshProUGUI _price;
        [SerializeField]
        private TextMeshProUGUI _discount;

        [SerializeField] private PackElement packElement;
        public delegate void PurchaseEvent(Product Model, Action OnComplete);
        public event PurchaseEvent OnPurchase;

        private Product _model;

        // protected override void OnEnable()
        // {
        //     base.OnEnable();
        // }

        protected void Start()
        {
            Debug.Log("start product");
            RegisterPurchase();
            RegisterEventButton();
        }

        protected virtual void RegisterPurchase()
        {
            Debug.Log("register purchase");
            StartCoroutine(IAPManager.Instance.CreateHandleProduct(this));
        }

        public void Setup(Product Product, string code, string price)
        {
            _model = Product;
            _price.text = price + " " + code;
            if (_discount != null)
            {
                if (code.Equals("VND"))
                {
                    var round = Mathf.Round(float.Parse(price) + float.Parse(price) * .4f);
                    _discount.text = code + " " + round;
                }
                else
                {
                    var priceFormat = string.Format("{0:0.00}", float.Parse(price) + float.Parse(price) * .4f);
                    _discount.text = code + " " + priceFormat;
                }
            }
        }

        protected virtual void RegisterEventButton()
        {
            _purchaseButton.onClick.AddListener(() => 
            {
                Purchase();
            });
        }

        public void Purchase()
        {
            OnPurchase?.Invoke(_model, HandlePurchaseComplete);
        }

        private void HandlePurchaseComplete()
        {
            packElement.OnSuccessPurchase();
        }

    }
}