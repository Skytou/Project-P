using UnityEngine;
using System.Collections.Generic;

public class ShopDB{

    List<ShopItem> ShopItemList = new List<ShopItem>();

    public ShopDB() 
    {
        CreateAvailableItems();
    }


    void CreateAvailableItems()
    {
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

