﻿using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelSelection : MonoBehaviour
{
	bool isTouched;

	public RectTransform levelBG;
	public float dragSpeed;

    public GameObject Popup;
    public Text PopupText;
    public GameObject loadingScreen;
    //public Slider loadingSlider;
    //bool isShowProgressBar = false;
	public Vector2 bgRectTransform;

    bool[] LevelStatus = new bool[17];
    public GameObject[] levelLock;
    public int levelsUnlocked = 3;

    // story
    public GameObject DialogBox;
    public GameObject Shruthi;
    public GameObject Frog;

    public List<GameObject> Dialog;

    public GameObject LSpeech;
    public GameObject RSpeech;
    public GameObject StoreBtn;

    int dialog = -1;
    public Text EnergyCountText;

	void Start () 
	{
		//isTouched = false;
		loadingScreen.SetActive ((false));

		if(Advertisement.IsReady())
		{
            if (GameGlobalVariablesManager.IsShowAd)
                Advertisement.Show ();
			Debug.Log ("Showing ad");
		}

        // lock status
        for (int i = 0; i < LevelStatus.Length; i++)
        {
            LevelStatus[i] = false;
        }

        UpdateLocksUI();
        
        // hide popup
        Popup.SetActive(false);

        Debug.Log("LevelsCleared : " + GameGlobalVariablesManager.LevelsCleared);
        if (GameGlobalVariablesManager.LevelsCleared < 1)
            OpenStoryDialog();

        //GameGlobalVariablesManager.EnergyAvailable = 0;
	}


    void UpdateLocksUI()
    {
        for (int i = 0; i < LevelStatus.Length; i++)
        {
            if (i <= GameGlobalVariablesManager.LevelsCleared)
            {
                LevelStatus[i] = true;
            }
            else
            {
                LevelStatus[i] = false;
            }
        }

        for (int i = 0; i < levelLock.Length; i++)
        {
            levelLock[i].SetActive(!LevelStatus[i]);
        }

        EnergyCountText.text = GameGlobalVariablesManager.EnergyAvailable.ToString();
    }


	IEnumerator LoadLevel(int levelNumber)
	{
		AsyncOperation async = Application.LoadLevelAsync(levelNumber);
		yield return async;
		Debug.Log("Loading complete");
	}


    IEnumerator LoadLevelStr(string levelName)
    {
        AsyncOperation async = Application.LoadLevelAsync(levelName);
        yield return async;
        Debug.Log("Loading complete");
    }


    public void OnLevelSelected(int level)
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Debug.Log(level + "vs " + GameGlobalVariablesManager.PlayableLevels);
        if (level >= GameGlobalVariablesManager.PlayableLevels)
        {
            ShowPopup("Coming Soon !!! \n With more action, enemies, powerups and thrilling story");
            return;
        }
        if (!LevelStatus[level])
        {
            ShowPopup("Level is Locked.");
            return;
        }
        else if(GameGlobalVariablesManager.EnergyAvailable <= 0)
        {
            ShowStorePopup("Not enough energy.\nBuy from store.");
            return;
        }

        // reset player
        GameGlobalVariablesManager.PlayerDied = false;
        GameGlobalVariablesManager.playerHealth = 100;

        switch (level)
        {
            case 0:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 0;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle1));
                break;
            case 1:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 1;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse1));
                break;
            case 2:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 2;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle2));
                break;
            case 3:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 3;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse2));
                break;

            case 4:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 4;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle3));
                break;
            case 5:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 5;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse3));
                break;
            case 6:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 6;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle4));
                break;
            case 7:
                loadingScreen.SetActive(true);
                GameGlobalVariablesManager.currentLevelnumber = 7;
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse4));
                break;
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
            case 17:
            case 18:
            case 19:
            case 20:

                break;
        }
    }


	public void OnPointerDown( BaseEventData data)
	{
		//var pointData = (PointerEventData)data;
		isTouched = true;
	}


	public void OnPointerDrag(  BaseEventData data)
	{
		var pointData = (PointerEventData)data;
		if(isTouched)
		{
			if(pointData.delta.x >0)
			{
				
				bgRectTransform.x+= dragSpeed;
				//bgRectTransform.x = Mathf.Clamp (bgRectTransform.x, -1463, 1521);
				levelBG.anchoredPosition = bgRectTransform;
			}
			else if(pointData.delta.x < 0)
			{
				//bgRectTransform = levelBG.anchoredPosition;
				bgRectTransform.x-= dragSpeed;
				//bgRectTransform.x = Mathf.Clamp (bgRectTransform.x, -1463, 1521);
				levelBG.anchoredPosition = bgRectTransform;
			}
            // ui fix
            if (levelBG.anchoredPosition.x > 1500)
            {
                Vector2 newVal = levelBG.anchoredPosition;
                newVal.x = 1500;
                levelBG.anchoredPosition = newVal;
            }
            else if (levelBG.anchoredPosition.x < -1500)
            {
                Vector2 newVal = levelBG.anchoredPosition;
                newVal.x = -1500;
                levelBG.anchoredPosition = newVal;
            }
            //Debug.Log(levelBG.anchoredPosition);
		}
	}


	public void OnPointerUp( BaseEventData data)
	{
		var pointData = (PointerEventData)data;
		isTouched = false;
	}


	public void BackButton()
	{
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		loadingScreen.SetActive (true);
		StartCoroutine (LoadLevel (1));
	}


	void Update ()
	{
		bgRectTransform = levelBG.anchoredPosition;
		bgRectTransform.x = Mathf.Clamp (bgRectTransform.x, -1463, 1471);
        UpdateLocksUI();
	}


    public void ShowPopup(string msg)
    {
        PopupText.text = msg;
        Popup.SetActive(true);        
    }


    public void ShowStorePopup(string msg)
    {
        PopupText.text = msg;
        Popup.SetActive(true);
        StoreBtn.SetActive(true);
    }


    public void ClosePopup()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Popup.SetActive(false);
        StoreBtn.SetActive(false);
    }


    public void OpenStore()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Popup.SetActive(false);
        StoreBtn.SetActive(false);
        Application.LoadLevel(GameGlobalVariablesManager.StoreScene);
    }


    public void OnEnergy()
    {
        if (GameGlobalVariablesManager.EnergyAvailable <= 0)
        {
            ShowStorePopup("Not enough energy.\nBuy from store.");            
        }
        else
        {
            ShowStorePopup("you have " + GameGlobalVariablesManager.EnergyAvailable + " energy.");
        }
    }


    #region Story
    public void ShowDialog()
    {
        for (int i = 0; i < Dialog.Count; i++)
        {
            if (i == dialog)
            {
                Dialog[i].SetActive(true);
                if (dialog == 0 || dialog == 2 || dialog == 6)
                {
                    LSpeech.SetActive(true);
                    RSpeech.SetActive(false);
                }
                else
                {
                    LSpeech.SetActive(false);
                    RSpeech.SetActive(true);
                }
            }
            else
                Dialog[i].SetActive(false);
        }

        if (dialog >= 5)
        {
            Shruthi.SetActive(false);
            Frog.SetActive(true);
        }
        else
        {
            Shruthi.SetActive(true);
            Frog.SetActive(false); 
        }
    }


    public void OnNextBtn()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        //Debug.Log("dialog" + dialog);
        dialog++;
        if (dialog >= 7)
        {
            DialogBox.SetActive(false);
        }
        else
        {
            ShowDialog();
        }
    }

    public void OnSkipBtn()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        dialog = 7;
        DialogBox.SetActive(false);
    }

    public void OnStoryBtn()
    {
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        OpenStoryDialog();
    }


    void OpenStoryDialog()
    {
        DialogBox.SetActive(true);
        dialog = 0;
        ShowDialog();
    }
    #endregion Story

}
