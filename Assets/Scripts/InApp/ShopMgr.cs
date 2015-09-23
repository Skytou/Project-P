using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Prime31;


public class ShopMgr : MonoBehaviour {

    bool isShopOpen = false;
    ShopDB shop = new ShopDB();
    int totalCoins;
    
    // debug
    public Text ShopStatus;
    public Text TotalCoins;

    public int storeIndex;

    public Sprite life, cyclone, energy, bomb, sword, timer, armor, coins, throwKnife;

    public Image storeImage;
    public Text storeText;

    public string selectedPowerUp;
    public GameObject PopUpNotEnoughCoins;
    public GameObject PopUpInApp;

    void OnEnable()
    {
		if( Application.platform != RuntimePlatform.Android )
			return;
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
    }


    void OnDisable()
    {
		if( Application.platform != RuntimePlatform.Android )
			return;
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
    }


	void Start () {
        OpenShop();
        PopUpInApp.SetActive(false);
        PopUpNotEnoughCoins.SetActive(false);
	}
	

    void Update () {
        //TotalCoins.text = "Coins : " + totalCoins;
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


    public void OnBuyInAppItem(string itemId)
    {
        if (isShopOpen)
        {
            GoogleIAB.purchaseProduct(itemId);
            ShopStatus.text = "BuyItem : " + itemId;
        }
    }


    public void OnClosePopup()
    {
        PopUpInApp.SetActive(false);
    }

    
    public void OnOpenPopup()
    {
        PopUpInApp.SetActive(true);
    }

    #region iabevents
    void billingSupportedEvent()
    {
        Debug.Log("znop: billingSupportedEvent");
        isShopOpen = true;
        //ShopStatus.text = "isShopOpen : " + isShopOpen;

    }


    void billingNotSupportedEvent(string error)
    {
        Debug.Log("znop: billingNotSupportedEvent: " + error);
        isShopOpen = false;
        //ShopStatus.text = "isShopOpen : " + isShopOpen;
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


    #region StoreHUD

    public void BuyLife()
    {
        storeImage.sprite = life;
        storeText.text = "The life of the hero can be upgraded to give more survival in-spite the attacks taken. Upgrade to get extra 20 survival capacity";
        selectedPowerUp = "Life";
    }

    public void BuyKnife()
    {
        storeImage.sprite = throwKnife;
        storeText.text = "This special weapon helps to kill the enemies from a distance. \nMore the throw knives more are the escape opportunities";
        selectedPowerUp = "Knife";
    }

    public void BuyCyclone()
    {
        storeImage.sprite = cyclone;
        storeText.text = "Master move by the player to kill the enemies with a circular spin";
        selectedPowerUp = "Cyclone";
    }

    public void BuyEnergy()
    {
        storeImage.sprite = energy;
        storeText.text = "The hero can sustain in the battle field only till his energy lasts. \nUpgrade to get extra 20 energy to give him more fighting time";
        selectedPowerUp = "Energy";
    }

    public void BuyBomb()
    {

        storeImage.sprite = bomb;
        storeText.text = "Stun bomb to stuns the enemies for a certain time. Purchase more bombs to freeze enemies at multiple instances";
        selectedPowerUp = "Bomb";
    }

    public void BuyTimerFreeze()
    {
        storeImage.sprite = timer;
        storeText.text = "An energy freeze to pause the energy depletion of the hero. This gives more fighting time";
        selectedPowerUp = "TimerFreeze";
    }

    public void BuySword()
    {
        storeImage.sprite = sword;
        storeText.text = "The power of the sword determines the hits to kill the enemy. \nUpgrade the sword to kill the enemy fast";
        selectedPowerUp = "Sword";
    }

    public void BuyCoins()
    {
        storeImage.sprite = coins;
        storeText.text = "Buy more coins and use them in store to get more upgrades";
        selectedPowerUp = "Coins";
    }


    public void BuyArmor()
    {
        storeImage.sprite = armor;
        storeText.text = "The armour protects the hero from attack by the enemies. \nUpgrade the armour to reduce damage ";
        selectedPowerUp = "Armor";
    }



    public void OnBuyButton()
    {
        switch (selectedPowerUp)
        {
            case "Life":

                break;


            case "Sword":
                break;

            case "Bomb":

                break;

            case "Armor":

                break;

            case "TimerFreeze":

                break;

            case "Energy":

                break;

            case "Cyclone":

                break;

            case "Coins":
                OnOpenPopup();
                break;
        }
    }
	
    #endregion StoreHUD
}
