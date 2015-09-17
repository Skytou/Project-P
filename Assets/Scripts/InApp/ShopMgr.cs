using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopMgr : MonoBehaviour {

    bool isShopOpen = false;
    ShopDB shop = new ShopDB();
    int totalCoins;

    // debug
    public Text ShopStatus;
    public Text TotalCoins;

    void OnEnable()
    {
#if UNITY_ANDROID
        // Listen to all events for illustration purposes
        GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
        GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
        GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
        GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
        GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
        GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
        GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
        GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
        GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
#endif
    }


    void OnDisable()
    {
#if UNITY_ANDROID
        // Remove all event handlers
        GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
        GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
        GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
        GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
        GoogleIABManager.purchaseCompleteAwaitingVerificationEvent -= purchaseCompleteAwaitingVerificationEvent;
        GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
        GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
        GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
        GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
#endif
    }


	void Start () {
        OpenShop();
	}
	

    void Update () {
        TotalCoins.text = "Coins : " + totalCoins;
	}


    bool IsShopOpen()
    {
        bool retVal = true;
        if (isShopOpen)
        {
        }
        else
        {
            OpenShop();
        }
        return retVal;
    }


    public void OpenShop()
    {
        var key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAviAOI4AdVKsLQbvDj+17rhM3Khhoq3DexB+6PR/PVJbTnuvMfr++Yr7unzbWHTnDdOMp3E+Wf0zbitCoJBtb7rFgoSz05Z23lYAxk94YT9/JuHJHgFFKoAAU0AK9/oBCaZVsQ57WrAxrOOv+RYMH4WZpHKaRrtN4YXX1hE8enmIqS9ZMZgCqCely+kVGi2Szbh7NoGljoIeL0hrhCknu0dM1QC+t/28zwPJrNuWX7ziQA0sijo3L9h41BmyZPnDLUW7mK2Gp493e9Kx9FKkvnCmz4C8VoCvJbPsZcq/sEG6gwf5NgUU1AxrZtVun6Um/H1m2qZta9+mdLuuGcnLfzQIDAQAB";
        GoogleIAB.init(key);
    }


    public void AddCoins(int coins)
    {
        totalCoins += coins;
    }

    
    public bool UseCoins(int coins)
    {
        bool retVal = false;
        if (coins >= totalCoins)
        {
            totalCoins -= coins;
            retVal = true;
        }
        return retVal;
    }


    public void PrintItemsInShop()
    {
        GoogleIAB.queryInventory(shop.GetAllItemIds());
    }


    public void BuyItem(string itemId)
    {
        if (isShopOpen)
        {
            GoogleIAB.purchaseProduct(itemId);
            ShopStatus.text = "BuyItem : " + itemId;
        }
    }

    #region iabevents
    void billingSupportedEvent()
    {
        Debug.Log("znop: billingSupportedEvent");
        isShopOpen = true;
        ShopStatus.text = "isShopOpen : " + isShopOpen;

    }


    void billingNotSupportedEvent(string error)
    {
        Debug.Log("znop: billingNotSupportedEvent: " + error);
        isShopOpen = false;
        ShopStatus.text = "isShopOpen : " + isShopOpen;
    }


    void queryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
    {
        Debug.Log(string.Format("znop: queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count));
        Prime31.Utils.logObject(purchases);
        Prime31.Utils.logObject(skus);
    }


    void queryInventoryFailedEvent(string error)
    {
        Debug.Log("znop: queryInventoryFailedEvent: " + error);
    }


    void purchaseCompleteAwaitingVerificationEvent(string purchaseData, string signature)
    {
        Debug.Log("znop: purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
    }

    // immediately consume after purchase
    void purchaseSucceededEvent(GooglePurchase purchase)
    {
        Debug.Log("znop: purchaseSucceededEvent: " + purchase);
        GoogleIAB.consumeProduct(purchase.productId);
        ShopStatus.text = "Success : " + purchase.productId;
    }


    void purchaseFailedEvent(string error, int response)
    {
        Debug.Log("purchaseFailedEvent: " + error + ", response: " + response);
        ShopStatus.text = "Fail : " + response;
    }


    void consumePurchaseSucceededEvent(GooglePurchase purchase)
    {
        Debug.Log("consumePurchaseSucceededEvent: " + purchase);
        AddCoins(shop.GetVirtualCurrency(purchase.productId));
        ShopStatus.text = "Success : " + purchase.productId;
    }


    void consumePurchaseFailedEvent(string error)
    {
        Debug.Log("consumePurchaseFailedEvent: " + error);
        ShopStatus.text = "consumePurchaseFailedEvent ";
    }
    #endregion iabevents
}
