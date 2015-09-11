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

	// Update is called once per frame
	void Update ()
	{

	}
}
