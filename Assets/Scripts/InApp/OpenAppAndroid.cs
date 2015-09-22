using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OpenAppAndroid : MonoBehaviour
{

    bool isGalleryImageLoaded = false;
    List<string> apps;
    int curAppIndex = 0;
    public Text curAppText;

    private static string fullClassName = "com.znop.unityplugin06.MainActivity";
    private static string unityClass = "com.unity3d.player.UnityPlayerNativeActivity";


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


    public void OnOpenApp()
    {
		if( Application.platform != RuntimePlatform.Android )
			return;

        AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
        if (pluginClass != null)
        {
            pluginClass.CallStatic("OpenApp", apps[curAppIndex]);

        }

        curAppIndex++;
        if (curAppIndex >= apps.Count)
        {
            curAppIndex = 0;
        }
        curAppText.text = "Open[" + curAppIndex + "]" + apps[curAppIndex];
    }

}
