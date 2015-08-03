using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;

public class SplashScreenManager : MonoBehaviour 
{

	public float loadGameTime;
	
	public GameObject[] splashScreen;
	public float showSplashScreenTime;


	void OnEnable()
	{
		TransitionKit.onScreenObscured += onScreenObscured;
		TransitionKit.onTransitionComplete += onTransitionComplete;
	}
	
	
	void OnDisable()
	{
		// as good citizens we ALWAYS remove event handlers that we added
		TransitionKit.onScreenObscured -= onScreenObscured;
		TransitionKit.onTransitionComplete -= onTransitionComplete;
	}

	void onScreenObscured()
	{
		 
	}
	
	
	void onTransitionComplete()
	{
	 
	}

	void Awake()
	{
	 	splashScreen[0].SetActive(true);
		splashScreen[1].SetActive(false);
	 
		StartCoroutine(ShowSplashScreen());
		//GoogleMobileAdsDemoScript.instance.HideBanner();
	}
	
 
	
	IEnumerator ShowSplashScreen()
	{
		for (int i = 0; i < splashScreen.Length; i++)
		{
			splashScreen[i].SetActive(true);
			yield return new WaitForSeconds(showSplashScreenTime);
			if(i!=splashScreen.Length-1)  
			splashScreen[i].SetActive(false);
		}
		LoadSceneManager.instance.LoadSceneWithTransistion(1,SceneTransition.RipplesToScene);
		Debug.Log("level loaded");
		//Application.LoadLevel(2);
	}


	IEnumerator LoadGame(float loadTime)
	{
		yield return new WaitForSeconds(loadTime);
		
	}
	 
}
