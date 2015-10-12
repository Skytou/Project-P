using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class OneSignalPushNotificationManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Registered for push");
		OneSignal.Init ("65870fdc-70cf-11e5-b414-c77cb008632e","419912205759",HandleNotification);
	}

	private static void HandleNotification(string message, Dictionary<string, object> additionalData, bool isActive) {
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}
