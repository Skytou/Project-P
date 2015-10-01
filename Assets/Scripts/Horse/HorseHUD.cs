using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HorseHUD : MonoBehaviour
{
	public static HorseHUD instance;
	public string introText;
	public string gameOverWinText;
	public string gameOverLoseText;

    public GameObject helpHudGameObject;

	public Text helpHudText;

	public GameObject pauseMenu;

	public bool isGameOver;
    public bool victory;

	public Sprite lifeOn, lifeOff;

	public Image[] lifeImage;

	private int lifeIndex;


	void Awake()
	{
		instance = this;
	}


	void Start () 
	{
		helpHudGameObject.SetActive (true);
		helpHudText.text = introText;
		pauseMenu.SetActive (false);
		Time.timeScale = 1.0f;
		isGameOver = false;
		SetLifeOnStart ();
        lifeIndex = 2;
    }



	public void SetLifeOnStart()
	{
		for(int i =0;i<lifeImage.Length;i++)
		{
			lifeImage [i].sprite = lifeOn;
		}
	}


	public void SetLifeInGame()
	{
		for(int i =0;i<lifeImage.Length;i++)
		{
            if (i == lifeIndex)
            {
                lifeImage[i].sprite = lifeOff;
                lifeIndex -= 1;
            }
        }
	}


	public void OkButtonHUD()
	{
        if (!isGameOver)
        {
            helpHudGameObject.SetActive(false);
            Time.timeScale = 1.0f;
            //HorseManager.instance.gameRunning = true;
        }
        else
        {
            Application.LoadLevel(GameGlobalVariablesManager.LevelSelection);
            if (victory)
                GameGlobalVariablesManager.OnLevelCleared();
            else
                GameGlobalVariablesManager.OnLevelOver();
        }
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
	}


	public void PauseButton()
	{
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		pauseMenu.SetActive (true);
		Time.timeScale = 0.0f;
	}

	public void ResumeButton()
	{
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		Time.timeScale = 1.0f;
		pauseMenu.SetActive (false);
	}

	public void RestartButton()
	{
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		Time.timeScale = 1.0f;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void MenuButton()
	{
        AudioMgr.Inst.PlaySfx(SfxVals.ButtonClick);
		Time.timeScale = 1.0f;
        Application.LoadLevel(GameGlobalVariablesManager.LevelSelection);
	}

	public void GameOverWin(int coinsCollected)
	{
		helpHudGameObject.SetActive (true);
		helpHudText.text = "Collected : " + coinsCollected + " Coins\n" + gameOverWinText;
		isGameOver = true;
        victory = true;
	}

    public void GameOverLose(int coinsCollected)
    {
        helpHudGameObject.SetActive(true);
        helpHudText.text = "Collected : " + coinsCollected + " Coins\n" + gameOverLoseText;
        isGameOver = true;
        victory = false;
    }

    void Update () 
	{
	}

}
