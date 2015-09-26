using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31;

public class NotifMgr : MonoBehaviour {

    public GameObject dailyBonusPopup;

    private int oneHrNotificationId;
    private int twoHrNotificationId;
    private int oneDayNotificationId;

    public Text timeRemain;
    long SecInDay = 24 * 60 * 60;
    long EnergyRefillTime = 5 * 60;
    float curTime = 0;
    float curTimeVal = 1;
    bool isDailyBonus = false;
    int tapCount = 0;

    void OnEnable()
    {
        EtceteraAndroidManager.notificationReceivedEvent += notificationReceivedEvent;
    }


    void OnDisable()
    {
        EtceteraAndroidManager.notificationReceivedEvent -= notificationReceivedEvent;
    }


    void Start()
    {
        SavedData.Inst.LoadSavedData();
        ChkForNotif();
        
        if (SavedData.Inst.GetGamePlayCount() == 1)
        {
            OnGiveDailyBonus();
            // set energy notif
            SetNotif_1Hr();
            // set daily bonus notif
            SetNotif_24Hr();
        }
        DailyBonusCheck();
        EnergyRefillCheck();
        UpdateUI();
    }


    void Update()
    {
        // check per second convert this to timer
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            curTime = curTimeVal;
            DailyBonusCheck();
            EnergyRefillCheck();
            UpdateUI();
        }
    }


    void UpdateUI()
    {
        if (isDailyBonus)
        {
            timeRemain.text = "Collect";
        }
        else
        {
            timeRemain.text = GetTimeDiff().ToString();
        }
    }


    public void SetNotif_1Hr()
    {
        var noteConfig = new AndroidNotificationConfiguration(EnergyRefillTime, "Got an energy", "Play the Epic Clash", "Have fun")
        {
            extraData = "one-hour-note",
            groupKey = "my-note-group",
            smallIcon = "app_icon"
        };

        // turn off sound and vibration for this notification
        noteConfig.sound = false;
        noteConfig.vibrate = false;

        System.DateTime currentDate = System.DateTime.Now;
        Debug.Log("SetNotif_1Hr : " + currentDate.ToString());
        oneHrNotificationId = EtceteraAndroid.scheduleNotification(noteConfig);

        SavedData.Inst.SaveAllData();
    }


    public void SetNotif_2Hr()
    {
        var noteConfig = new AndroidNotificationConfiguration(2 * 60 * 60, "Got an energy", "Play the epic clash", "Have fun")
        {
            extraData = "two-hour-note",
            groupKey = "my-note-group",
            smallIcon = "app_icon"
        };

        // turn off sound and vibration for this notification
        noteConfig.sound = false;
        noteConfig.vibrate = false;

        twoHrNotificationId = EtceteraAndroid.scheduleNotification(noteConfig);
    }


    public void SetNotif_24Hr()
    {
        var noteConfig = new AndroidNotificationConfiguration(SecInDay, "Collect your daily bonus", "Play the epic clash", "Have fun")
        {
            extraData = "one-day-note",
            groupKey = "my-note-group",
            smallIcon = "app_icon"
        };

        System.DateTime currentDate = System.DateTime.Now;
        Debug.Log("SetNotif_24Hr : " + currentDate.ToString());
        oneDayNotificationId = EtceteraAndroid.scheduleNotification(noteConfig);
        SavedData.Inst.SaveAllData();
    }


    public void CancelAllNotif()
    {
        EtceteraAndroid.cancelNotification( oneHrNotificationId );
        EtceteraAndroid.cancelNotification( oneDayNotificationId );
        EtceteraAndroid.cancelAllNotifications();
    }


    void notificationReceivedEvent(string extraData)
    {
        Debug.Log("notificationReceivedEvent: " + extraData);
    }


    #region Energy
    public void EnergyRefillCheck()
    {
        System.DateTime currentDate = System.DateTime.Now;
        long oldDateLong = SavedData.Inst.GetLastBonusTime();
        System.DateTime oldDate = System.DateTime.FromBinary(oldDateLong);

        System.TimeSpan diff = currentDate.Subtract(oldDate);

        if (diff.TotalSeconds >= EnergyRefillTime)
        {
            //Debug.Log("Energy");
            GiveEnergyBonus();            
        }
    }


    public void GiveEnergyBonus()
    {
        // save it to pref
        SavedData.Inst.OnDailyBonusGiven();
        SetNotif_1Hr();
    }
    #endregion Energy


    #region DailyBonus
    public void DailyBonusCheck()
    {
        System.DateTime currentDate = System.DateTime.Now;
        long oldDateLong = SavedData.Inst.GetLastBonusTime();
        System.DateTime oldDate = System.DateTime.FromBinary(oldDateLong);

        System.TimeSpan diff = currentDate.Subtract(oldDate);

        if (diff.TotalSeconds >= SecInDay)
        {
            isDailyBonus = true;
        }
        else
        {
            isDailyBonus = false;
            //Debug.Log("BONUS After : " + (SecInDay - diff.TotalSeconds) + " Seconds");
        }
    }


    public void OnGiveDailyBonus()
    {
        if (isDailyBonus)
        {
            GameGlobalVariablesManager.totalNumberOfCoins += GameGlobalVariablesManager.DailyBonusCoins;
            // save it to pref
            SavedData.Inst.OnDailyBonusGiven();
            SetNotif_24Hr();
            dailyBonusPopup.SetActive(true);
        }
    }


    public void OnClosePopup()
    {
        tapCount++;
        if(tapCount >= 2)
        {
            dailyBonusPopup.SetActive(false);
            tapCount = 0;
        }
    }


    // for ui
    string GetTimeDiff()
    {
        string retVal = "";
        System.DateTime currentDate = System.DateTime.Now;
        long oldDateLong = SavedData.Inst.GetLastBonusTime();
        System.DateTime oldDate = System.DateTime.FromBinary(oldDateLong);

        System.TimeSpan diff = currentDate.Subtract(oldDate);
        System.TimeSpan oneDayTime = System.TimeSpan.FromSeconds(SecInDay);
        diff = oneDayTime - diff;

        if(diff.Hours < 10)
            retVal = "0";
        retVal += diff.Hours + ":";
        if(diff.Minutes < 10)
            retVal += "0";
        retVal += diff.Minutes + ":";
        if(diff.Seconds < 10)
            retVal += "0";
        retVal += diff.Seconds;
        return retVal;
    }
    #endregion DailyBonus


    public void ChkForNotif()
    {
        EtceteraAndroid.checkForNotifications();
    }


}
