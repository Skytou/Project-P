using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OpenAppAndroid : MonoBehaviour
{

    bool isGalleryImageLoaded = false;
    public List<string> apps;
    public List<GameObject> imgList;
    int curAppIndex = 0;
    public Text curAppText;

    private static string fullClassName = "com.znop.unityplugin06.MainActivity";
    private static string unityClass = "com.unity3d.player.UnityPlayerNativeActivity";

    void Start()
    {
        // case sensitive
        //curAppText.text = "Open[" + curAppIndex + "]" + apps[curAppIndex];
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
        for (int i = 0; i < imgList.Count; i++)
        {
            imgList[i].SetActive(false);
        }
        imgList[curAppIndex].SetActive(true);
        //curAppText.text = "Open[" + curAppIndex + "]" + apps[curAppIndex];
    }

}
