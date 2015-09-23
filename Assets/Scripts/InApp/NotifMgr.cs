using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31;

public class NotifMgr : MonoBehaviour {

    private int oneHrNotificationId;
    private int twoHrNotificationId;
    private int oneDayNotificationId;

    long SecInDay = 24 * 60 * 60;

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
    }


    void Update()
    {
    }


    void UpdateUI()
    {
    }


    public void SetNotif_1Hr()
    {
        var noteConfig = new AndroidNotificationConfiguration(10, "Got an energy", "Play the epic clash", "havefun")
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
        var noteConfig = new AndroidNotificationConfiguration(2 * 60 * 60, "Got an energy", "Play the epic clash", "havefun")
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
        var noteConfig = new AndroidNotificationConfiguration(SecInDay, "Collect your daily bonus", "Play the epic clash", "havefun")
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


    public void EnergyRefillCheck()
    {
        /*
        System.DateTime currentDate = System.DateTime.Now;
        long oldDateLong = SavedData.Inst.GetLastBonusTime();
        System.DateTime oldDate = System.DateTime.FromBinary(oldDateLong);

        System.TimeSpan diff = currentDate.Subtract(oldDate);

        if (diff.TotalSeconds >= 10)
        {
            Debug.Log("BONUS BONUS BONUS");
            GiveDailyBonus();
        }
        else
        {
            Debug.Log("BONUS After : " + (SecInDay - diff.TotalSeconds) + " Seconds");
        }
        */
    }


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
        SavedData.Inst.OnBonusGiven();
    }


    public void ChkForNotif()
    {
        EtceteraAndroid.checkForNotifications();
        Debug.Log("checkForNotifications");
    }
}
