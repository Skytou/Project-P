using UnityEngine;
using System.Collections;

public class SavedData : MonoBehaviour
{
    public int GamePlayCount = 0;
    public int TotalCoins = 0;
    public int TotalCrystals = 0;
    public string LastSavedTime = "";
    public string LastBonusTime = "";
    public float DailyBonusTime = 10;//24 * 60 * 60; // seconds of the day
    public float EneryFillerTime = 5 * 60; // 5 min = 1 energy

    System.DateTime currentDate;
    System.DateTime oldDate;

	void Start () {
        LoadSavedData();
	}


	void Update () {
	}


    public void LoadSavedData()
    {
        currentDate = System.DateTime.Now;
        long temp = System.Convert.ToInt64(PlayerPrefs.GetString("LastSavedTime"));

        System.DateTime oldDate = System.DateTime.FromBinary(temp);
        Debug.Log("oldDate: " + oldDate);

        System.TimeSpan difference = currentDate.Subtract(oldDate);
        Debug.Log("Seconds: " + difference.TotalSeconds);
        Debug.Log("Min: " + difference.TotalMinutes);
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


    public float TimeDiff()
    {
        return 0;
    }


    void OnApplicationQuit()
    {
        SaveAllData();
    }


}
