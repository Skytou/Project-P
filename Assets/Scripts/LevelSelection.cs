using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
	public GameObject[] castle;

	bool isTouched;

	public RectTransform levelBG;
	public float dragSpeed;

	public GameObject loadingScreen;
	//public Slider loadingSlider;

	public Vector2 bgRectTransform;

	//bool isShowProgressBar = false;

    bool[] LevelStatus = new bool[9];
    bool[] HorseLevelStatus = new bool[8];

    public GameObject[] castleLock;
    public GameObject[] horseLock;
    
	// Use this for initialization
	void Start () 
	{
		//isTouched = false;
		loadingScreen.SetActive ((false));
		for(int i =1;i<castle.Length;i++)
		{
			if(GameGlobalVariablesManager.castleLocked[i]==1)
			{
				Color c = castle [i].GetComponent<Image> ().color;
				c.a = 1;
				castle [i].GetComponent<Image> ().color = c;
				//castle [i].GetComponent<Image> ().color.a = new float (255);// 255;
			}
			else
			{
				Color c = castle [i].GetComponent<Image> ().color;
				c.a = 0.5f;
				castle [i].GetComponent<Image> ().color = c;
				//castle [i].GetComponent<Image> ().color.a = 150;
			}
		}

		if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}

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


	public void Level1()
	{
		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/
		loadingScreen.SetActive (true);
		StartCoroutine (LoadLevel (3));
		//LoadLevel1 ();
		//LoadSceneManager.instance.LoadSceneWithTransistion (3,SceneTransition.FishEyeToScene);
		//Application.LoadLevel (3);
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

	public void Level2()
	{
		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/
        if (!LevelStatus[1])
            return;

		loadingScreen.SetActive (true);
		StartCoroutine (LoadLevel (4));
		//LoadSceneManager.instance.LoadSceneWithTransistion (4,SceneTransition.FishEyeToScene);
		//Application.LoadLevel (4);
	}

	public void Level3()
	{
        if (!LevelStatus[2])
            return;

		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/

		loadingScreen.SetActive (true);
		StartCoroutine (LoadLevel (5));
		//LoadSceneManager.instance.LoadSceneWithTransistion (5,SceneTransition.FishEyeToScene);
		//Application.LoadLevel (5);
	}

	public void Level4()
	{
        if (!LevelStatus[3])
            return;

		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/
		loadingScreen.SetActive (true);
		StartCoroutine (LoadLevel (6));
		//LoadSceneManager.instance.LoadSceneWithTransistion (6,SceneTransition.FishEyeToScene);
	}

    public void Level5()
    {
        if (!LevelStatus[4])
            return;

        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(6));
    }

    public void Level6()
    {
        if (!LevelStatus[5])
            return;

        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(6));
    }

    public void Level7()
    {
        if (!LevelStatus[6])
            return;

        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(6));
    }

    public void Level8()
    {
        if (!LevelStatus[7])
            return;

        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(6));
    }

    public void Level9()
    {
        if (!LevelStatus[8])
            return;

        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(6));
    }

    public void OnHorseSelected(int horseLevel)
    {
        if (!HorseLevelStatus[horseLevel - 1])
            return;
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
	// Update is called once per frame
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
	
}
