using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

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

    bool[] LevelStatus = new bool[9];
    bool[] HorseLevelStatus = new bool[8];

    public GameObject[] castleLock;
    public GameObject[] horseLock;
    
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

        for (int i = 0; i < HorseLevelStatus.Length; i++)
        {
            HorseLevelStatus[i] = false;
        }
        LevelStatus[0] = true;
        HorseLevelStatus[0] = true;
        UpdateLocksUI();
        ClosePopup();
	}


    void UpdateLocksUI()
    {
        for (int i = 0; i < castleLock.Length; i++)
        {
            castleLock[i].SetActive(!LevelStatus[i]);
        }

        for (int i = 0; i < horseLock.Length; i++)
        {
            horseLock[i].SetActive(!HorseLevelStatus[i]);
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


    public void OnCastleSelected(int level)
    {
        if (!LevelStatus[level])
        {
            ShowPopup("Level is Locked.");
            return;
        }
        else if(GameGlobalVariablesManager.playerEnergy <= 0)
        {
            ShowPopup("Not enough energy.\nBuy from store.");
            return;
        }
        
        switch (level)
        {
            case 0:
            case 4:
            case 8:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle0));
                break;

            case 1:
            case 5:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle1));
                break;

            case 2:
            case 6:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle2));
                break;

            case 3:
            case 7:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneCastle3));
                break;
        }
    }


    public void OnHorseSelected(int horseLevel)
    {
        if (!HorseLevelStatus[horseLevel])
        {
            ShowPopup("Level is Locked");
            return;
        }
        else if (GameGlobalVariablesManager.playerEnergy <= 0)
        {
            ShowPopup("Not enough energy.\nBuy from store.");
            return;
        }

        switch(horseLevel)
        {
            case 0:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse1));
                break;
            case 1:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse2));
                break;
            case 2:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse3));
                break;
            
            case 3:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse1));
                break;
            case 4:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse2));
                break;
            case 5:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse3));
                break;
            
            case 6:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse1));
                break;
            case 7:
                loadingScreen.SetActive(true);
                StartCoroutine(LoadLevelStr(GameGlobalVariablesManager.SceneHorse2));
                break;
        };
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
}
