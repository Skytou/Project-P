using UnityEngine;
using System.Collections;
using Prime31;

public class SavedData
{
    public int GamePlayCount = 0;
    public int TotalCoins = 0;
    public int TotalCrystals = 0;
    public string LastSavedTime = "";
    public string FirstDailyBonusTime = "";
    public string LastDailyBonusTime = "";
    public string LastEnergyBonusTime = "";

    public int KnifeCount = 5;
    public int BombsCount = 3;
    public int CycloneCount = 3;

    public int LevelsCleared = 1;
    public int EnergyAvailable = 1;

    public int SwordLevel = 1;
    public int ArmorLevel = 1;
    public int PlayerLevel = 1;

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
        //CreateDefaultPref();
        GamePlayCount = PlayerPrefs.GetInt("GamePlayCount", 0);        
        if (GamePlayCount == 0)
        {
            CreateDefaultPref();
        }
        GamePlayCount++;
        currentDate = System.DateTime.Now;
        TotalCoins = PlayerPrefs.GetInt("TotalCoins", GameGlobalVariablesManager.InitCoins);
        GameGlobalVariablesManager.totalNumberOfCoins = TotalCoins;
        TotalCrystals = PlayerPrefs.GetInt("TotalCrystals", 0);
        LastSavedTime = PlayerPrefs.GetString("LastSavedTime", currentDate.ToBinary().ToString());
        LastDailyBonusTime = PlayerPrefs.GetString("LastDailyBonusTime", currentDate.ToBinary().ToString());
        LastEnergyBonusTime = PlayerPrefs.GetString("LastEnergyBonusTime", currentDate.ToBinary().ToString());
        FirstDailyBonusTime = PlayerPrefs.GetString("FirstDailyBonusTime", currentDate.ToBinary().ToString());

        GameGlobalVariablesManager.EnergyAvailable = PlayerPrefs.GetInt("EnergyAvailable", GameGlobalVariablesManager.InitEnergyAvailable);
        GameGlobalVariablesManager.PlayerLevel  = PlayerPrefs.GetInt("PlayerLevel", 1);
        GameGlobalVariablesManager.SwordLevel = PlayerPrefs.GetInt("SwordLevel", 1);
        GameGlobalVariablesManager.ArmorLevel = PlayerPrefs.GetInt("ArmorLevel", 1);

        GameGlobalVariablesManager.KnifeCount = PlayerPrefs.GetInt("KnifeCount", GameGlobalVariablesManager.InitKnifeCount);
        GameGlobalVariablesManager.BombsCount = PlayerPrefs.GetInt("BombsCount", GameGlobalVariablesManager.InitBombsCount);
        GameGlobalVariablesManager.CycloneCount = PlayerPrefs.GetInt("CycloneCount", GameGlobalVariablesManager.InitCycloneCount);
        GameGlobalVariablesManager.LevelsCleared = PlayerPrefs.GetInt("LevelsCleared", GameGlobalVariablesManager.LevelsCleared);
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
        PlayerPrefs.SetString("FirstDailyBonusTime", FirstDailyBonusTime);
        PlayerPrefs.SetString("LastDailyBonusTime", LastDailyBonusTime);
        PlayerPrefs.SetString("LastEnergyBonusTime", LastEnergyBonusTime);

        PlayerPrefs.SetInt("EnergyAvailable", GameGlobalVariablesManager.EnergyAvailable);
        PlayerPrefs.SetInt("PlayerLevel", GameGlobalVariablesManager.PlayerLevel);
        PlayerPrefs.SetInt("SwordLevel", GameGlobalVariablesManager.SwordLevel);
        PlayerPrefs.SetInt("ArmorLevel", GameGlobalVariablesManager.ArmorLevel);
        PlayerPrefs.SetInt("KnifeCount", GameGlobalVariablesManager.KnifeCount);
        PlayerPrefs.SetInt("BombsCount", GameGlobalVariablesManager.BombsCount);
        PlayerPrefs.SetInt("CycloneCount", GameGlobalVariablesManager.CycloneCount);
        PlayerPrefs.SetInt("LevelsCleared", GameGlobalVariablesManager.LevelsCleared);        
        PlayerPrefs.Save();
    }


    public void CreateDefaultPref()
    {
        PlayerPrefs.SetInt("GamePlayCount", GamePlayCount);
        PlayerPrefs.SetInt("TotalCoins", GameGlobalVariablesManager.InitCoins);
        PlayerPrefs.SetInt("TotalCrystals", TotalCrystals);
        PlayerPrefs.SetInt("EnergyAvailable", GameGlobalVariablesManager.InitEnergyAvailable);
        PlayerPrefs.SetInt("PlayerLevel", GameGlobalVariablesManager.PlayerLevel);
        PlayerPrefs.SetInt("SwordLevel", GameGlobalVariablesManager.SwordLevel);
        PlayerPrefs.SetInt("ArmorLevel", GameGlobalVariablesManager.ArmorLevel);
        PlayerPrefs.SetInt("KnifeCount", GameGlobalVariablesManager.InitKnifeCount);
        PlayerPrefs.SetInt("BombsCount", GameGlobalVariablesManager.InitBombsCount);
        PlayerPrefs.SetInt("CycloneCount", GameGlobalVariablesManager.InitCycloneCount);
        PlayerPrefs.SetInt("LevelsCleared", GameGlobalVariablesManager.InitLevelsCleared);

        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        LastDailyBonusTime = LastSavedTime;
        LastEnergyBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("FirstDailyBonusTime", LastSavedTime);
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

    public void OnFirstBonusGiven()
    {
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        LastDailyBonusTime = LastSavedTime;
        FirstDailyBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastDailyBonusTime", LastDailyBonusTime);
        PlayerPrefs.SetString("FirstDailyBonusTime", LastDailyBonusTime);        
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


    public void OnEnergyUsed()
    {
        LastSavedTime = currentDate.ToBinary().ToString();
        LastEnergyBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetInt("EnergyAvailable", EnergyAvailable);
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
        long daily;
        try
        {
            daily = System.Convert.ToInt64(LastDailyBonusTime);
        }
        catch (System.Exception e)
        {
            System.DateTime curDate = System.DateTime.Now;
            daily = currentDate.ToBinary();
        }
        return daily;
    }


    public long GetFirstBonusTime()
    {
        long daily;
        try
        {
            daily = System.Convert.ToInt64(FirstDailyBonusTime);
        }
        catch (System.Exception e)
        {
            System.DateTime curDate = System.DateTime.Now;
            daily = currentDate.ToBinary();
        }
        return daily;
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
