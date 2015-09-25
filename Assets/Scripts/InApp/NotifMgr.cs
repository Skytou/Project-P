using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31;

public class NotifMgr : MonoBehaviour {

    private int oneHrNotificationId;
    private int twoHrNotificationId;
    private int oneDayNotificationId;

    public Text timeRemain;
    long SecInDay = 24 * 60 * 60;
    long EnergyRefillTime = 60 * 60;
    float curTime = 0;
    float curTimeVal = 1;

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
        ChkForNotif();
        SavedData.Inst.LoadSavedData();
        DailyBonusCheck();
    }


    void Update()
    {
        // check per second convert this to timer
        curTime -= Time.deltaTime;
        if (curTime <= 0)
        {
            curTime = curTimeVal;
            UpdateUI();
            DailyBonusCheck();
            EnergyRefillCheck();
        }
    }


    void UpdateUI()
    {
        timeRemain.text = GetTimeDiff().ToString();
    }


    public void SetNotif_1Hr()
    {
        var noteConfig = new AndroidNotificationConfiguration(1 * 60 * 60, "Got an energy", "Play the epic clash", "Have fun")
        {
            extraData = "one-hour-note",
            groupKey = "my-note-group",
            smallIcon = "app_icon"
        };

        // turn off sound and vibration for this notification
        noteConfig.sound = false;
        noteConfig.vibrate = false;

        oneHrNotificationId = EtceteraAndroid.scheduleNotification(noteConfig);
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
            groupKey = "my-note-group"            
        };
        oneDayNotificationId = EtceteraAndroid.scheduleNotification(noteConfig);
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
            Debug.Log("BONUS BONUS BONUS");
            GiveEnergyBonus();
        }
        else
        {
            Debug.Log("Energy After : " + (EnergyRefillTime - diff.TotalSeconds) + " Seconds");
        }
    }


    public void GiveEnergyBonus()
    {
        // save it to pref
        SavedData.Inst.OnDailyBonusGiven();
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
            Debug.Log("BONUS BONUS BONUS");
            GiveDailyBonus();
        }
        else
        {
            Debug.Log("BONUS After : " + (SecInDay - diff.TotalSeconds) + " Seconds");
        }
    }


    public void GiveDailyBonus()
    {
        // save it to pref
        SavedData.Inst.OnDailyBonusGiven();
    }

    // for ui
    string GetTimeDiff()
    {
        string retVal = "";
        System.DateTime currentDate = System.DateTime.Now;
        long oldDateLong = SavedData.Inst.GetLastBonusTime();
        System.DateTime oldDate = System.DateTime.FromBinary(oldDateLong);

        System.TimeSpan diff = currentDate.Subtract(oldDate);
        retVal = diff.ToString();// need to format
        return retVal;
    }
    #endregion DailyBonus


    public void ChkForNotif()
    {
        EtceteraAndroid.checkForNotifications();
        Debug.Log("checkForNotifications");
    }


}
