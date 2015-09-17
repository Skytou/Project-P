using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BonusMgr : MonoBehaviour {

    float BonusTime = 10; // bonus after 10 seconds
    float prevTime = 0;
    float curTime = 0;

    public Text BonusText;
    public Text PrevTimeText;
    public Text CurTimeText;

	void Start () {
    }
	

	void Update () {
        BonusTime -= Time.deltaTime;
        if (BonusTime < 0)
        {
            BonusTime = 10;
            prevTime = curTime;
            curTime++;
        }
        int BonusTimeInt = (int)BonusTime;
        BonusText.text = "B: " + BonusTimeInt.ToString();
        PrevTimeText.text = "p : " + prevTime.ToString();
        CurTimeText.text = "c : " + curTime.ToString();
    }


    void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("curTime", curTime);
        PlayerPrefs.SetFloat("BonusTime", BonusTime);
        PlayerPrefs.Save();
    }

}
