using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OpenAppAndroid : MonoBehaviour
{

    bool isGalleryImageLoaded = false;
    public Image curImg;
    List<string> apps;
    int curAppIndex = 0;
    public Text curAppText;

    void Start()
    {
        // case sensitive
        apps = new List<string>();
        apps.Add("com.skyTou.kaththi3dgameAndroid");
        apps.Add("air.CleanIndiaMission");
        apps.Add("com.skytou.pokkiripongal");
        apps.Add("com.skytou.Gabbar");
        curAppText.text = "Open[" + curAppIndex + "]" + apps[curAppIndex];
    }


    void Update()
    {
    }


    public void OnOpen()
    {
		if( Application.platform != RuntimePlatform.Android )
			return;
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        bool chkVal = currentActivity.Call<bool>("appInstalledOrNot", "com.SkyTou.PuppyWorld"); //com.SkyTou.PuppyWorld
        Debug.Log("chkVal : " + chkVal);
    }


    public void OncallShareApp()
    {
		if( Application.platform != RuntimePlatform.Android )
			return;
        string subject = "WORD-O-MAZE";
        string body = "PLAY THIS AWESOME. GET IT ON THE PLAYSTORE";

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("shareText", subject, body);
    }


    public void OnOpenApp()
    {
		if( Application.platform != RuntimePlatform.Android )
			return;
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("openApp", apps[curAppIndex]); //com.SkyTou.PuppyWorld

        curAppIndex++;
        if (curAppIndex >= apps.Count)
        {
            curAppIndex = 0;
        }
        curAppText.text = "Open[" + curAppIndex + "]" + apps[curAppIndex];
    }


    public void OnPhotoPick(string filePath)
    {
        Debug.Log("OnPhotoPick : " + filePath);
    }
}
