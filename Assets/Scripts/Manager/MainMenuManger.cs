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
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        if (SavedData.Inst.GetGamePlayCount() <= 1)
            Application.LoadLevel(GameGlobalVariablesManager.TutorialScene);
        else
            Application.LoadLevel(GameGlobalVariablesManager.LevelSelection);
    }

    public void OnTutorialClick()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Application.LoadLevel(GameGlobalVariablesManager.TutorialScene);
    }

    public void Store()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Application.LoadLevel(GameGlobalVariablesManager.StoreScene);
    }

    public void Credits()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
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
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        AudioMgr.Inst.MusicToggle();
    }


    public void OnEnergy()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        //show video reward ads
    }

	public void ShowRewardedAd()
	{
		Debug.Log ("Video ad reward");
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
			GameGlobalVariablesManager.EnergyAvailable+=1;
			SavedData.Inst.SaveAllData ();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
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

