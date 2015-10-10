using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Prime31;

public class NotifMgr : MonoBehaviour {

    public GameObject dailyBonusPopup;
    public GameObject CollectDailyBonusPopup;
    public Text DayCountText;

    private int oneHrNotificationId;
    private int twoHrNotificationId;
    private int oneDayNotificationId;

    long SecInDay = 24 * 60 * 60;
    long EnergyRefillTime = 30 * 60; // 30 minutes = 1 energy
    float curTime = 0;
    float curTimeVal = 1;
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

        Debug.Log("play count : " + SavedData.Inst.GetGamePlayCount().ToString());
        if (SavedData.Inst.GetGamePlayCount() == 1)
        {
            // set energy notif
            SetNotif_1Hr();
            // set daily bonus notif
            SetNotif_24Hr();
        }

        CollectDailyBonusPopup.SetActive(false);
        dailyBonusPopup.SetActive(false);

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
        //timeRemain.text = GetTimeDiff().ToString();
    }


    public void SetNotif_1Hr()
    {
        var noteConfig = new AndroidNotificationConfiguration(EnergyRefillTime, "Epic Clash - The Puli", "Got an energy, conitnue the game", "Have fun")
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
        var noteConfig = new AndroidNotificationConfiguration(SecInDay, "Epic Clash - The Puli", "Play daily to collect your daily bonus", "Have fun")
        {
            extraData = "one-day-note",
            groupKey = "my-note-group",
            smallIcon = "app_icon"
        };

        System.DateTime currentDate = System.DateTime.Now;
        //Debug.Log("SetNotif_24Hr : " + currentDate.ToString());
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
        GameGlobalVariablesManager.IncreaseEnergy();
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
            long firstDateLong = SavedData.Inst.GetFirstBonusTime();
            System.DateTime firstDate = System.DateTime.FromBinary(firstDateLong);
            System.TimeSpan firstDiff = currentDate.Subtract(firstDate);

            double dayCount = firstDiff.TotalSeconds / SecInDay;
            if (dayCount > 30)
                dayCount = 30;
            if (dayCount <= 0)
                dayCount = 1;
            Debug.Log("OnGiveDailyBonus" + firstDate.ToString() + " = " + oldDate.ToString());

            OnGiveDailyBonus((int)dayCount);
        }
    }


    public void OnGiveDailyBonusBtn()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        dailyBonusPopup.SetActive(true);
        CollectDailyBonusPopup.SetActive(false);
    }


    public void OnGiveDailyBonus(int dayCount)
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        GameGlobalVariablesManager.totalNumberOfCoins += GameGlobalVariablesManager.DailyBonusCoins;

        DayCountText.text = "Day " + dayCount;

        Debug.Log("save it to pref");

        // save it to pref
        if (dayCount == 1)
            SavedData.Inst.OnFirstBonusGiven();
        else
            SavedData.Inst.OnDailyBonusGiven();
        SavedData.Inst.SaveAllData();
        SetNotif_24Hr();

        CollectDailyBonusPopup.SetActive(true);
        dailyBonusPopup.SetActive(false);
    }


    public void OnClosePopup()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        dailyBonusPopup.SetActive(false); 
        CollectDailyBonusPopup.SetActive(false);
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
