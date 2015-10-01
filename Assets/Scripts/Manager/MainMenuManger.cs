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

