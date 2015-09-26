using UnityEngine;
using System.Collections;
using Prime31;

public class SavedData
{
    public int GamePlayCount = 0;
    public int TotalCoins = 0;
    public int TotalCrystals = 0;
    public string LastSavedTime = "";
    public string LastDailyBonusTime = "";
    public string LastEnergyBonusTime = "";

    System.DateTime currentDate;

    private int _fiveSecondNotificationId;
    private int _tenSecondNotificationId;

    protected SavedData() { }
    private static SavedData instance = null;
    public static SavedData Inst
    {
        get
        {
            if (instance == null)
            {
                instance = new SavedData();
            }
            return instance;
        }
    }


    public void LoadSavedData()
    {
        GamePlayCount = PlayerPrefs.GetInt("GamePlayCount", 0);
        if (GamePlayCount == 0)
        {
            GamePlayCount++;
            CreateDefaultPref();
        }
        TotalCoins = PlayerPrefs.GetInt("TotalCoins", 1000);
        GameGlobalVariablesManager.totalNumberOfCoins = TotalCoins;
        TotalCrystals = PlayerPrefs.GetInt("TotalCrystals", 0);
        currentDate = System.DateTime.Now;
        LastSavedTime = PlayerPrefs.GetString("LastSavedTime", currentDate.ToBinary().ToString());
        LastDailyBonusTime = PlayerPrefs.GetString("LastDailyBonusTime", currentDate.ToBinary().ToString());
        LastEnergyBonusTime = PlayerPrefs.GetString("LastEnergyBonusTime", currentDate.ToBinary().ToString());
    }


    public void SaveAllData()
    {
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        PlayerPrefs.SetInt("GamePlayCount", GamePlayCount);
        TotalCoins = GameGlobalVariablesManager.totalNumberOfCoins;
        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        PlayerPrefs.SetInt("TotalCrystals", TotalCrystals);
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastDailyBonusTime", LastDailyBonusTime);
        PlayerPrefs.SetString("LastEnergyBonusTime", LastEnergyBonusTime);
        PlayerPrefs.Save();
    }


    public void CreateDefaultPref()
    {
        PlayerPrefs.SetInt("GamePlayCount", GamePlayCount);
        PlayerPrefs.SetInt("TotalCoins", GameGlobalVariablesManager.GameStartCoins);
        PlayerPrefs.SetInt("TotalCrystals", TotalCrystals);
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        LastDailyBonusTime = LastSavedTime;
        LastEnergyBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastDailyBonusTime", LastDailyBonusTime);
        PlayerPrefs.SetString("LastEnergyBonusTime", LastEnergyBonusTime);
        PlayerPrefs.Save();
    }


    public void OnDailyBonusGiven()
    {
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        LastDailyBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastDailyBonusTime", LastDailyBonusTime);
        PlayerPrefs.Save();
    }


    public void OnEnergyBonusGiven()
    {
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        LastEnergyBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastEnergyBonusTime", LastEnergyBonusTime);
        PlayerPrefs.Save();
    }


    void OnApplicationQuit()
    {
        SaveAllData();
    }


    void OnApplicationPause()
    {
        SaveAllData();
    }

        
    public int GetGamePlayCount()
    {
        return GamePlayCount;
    }


    public int GetTotalCoins()
    {
        return TotalCoins;
    }


    public int GetTotalCrystals()
    {
        return TotalCrystals;
    }


    public long GetLastBonusTime()
    {
        return System.Convert.ToInt64(LastDailyBonusTime);
    }


    public long GetLastEnergyTime()
    {
        return System.Convert.ToInt64(LastEnergyBonusTime);
    }


    public long GetLastSavedTime()
    {
        return System.Convert.ToInt64(LastDailyBonusTime);
    }

}
