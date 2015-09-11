using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using UnityEngine.Advertisements;


public class AnalyticsAndAdManager : MonoBehaviour 
{

	void Awake()
	{
		

		Analytics.CustomEvent ("GameStart", null);
	}

	// Use this for initialization
	void Start () 
	{
		if(Advertisement.isSupported)
		{
			Advertisement.Initialize ("677b2d40-84c7-43c7-9757-307e3d7b8a55");	
			Debug.Log ("Ad init");
		}
	}


	 
 		public void ShowRewardedAd()
		{
			if (Advertisement.IsReady("rewardedVideo"))
			{
				var options = new ShowOptions { resultCallback = HandleShowResult };
				Advertisement.Show("rewardedVideo", options);
			}
		}

		private void HandleShowResult(ShowResult result)
		{
			switch (result)
			{
			case ShowResult.Finished:
				Debug.Log("The ad was successfully shown.");
				//
				// YOUR CODE TO REWARD THE GAMER
				// Give coins etc.
				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
				break;
			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
				break;
			}
		}

	// Update is called once per frame
	void Update ()
	{

	}
}
