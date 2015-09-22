using UnityEngine;
using System.Collections;
using Prime31;

public class SavedData
{
    public int GamePlayCount = 0;
    public int TotalCoins = 0;
    public int TotalCrystals = 0;
    public string LastSavedTime = "";
    public string LastBonusTime = "";

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
        TotalCoins = PlayerPrefs.GetInt("TotalCoins");
        TotalCrystals = PlayerPrefs.GetInt("TotalCrystals");
        LastSavedTime = PlayerPrefs.GetString("LastSavedTime");
        LastBonusTime = PlayerPrefs.GetString("LastBonusTime");
    }


    public void SaveAllData()
    {
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        PlayerPrefs.SetInt("GamePlayCount", GamePlayCount);
        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        PlayerPrefs.SetInt("TotalCrystals", TotalCrystals);
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastBonusTime", LastBonusTime);
        PlayerPrefs.Save();
    }


    public void CreateDefaultPref()
    {
        PlayerPrefs.SetInt("GamePlayCount", GamePlayCount);
        PlayerPrefs.SetInt("TotalCoins", TotalCoins);
        PlayerPrefs.SetInt("TotalCrystals", TotalCrystals);
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        LastBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastBonusTime", LastBonusTime);
        PlayerPrefs.Save();
    }


    public void OnBonusGiven()
    {
        currentDate = System.DateTime.Now;
        LastSavedTime = currentDate.ToBinary().ToString();
        LastBonusTime = LastSavedTime;
        PlayerPrefs.SetString("LastSavedTime", LastSavedTime);
        PlayerPrefs.SetString("LastBonusTime", LastBonusTime);
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
        return System.Convert.ToInt64(LastBonusTime);
    }


    public long GetLastSavedTime()
    {
        return System.Convert.ToInt64(LastBonusTime);
    }

}
