using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; 
using UnityEngine.Advertisements;

public class InGameHUD : MonoBehaviour
{
	public static InGameHUD instance = null;

	public GameObject pauseScreen,gameHUD;

	public Sprite swordOn,swordOff,knifeOn,knifeOff;

	public Sprite timerOn, timerOff;

	public Sprite bombOn, bombOff;
	 
	public Sprite cycloneOn, cycloneOff;

	public GameObject sword, knife, timer, bomb, cyclone;

	public Slider healthBarSlider;
	 
	public GameObject dialogueHUD;
	public Text dialogueHUDText;

    public Text totalCoinsText;
    public Text totalCoinsTextShadow;

    public Text TotalKnifeText;
    public Text TotalBombText;
    public Text TotalCycloneText;

    public GameObject freezeui;

	void Awake()
	{
		instance = this;
	}


	void Start ()
	{
		healthBarSlider.maxValue = GameGlobalVariablesManager.playerHealth;
		healthBarSlider.value = GameGlobalVariablesManager.playerHealth;
		pauseScreen.SetActive (false);
		gameHUD.SetActive (true);
		SetSpriteDefault ();
		dialogueHUD.SetActive (false);
	}


	public void EnableDialogueHUD(string text)
	{
		dialogueHUD.SetActive (true);
		dialogueHUDText.text = text;
	}


	public void DisableDialogueHUD()
	{
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		dialogueHUD.SetActive (false);
        if (GameGlobalVariablesManager.PlayerDied)
        {
            Application.LoadLevel(GameGlobalVariablesManager.LevelSelection);
        }
	}


	void SetSpriteDefault()
	{
		sword.GetComponent<Image>().sprite = swordOn;
		knife.GetComponent<Image> ().sprite = knifeOn;

		timer.GetComponent<Image>().sprite = timerOn;
		bomb.GetComponent<Image>().sprite = bombOn;
		cyclone.GetComponent<Image>().sprite = cycloneOn;
	}


	void SetSpriteUpdated()
	{
		if(GameGlobalVariablesManager.isPlayerSpin)
		{
			cyclone.GetComponent<Image>().sprite = cycloneOff;
		}
		else
		{
			cyclone.GetComponent<Image>().sprite = cycloneOn;
		}

		if(GameGlobalVariablesManager.isKnifeThrow)
		{
			knife.GetComponent<Image>().sprite = knifeOn;
			sword.GetComponent<Image>().sprite = swordOff;
		}
		else
		{
			knife.GetComponent<Image>().sprite = knifeOff;
			sword.GetComponent<Image>().sprite = swordOn;
		}
        if (GameGlobalVariablesManager.isCameraLocked)
            freezeui.SetActive(true);
        else
            freezeui.SetActive(false);
    }


	public void Knife()
	{
        if (GameGlobalVariablesManager.KnifeCount > 0 
            && !GameGlobalVariablesManager.isPlayerSpin)
		{
            if (!GameGlobalVariablesManager.isKnifeThrow)
            {
                AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
                GameGlobalVariablesManager.isKnifeThrow = true;
            }
		}
		else
		{
            //ponz.2do
			Debug.Log ("show pop up to store");
		}
	}


	public void Sword()
	{
		if(GameGlobalVariablesManager.isKnifeThrow)
        {
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
			GameGlobalVariablesManager.isKnifeThrow = false;
        }
	}


	public void Timer()
	{	 
	}


	public void Bomb()
	{
        if (!GameGlobalVariablesManager.isBombActivated && GameGlobalVariablesManager.BombsCount>0)
		{
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
			GameGlobalVariablesManager.isBombActivated = true;
            GameGlobalVariablesManager.BombsCount -= 1;
		}
        else
        {
            //ponz.2do
            Debug.Log("show pop up to store");
        }
	}


	public void Cyclone()
	{
        if (!GameGlobalVariablesManager.isPlayerSpin && GameGlobalVariablesManager.CycloneCount > 0)
		{
            AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
			//Debug.Log ("cyclone");
			GameGlobalVariablesManager.isPlayerSpin = true;
			GameGlobalVariablesManager.isKnifeThrow = false;
            GameGlobalVariablesManager.CycloneCount -= 1;
        }
        else
        {
            //ponz.2do
            Debug.Log("show pop up to store");
        }	
	}

	
    public void Pause()
	{
		if(Advertisement.IsReady())
		{
            if (GameGlobalVariablesManager.IsShowAd)
                Advertisement.Show();
			Debug.Log ("Showing ad");
		}
		//LoadSceneManager.instance.LoadSameSceneWithTransistion (SceneTransition.RipplesToSameScene);
		pauseScreen.SetActive (true);
		gameHUD.SetActive (false);
		Time.timeScale = 0f;
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
	}

	
    public void Resume()
	{
		//var values = Enum.GetValues (typeof(SceneTransistionSelf));
		//SceneTransistionSelf s = (SceneTransistionSelf)values ( Random.Range (0, values.Length));
		//LoadSceneManager.instance.LoadSameSceneWithTransistion (SceneTransistionSelf.RipplesToSameScene );//  SceneTransistionSelf.RipplesToSameScene);
		pauseScreen.SetActive (false);
		gameHUD.SetActive (true);
		Time.timeScale = 1.0f;
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
	}

	
    public void Restart()
	{
		Time.timeScale = 1.0f;
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		Application.LoadLevel (Application.loadedLevel);
	}

	
    public void Menu()
	{
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
        Application.LoadLevel(GameGlobalVariablesManager.LevelSelection);
	}

	 
	void Update()
	{
		SetSpriteUpdated ();
		healthBarSlider.value = GameGlobalVariablesManager.playerHealth;
		totalCoinsText.text = "" + GameGlobalVariablesManager.totalNumberOfCoins;
        totalCoinsTextShadow.text = "" + GameGlobalVariablesManager.totalNumberOfCoins;

        TotalKnifeText.text = "" + GameGlobalVariablesManager.KnifeCount;
        TotalBombText.text = "" + GameGlobalVariablesManager.BombsCount;
        TotalCycloneText.text = "" + GameGlobalVariablesManager.CycloneCount;
	}


}
