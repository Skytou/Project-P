using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class MainMenuManger : MonoBehaviour 
{
	

	// Use this for initialization
	void Start () 
	{
		if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}
	}


	public void Play()
	{
		
		Application.LoadLevel (2);
	}


	public void Store()
	{
		
	}
	 
}
