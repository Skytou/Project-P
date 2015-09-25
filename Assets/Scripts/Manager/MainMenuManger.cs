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
            if (GameGlobalVariablesManager.IsShowAd)
            {
                Advertisement.Show();
                Debug.Log("Showing ad");
            }			
		}
	}


	public void Play()
	{

        Application.LoadLevel(GameGlobalVariablesManager.LevelSelection);
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

	public void Store()
	{
        Application.LoadLevel(GameGlobalVariablesManager.StoreScene);
	}


	public void OnTutorialClick()
	{
        Application.LoadLevel(GameGlobalVariablesManager.TutorialScene);
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
