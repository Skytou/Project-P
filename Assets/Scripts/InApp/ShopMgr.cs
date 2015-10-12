using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.Collections.Generic;
using Prime31;


public class ShopMgr : MonoBehaviour {

    bool isShopOpen = false;
    ShopDB shop = new ShopDB();
    
    // debug
    public Text ShopStatus;
    public Text TotalCoins;

    public int storeIndex;

    public Sprite life, cyclone, energy, bomb, sword, timer, armor, coins, throwKnife;

    public Image storeImage;
    public Text storeText;

    public string selectedPowerUp = "store";
    public GameObject PopUpNotEnoughCoins;
    public GameObject PopUpInApp;

    public Text KnifeCountText;
    public Text BombsCountText;
    public Text CycloneCountText;

    public Text LevelText;
    public GameObject LevelTextBg;
    public Text CoinsValText;
    public GameObject CoinsValBg;


    int hackTap = 0;

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
        SavedData.Inst.LoadSavedData();
        OpenShop();
        PopUpInApp.SetActive(false);
        PopUpNotEnoughCoins.SetActive(false);

        HideLevelUI();
        HideCoinsUI();

        UpdateUI();
        hackTap = 0;
	}
	

    void Update () {
	}

    void UpdateUI()
    {
        TotalCoins.text = GameGlobalVariablesManager.totalNumberOfCoins.ToString();
        KnifeCountText.text = GameGlobalVariablesManager.KnifeCount.ToString();
        BombsCountText.text = GameGlobalVariablesManager.BombsCount.ToString();
        CycloneCountText.text = GameGlobalVariablesManager.CycloneCount.ToString();
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
        GameGlobalVariablesManager.totalNumberOfCoins += coins;
    }

    
    public bool UseCoins(int coins)
    {
        bool retVal = false;
        if (coins >= GameGlobalVariablesManager.totalNumberOfCoins)
        {
            GameGlobalVariablesManager.totalNumberOfCoins -= coins;
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
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
            GoogleIAB.purchaseProduct(itemId);
            //ShopStatus.text = "BuyItem : " + itemId;
        }
    }


    public void OnClosePopup()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        PopUpInApp.SetActive(false);
    }

    
    public void OnOpenPopup()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        PopUpInApp.SetActive(true);
        PopUpNotEnoughCoins.SetActive(false);
    }


    public void OnOpenNotEnoughCoins()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        PopUpInApp.SetActive(false);
        PopUpNotEnoughCoins.SetActive(true);
    }


    public void OnCloseNotEnoughCoins()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        PopUpNotEnoughCoins.SetActive(false);
    }


    public void OnBackButton()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Application.LoadLevel(GameGlobalVariablesManager.MainMenu);
    }


    #region iabevents
    void billingSupportedEvent()
    {
        Debug.Log("znop: billingSupportedEvent");
        isShopOpen = true;
    }


    void billingNotSupportedEvent(string error)
    {
        Debug.Log("znop: billingNotSupportedEvent: " + error);
        isShopOpen = false;
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
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = life;
        storeText.text = "The life of the hero can be upgraded to give more survival in-spite the attacks taken. Upgrade to get extra life";
        selectedPowerUp = "Life";
        UpdateLevelUI();
    }


    public void BuySword()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = sword;
        storeText.text = "The power of the sword determines the hits to kill the enemy. \nUpgrade the sword to kill the enemy fast";
        selectedPowerUp = "Sword";
        UpdateLevelUI();
    }


    public void BuyKnife()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = throwKnife;
        storeText.text = "This special weapon helps to kill the enemies from a distance. \nMore the throw knives more are the escape opportunities";
        selectedPowerUp = "Knife";
        UpdateLevelUI();
    }


    public void BuyArmor()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = armor;
        storeText.text = "The armour protects the hero from attack by the enemies. \nUpgrade the armour to reduce damage ";
        selectedPowerUp = "Armor";
        UpdateLevelUI();
    }


    public void BuyEnergy()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = energy;
        storeText.text = "Hero needs energy to enter every level.\nBuy more energy to continue fight";
        selectedPowerUp = "Energy";
        UpdateLevelUI();
    }


    public void BuyCyclone()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = cyclone;
        storeText.text = "Master move by the player to kill the enemies with a circular spin";
        selectedPowerUp = "Cyclone";
        UpdateLevelUI();
    }


    public void BuyBomb()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = bomb;
        storeText.text = "Stun bomb stuns the enemies for a certain time. Purchase more bombs to freeze enemies at multiple instances";
        selectedPowerUp = "Bomb";
        UpdateLevelUI();
    }


    public void BuyTimerFreeze()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = timer;
        storeText.text = "An energy freeze to pause the energy depletion of the hero. This gives more fighting time";
        selectedPowerUp = "Timer";
        UpdateLevelUI();
    }


    public void BuyCoins()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        storeImage.sprite = coins;
        storeText.text = "Buy more coins and use them in store to get more upgrades";
        selectedPowerUp = "Coins";
        UpdateLevelUI();
    }

	public void ShowRewardedAd()
	{
        
		Debug.Log ("Video ad reward");
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
            GameGlobalVariablesManager.IncreaseEnergy(1);
			SavedData.Inst.SaveEnergyData ();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
        UpdateUI();
        UpdateLevelUI();
	}

    public void OnBuyButton()
    {
        if (selectedPowerUp.Equals("Coins"))
        {
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
            OnOpenPopup();
            return;
        }

        long curItemPrice = shop.GetCoins(selectedPowerUp);
        Debug.Log("curItemPrice : " + curItemPrice);
        if (curItemPrice > GameGlobalVariablesManager.totalNumberOfCoins)
        {
            OnOpenNotEnoughCoins();
            return;
        }
        bool isPurchased = false;
        switch (selectedPowerUp)
        {
            case "Life":
                if (GameGlobalVariablesManager.PlayerLevel < GameGlobalVariablesManager.MaxLevel)
                {
                    GameGlobalVariablesManager.PlayerLevel += 1;
                    isPurchased = true;
                }
                break;
            case "Sword":
                if (GameGlobalVariablesManager.SwordLevel < GameGlobalVariablesManager.MaxLevel)
                {
                    GameGlobalVariablesManager.SwordLevel += 1;
                    isPurchased = true;
                }
                break;
            case "Knife":
                if (GameGlobalVariablesManager.KnifeCount < GameGlobalVariablesManager.MaxKnife)
                {
                    GameGlobalVariablesManager.KnifeCount += 1;
                    isPurchased = true;
                }
                break;
            case "Bomb":
                if (GameGlobalVariablesManager.BombsCount < GameGlobalVariablesManager.MaxBombs)
                {
                    GameGlobalVariablesManager.BombsCount += 1;
                    isPurchased = true;
                }
                break;
            case "Armor":
                if (GameGlobalVariablesManager.ArmorLevel < GameGlobalVariablesManager.MaxLevel)
                {
                    GameGlobalVariablesManager.ArmorLevel += 1;
                    GameGlobalVariablesManager.AttackHealthLost = GameGlobalVariablesManager.MaxLevel - GameGlobalVariablesManager.ArmorLevel + 1;
                    if (GameGlobalVariablesManager.AttackHealthLost < 1)
                        GameGlobalVariablesManager.AttackHealthLost = 1;
                    isPurchased = true;
                }
                break;
            case "Energy":
                if (GameGlobalVariablesManager.EnergyAvailable < GameGlobalVariablesManager.MaxEnergy)
                {
                    GameGlobalVariablesManager.IncreaseEnergy(1);
                    isPurchased = true;
                }
                break;
            case "Cyclone":
                if (GameGlobalVariablesManager.CycloneCount < GameGlobalVariablesManager.MaxCyclone)
                {
                    GameGlobalVariablesManager.CycloneCount += 1;
                    isPurchased = true;
                }
                break;           
        }
        if(isPurchased)
        {
            GameGlobalVariablesManager.totalNumberOfCoins -= (int)curItemPrice;
            SavedData.Inst.SaveAllData();
            AudioMgr.Inst.PlaySfx(SfxVals.BuyItem);
            SavedData.Inst.SaveAllData();
        }        
        UpdateUI();
        UpdateLevelUI();
    }

    // hack 
    public void OnHackMoney()
    {
        hackTap++;
        if (hackTap > 5 && GameGlobalVariablesManager.IsHackEnabled)
            GameGlobalVariablesManager.totalNumberOfCoins += 1000;
        UpdateUI();
    }

    public void UpdateLevelUI()
    {
        long curItemPrice = shop.GetCoins(selectedPowerUp);
        switch (selectedPowerUp)
        {
            case "Life":
                LevelText.gameObject.SetActive(true);
                LevelTextBg.SetActive(true);
                LevelText.text = GameGlobalVariablesManager.PlayerLevel.ToString();
                CoinsValText.gameObject.SetActive(true);
                CoinsValBg.SetActive(true);
                CoinsValText.text = curItemPrice.ToString();
                break;
            case "Sword":
                LevelText.gameObject.SetActive(true);
                LevelTextBg.SetActive(true);
                LevelText.text = GameGlobalVariablesManager.SwordLevel.ToString();
                CoinsValText.gameObject.SetActive(true);
                CoinsValBg.SetActive(true);
                CoinsValText.text = curItemPrice.ToString();
                break;
            case "Armor":
                LevelText.gameObject.SetActive(true);
                LevelTextBg.SetActive(true);
                LevelText.text = GameGlobalVariablesManager.ArmorLevel.ToString();
                CoinsValText.gameObject.SetActive(true);
                CoinsValBg.SetActive(true);
                CoinsValText.text = curItemPrice.ToString();
                break;
            case "Knife":
                LevelText.gameObject.SetActive(false);
                LevelTextBg.SetActive(false);
                CoinsValText.gameObject.SetActive(true);
                CoinsValBg.SetActive(true);
                CoinsValText.text = curItemPrice.ToString();
                break;
            case "Bomb":
                LevelText.gameObject.SetActive(false);
                LevelTextBg.SetActive(false);
                CoinsValText.gameObject.SetActive(true);
                CoinsValBg.SetActive(true);
                CoinsValText.text = curItemPrice.ToString();
                break;
            case "Cyclone":
                LevelText.gameObject.SetActive(false);
                LevelTextBg.SetActive(false);
                CoinsValText.gameObject.SetActive(true);
                CoinsValBg.SetActive(true);
                CoinsValText.text = curItemPrice.ToString();
                break;
            case "Energy":
                LevelText.gameObject.SetActive(true);
                LevelTextBg.SetActive(true);
                LevelText.text = GameGlobalVariablesManager.EnergyAvailable.ToString();
                CoinsValText.gameObject.SetActive(true);
                CoinsValBg.SetActive(true);
                CoinsValText.text = curItemPrice.ToString();
                break;
            case "Coins":
                LevelText.gameObject.SetActive(false);
                LevelTextBg.SetActive(false);
                CoinsValText.gameObject.SetActive(false);
                CoinsValBg.SetActive(false);
                break;
        }
    }
        

    public void HideLevelUI()
    {
        LevelText.gameObject.SetActive(false);
        LevelTextBg.SetActive(false);
    }

    public void HideCoinsUI()
    {
        CoinsValText.gameObject.SetActive(false);
        CoinsValBg.SetActive(false);
    }

    #endregion StoreHUD
}
