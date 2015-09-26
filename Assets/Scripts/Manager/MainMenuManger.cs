using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class MainMenuManger : MonoBehaviour 
{
    public GameObject storeUI;

	public Text soundMutedText;
	//public GameObject backButton;

    public GameObject SoundOn;
    public GameObject SoundOff;

    void Start() 
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

    public void OnTutorialClick()
    {
        Application.LoadLevel(GameGlobalVariablesManager.TutorialScene);
    }

    public void Store()
    {
        Application.LoadLevel(GameGlobalVariablesManager.StoreScene);
    }

    public void Credits()
    {
        Application.Quit();
        return;
        // Credits Button
        //Application.LoadLevel(GameGlobalVariablesManager.Credits);
    }

    public void Sound()
    {
        if (!GameGlobalVariablesManager.isSoundMuted)
        {
            GameGlobalVariablesManager.isSoundMuted = true;
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
        }
        else
        {
            GameGlobalVariablesManager.isSoundMuted = false;
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
    }

	void Update()
	{
        UpdateUI();
	}


    void UpdateUI()
    {
        if (GameGlobalVariablesManager.isSoundMuted)
        {
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
        }
        else
        {
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
    }
}

