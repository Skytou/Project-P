using UnityEngine;
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
    
	// Use this for initialization
	void Start () 
	{
		//isTouched = false;
		loadingScreen.SetActive ((false));

		/*if(Advertisement.IsReady())
		{
            if (GameGlobalVariablesManager.IsShowAd)
                Advertisement.Show ();
			Debug.Log ("Showing ad");
		}
*/
        // lock status
        for (int i = 0; i < LevelStatus.Length; i++)
        {
            LevelStatus[i] = false;
        }

        UpdateLocksUI();
        ClosePopup();
        Debug.Log("LevelsCleared : " + GameGlobalVariablesManager.LevelsCleared);
        if (GameGlobalVariablesManager.LevelsCleared < 1)
            OnStoryBtn();
	}


    void UpdateLocksUI()
    {
        for (int i = 0; i < LevelStatus.Length; i++)
        {
            if (i <= 8)
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
        if (!LevelStatus[level])
        {
            ShowPopup("Level is Locked.");
            return;
        }
        else if(GameGlobalVariablesManager.EnergyAvailable <= 0)
        {
            ShowPopup("Not enough energy.\nBuy from store.");
            return;
        }
        
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
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse1));
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
		loadingScreen.SetActive (true);
		StartCoroutine (LoadLevel (1));
	}


	void Update ()
	{
		bgRectTransform = levelBG.anchoredPosition;
		bgRectTransform.x = Mathf.Clamp (bgRectTransform.x, -1463, 1471);
		
		//levelBG.anchoredPosition = new Vector2( Mathf.Clamp(levelBG.anchoredPosition.x,20f,-2400f) ,levelBG.anchoredPosition.y);

		/*if(isShowProgressBar)
		{
			if(loadingSlider.value<=loadingSlider.maxValue)
			loadingSlider.value += Time.deltaTime;

		}*/

        UpdateLocksUI();
	}


    public void ShowPopup(string msg)
    {
        PopupText.text = msg;
        Popup.SetActive(true);
    }


    public void ClosePopup()
    {
        Popup.SetActive(false);
    }



    // story
    public GameObject DialogBox;
    public GameObject Shruthi;
    public GameObject Frog;

    public List<GameObject> Dialog;

    public GameObject LSpeech;
    public GameObject RSpeech;

    int dialog = -1;
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
        Debug.Log("dialog" + dialog);
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


    public void OnStoryBtn()
    {
        DialogBox.SetActive(true);
        dialog = 0;
        ShowDialog();
    }
    

}
