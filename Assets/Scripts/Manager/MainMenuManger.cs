using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class MainMenuManger : MonoBehaviour 
{


	public GameObject storeUI;

	public Text soundMutedText;
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

	public void Sound()
	{
		if(!GameGlobalVariablesManager.isSoundMuted)
		{
			GameGlobalVariablesManager.isSoundMuted = true;
			soundMutedText.color = Color.red;
		}
		else
		{
			GameGlobalVariablesManager.isSoundMuted = false;
			soundMutedText.color = Color.black;
		}
	}

	public void Back()
	{
		storeUI.SetActive (false);
	}

	public void Store()
	{
		storeUI.SetActive (true);
	}


	public void Quit()
	{
		Application.Quit ();
	}

	void Update()
	{
		if(!GameGlobalVariablesManager.isSoundMuted)
		{
			 
			soundMutedText.color = Color.red;
		}
		else
		{
			 
			soundMutedText.color = Color.black;
		}
	}
	 
}
