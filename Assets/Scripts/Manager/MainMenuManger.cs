using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class MainMenuManger : MonoBehaviour 
{


	public GameObject storeUI;

	//public GameObject backButton;

	// Use this for initialization
	void Start () 
	{
		storeUI.SetActive (false);

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


	public void Back()
	{
		storeUI.SetActive (false);
	}

	public void Store()
	{
		storeUI.SetActive (true);
	}
	 
}
