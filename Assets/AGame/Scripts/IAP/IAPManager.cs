

using DucLV;

namespace Remi.InApp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Unity.Services.Core;
    using Unity.Services.Core.Environments;
    using UnityEngine;
    using UnityEngine.Purchasing;
    using UnityEngine.Purchasing.Extension;

    public class IAPManager : Singleton<IAPManager>, IStoreListener, IDetailedStoreListener
    {
        // [SerializeField]
        // private List<UIProduct> _uIProducts;
        // [SerializeField]
        // private GameObject LoadingOverlay;
        [SerializeField]
        private bool UseFakeStore = false;

        private Action OnPurchaseCompleted;
        private IStoreController StoreController;
        private IExtensionProvider ExtensionProvider;

        protected async void Awake()
        {
            InitializationOptions options = new InitializationOptions()
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            .SetEnvironmentName("test");
#else
            .SetEnvironmentName("production");
#endif
            await UnityServices.InitializeAsync(options);
            ResourceRequest operation = Resources.LoadAsync<TextAsset>("IAPProductCatalog");
            operation.completed += HandleIAPCatalogLoaded;
            DontDestroyOnLoad(gameObject);
        }

        // public void AddUIProduct(UIProduct product)
        // {
        //     _uIProducts.Add(product);
        // }

        private void HandleIAPCatalogLoaded(AsyncOperation Operation)
        {
            ResourceRequest request = Operation as ResourceRequest;

            Debug.Log($"Loaded Asset: {request.asset}");
            ProductCatalog catalog = JsonUtility.FromJson<ProductCatalog>((request.asset as TextAsset).text);
            Debug.Log($"Loaded catalog with {catalog.allProducts.Count} items");

            if (UseFakeStore) // Use bool in editor to control fake store behavior.
            {
                StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser; // Comment out this line if you are building the game for publishing.
                StandardPurchasingModule.Instance().useFakeStoreAlways = true; // Comment out this line if you are building the game for publishing.
            }

#if UNITY_ANDROID
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(
                StandardPurchasingModule.Instance(AppStore.GooglePlay)
            );
#elif UNITY_IOS
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.AppleAppStore)
        );
#else
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(
            StandardPurchasingModule.Instance(AppStore.NotSpecified)
        );
#endif
            foreach (ProductCatalogItem item in catalog.allProducts)
            {
                builder.AddProduct(item.id, item.type);
            }

            Debug.Log($"Initializing Unity IAP with {builder.products.Count} products");
            for (int i = 0; i < builder.products.Count; i++)
            {
                var valueAndIndex = builder.products.Select((Value, Index) => new { Value, Index })
                           .ToList();
                Debug.Log(valueAndIndex[i].Value);
                // Debug.Log(StoreController.products.WithID().metadata.isoCurrencyCode + " CurrencyCode");
                // Debug.Log(StoreController.products.WithID(StoreController.products.all[i].definition.id).metadata.localizedPrice + " localizedPrice");
            }
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {

            StoreController = controller;
            ExtensionProvider = extensions;

            Debug.Log($"Successfully Initialized Unity IAP. Store Controller has {StoreController.products.all.Length} products");

            // StartCoroutine(CreateHandleProduct());
        }

        // private IEnumerator CreateHandleProduct()
        // {
        //     List<Product> sortedProducts = StoreController.products.all
        //         .TakeWhile(item => !item.definition.id.Contains("sale"))
        //         .OrderBy(item => item.metadata.localizedPrice)
        //         .ToList();
        //     foreach (Product product in sortedProducts)
        //     {
        //         for (int i = 0; i < _uIProducts.Count; i++)
        //         {
        //             if (_uIProducts[i].purchaseID == product.definition.id)
        //             {
        //                 var code = "";
        //                 var price = "";
        //                 // if (_uIProducts[i].nextLine)
        //                 // {
        //                 //     price = StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.isoCurrencyCode
        //                 //             + "\n" + StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.localizedPrice;
        //                 // }
        //                 // else
        //                 // {
        //                 code = StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.isoCurrencyCode;
        //                 price = StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.localizedPrice.ToString();
        //                 // }
        //                 // Debug.Log(StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.isoCurrencyCode + " CurrencyCode");
        //                 // Debug.Log(StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.localizedPrice + " localizedPrice");
        //                 _uIProducts[i].OnPurchase += HandlePurchase;
        //                 _uIProducts[i].Setup(product, code, price);
        //             }
        //         }
        //         // yield return null;
        //     }
        //     yield return null;
        // }

        public IEnumerator CreateHandleProduct(UIProduct uIProduct)
        {
            
            List<Product> sortedProducts = StoreController.products.all
                .TakeWhile(item => !item.definition.id.Contains("sale"))
                .OrderBy(item => item.metadata.localizedPrice)
                .ToList();
            Debug.Log("CreateHandleProduct "+ sortedProducts.Count);
            foreach (Product product in sortedProducts)
            {
                if (uIProduct.purchaseID == product.definition.id)
                {
                    var code = "";
                    var price = "";
                    // if (_uIProducts[i].nextLine)
                    // {
                    //     price = StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.isoCurrencyCode
                    //             + "\n" + StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.localizedPrice;
                    // }
                    // else
                    // {
                    code = StoreController.products.WithID(uIProduct.purchaseID).metadata.isoCurrencyCode;
                    price = StoreController.products.WithID(uIProduct.purchaseID).metadata.localizedPrice.ToString();
                    Debug.Log("price: " + price);
                    // }
                    // Debug.Log(StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.isoCurrencyCode + " CurrencyCode");
                    // Debug.Log(StoreController.products.WithID(_uIProducts[i].purchaseID).metadata.localizedPrice + " localizedPrice");
                    uIProduct.OnPurchase += HandlePurchase;
                    uIProduct.Setup(product, code, price);
                }
                // yield return null;
            }
            yield return null;
        }

        private void HandlePurchase(Product Product, Action OnPurchaseCompleted)
        {
            // LoadingOverlay.SetActive(true);
            //AdsController.Instance.canShowOpen = false;
            this.OnPurchaseCompleted = OnPurchaseCompleted;
            StoreController.InitiatePurchase(Product);
        }

        public void RestorePurchase() // Use a button to restore purchase only in iOS device.
        {
#if UNITY_IOS
        ExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestore);
#endif
        }

        private void OnRestore(bool obj)
        {
            throw new NotImplementedException();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"Error initializing IAP because of {error}." +
                $"\r\nShow a message to the player depending on the error.");
            
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Failed to purchase {product.definition.id} because {failureReason}");
            OnPurchaseCompleted?.Invoke();
            OnPurchaseCompleted = null;
            // LoadingOverlay.SetActive(false);
            
            //AdsController.Instance.canShowOpen = true;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Successfully purchased {purchaseEvent.purchasedProduct.definition.id}");
            OnPurchaseCompleted?.Invoke();
            OnPurchaseCompleted = null;
            // LoadingOverlay.SetActive(false);

            // do something, like give the player their currency, unlock the item,
            // update some metrics or analytics, etc...
            //AdsController.Instance.canShowOpen = true;
            return PurchaseProcessingResult.Complete;
            
            
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            //AdsController.Instance.canShowOpen = true;
            return;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            //AdsController.Instance.canShowOpen = true;
            return;
        }
    }
}