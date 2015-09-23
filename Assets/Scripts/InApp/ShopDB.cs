using UnityEngine;
using System.Collections.Generic;

public class ShopDB{

    List<ShopItem> ShopItemList = new List<ShopItem>();
    List<LocalShopItem> LocalShopItemList = new List<LocalShopItem>();

    public ShopDB() 
    {
        CreateAvailableItems();
    }


    void CreateAvailableItems()
    {
        LocalShopItem curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Life";
        curLocalShopItem.type = 1;
        curLocalShopItem.virtualAmount = 100;
        curLocalShopItem.Desc = "Life";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Sword";
        curLocalShopItem.type = 2;
        curLocalShopItem.virtualAmount = 200;
        curLocalShopItem.Desc = "Sword";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Knife";
        curLocalShopItem.type = 3;
        curLocalShopItem.virtualAmount = 300;
        curLocalShopItem.Desc = "Knife";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Armor";
        curLocalShopItem.type = 4;
        curLocalShopItem.virtualAmount = 400;
        curLocalShopItem.Desc = "Armor";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Energy";
        curLocalShopItem.type = 5;
        curLocalShopItem.virtualAmount = 500;
        curLocalShopItem.Desc = "Energy";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Cyclone";
        curLocalShopItem.type = 6;
        curLocalShopItem.virtualAmount = 600;
        curLocalShopItem.Desc = "Cyclone";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Bomb";
        curLocalShopItem.type = 7;
        curLocalShopItem.virtualAmount = 700;
        curLocalShopItem.Desc = "Bomb";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Timer";
        curLocalShopItem.type = 8;
        curLocalShopItem.virtualAmount = 800;
        curLocalShopItem.Desc = "Timer";
        LocalShopItemList.Add(curLocalShopItem);

        curLocalShopItem = new LocalShopItem();
        curLocalShopItem.uid = "Coins";
        curLocalShopItem.type = 9;
        curLocalShopItem.virtualAmount = 900;
        curLocalShopItem.Desc = "Coins";
        LocalShopItemList.Add(curLocalShopItem);


        // Google shop

        ShopItem curShopItem = new ShopItem();
        curShopItem.type = 1;
        curShopItem.uid = "consume_100_coins";
        curShopItem.realAmount = 10;
        curShopItem.virtualAmount = 100;
        curShopItem.Desc = "100 Coins";
        ShopItemList.Add(curShopItem);

        curShopItem = new ShopItem();
        curShopItem.type = 1;
        curShopItem.uid = "consume_200_coins";
        curShopItem.realAmount = 11;
        curShopItem.virtualAmount = 200;
        curShopItem.Desc = "200 Coins";
        ShopItemList.Add(curShopItem);

        curShopItem = new ShopItem();
        curShopItem.type = 2;
        curShopItem.uid = "consume_100_crystals";
        curShopItem.realAmount = 10;
        curShopItem.virtualAmount = 1000;
        curShopItem.Desc = "100 Crystals";
        ShopItemList.Add(curShopItem);

        PrintStatus();
    }


    public string[] GetAllItemIds()
    {
        List<string> itemIds = new List<string>();
        for(int i = 0; i < ShopItemList.Count ;i++)
        {
            itemIds.Add(ShopItemList[i].uid);
        }
        return itemIds.ToArray();
    }


    public int GetVirtualCurrency(string itemId)
    {
        for (int i = 0; i < ShopItemList.Count; i++)
        {
            if (ShopItemList[i].uid.Equals(itemId))
                return ShopItemList[i].virtualAmount;
        }
        return 0;
    }

    public long GetCoins(string itemId)
    {
        for (int i = 0; i < LocalShopItemList.Count; i++)
        {
            Debug.Log("coins : " + itemId + " = " + LocalShopItemList[i].uid);
            if (LocalShopItemList[i].uid.Equals(itemId))
                return LocalShopItemList[i].virtualAmount;
        }
        return 0;
    }


    public void PrintStatus()
    {
        for (int i = 0;i < ShopItemList.Count; i++)
        {
            Debug.Log(ShopItemList[i].uid + ";" + ShopItemList[i].virtualAmount);
        }
    }
}


public class ShopItem
{
    public ShopItem() { }
    public int type; //0:none, 1:coins, 2: crystals
    public int virtualAmount;
    public int realAmount;
    public string uid;// used in android store
    public string Desc;
}


public class LocalShopItem
{
    public LocalShopItem() { }
    public int type; 
    public long virtualAmount;
    public string uid;
    public string Desc;
}

