using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AIGameObjectInSections
{
	public GameObject[] areaLockCollider;
	public List<GameObject> ai;
    public string textToBeDisplayed;
    public string textOnFightOver;
    public Vector3 cameraLockPos;
	public int totalNumberOfAIVisible;
}


/* Level Manager - Takes care of the properties of the level */
public class LevelManager : MonoBehaviour 
{
	public static LevelManager instance = null;
	//public GameObject[] aiPrefab;
	//public GameObject[] spawnPoiints;

	public string  dialogueHUDTextLevelStart ,dialogueHUDTextLevelEnd ;

	public List<AIGameObjectInSections> aiGameObjectsInSections;

	//public bool spawnAI1, spawnAI2, spawnAI3, spawnAI4;
	public GameObject[] doorsToBeOpened; 

	public bool[] activateAISpawn;

    public Text helpText;
    public Text helpTextShadow;

	public GameObject cameraRef;

	public bool[] stageCompleted;

	public GameObject portal;
    bool isPortalOpen = false;

    public int StageCompleteIndex;
	public bool levelCompleted;

	void Awake()
	{
		Time.timeScale = 1.0f;
		instance = this;
		activateAISpawn = new bool[10];
		stageCompleted = new bool[10];
		GameGlobalVariablesManager.isCameraLocked = false;
		portal.SetActive (false);

        StageCompleteIndex = aiGameObjectsInSections.Count;
	}


	void Start()
	{
		/*if(Advertisement.IsReady())
		{
            if (GameGlobalVariablesManager.IsShowAd)
			    Advertisement.Show ();
			Debug.Log ("Showing ad");
		} */

		InGameHUD.instance.EnableDialogueHUD (dialogueHUDTextLevelStart);

        if (helpText == null)
        {
            helpText = GameObject.FindGameObjectWithTag("HelpText").GetComponent<Text>() as Text;
        }
        if (helpTextShadow == null)
        {
            helpTextShadow = GameObject.FindGameObjectWithTag("HelpTextShadow").GetComponent<Text>() as Text;
        }

		switch(GameGlobalVariablesManager.currentLevelnumber)
		{
		case 0:
			helpText.text = "Find clue about the old wisp!";
			break;

		case 2:
			helpText.text = "Crack the pots to find extra coins";
			break;

		case 4:
			helpText.text = "Game On!";
			break;

		case 6:
			helpText.text = "Game On!";
			break;
		}
        helpText.text = "Game On!";
        helpTextShadow.text = helpText.text;
	}

	public void SpawnAI(int index)
	{
        if (isPortalOpen)
            return;

		var limit = index;// - 1;
		 
		{
			 
			if(aiGameObjectsInSections [limit].ai.Count >0)
			{
				if(!aiGameObjectsInSections [limit].ai.Contains(null))
				{
					for(int i =0;i<aiGameObjectsInSections [limit].ai.Count;i++)
					{
						if(i<aiGameObjectsInSections [limit].totalNumberOfAIVisible)
						{
							aiGameObjectsInSections [limit].ai [i].SetActive (true);
							 
						}
					}
				}

				else
				{
					aiGameObjectsInSections [limit].ai.RemoveAll (item => item == null);
					//Debug.Log ("removing ai form index");
				}
			}
			else
			{
                Debug.Log("Limit reached : " + aiGameObjectsInSections.Count);
				// TODO: unlock the next area collider  , call camera movement
				//if(limit>aiGameObjectsInSections.Count )

                if (limit < aiGameObjectsInSections.Count - 1)
				{
                    for (int i = 0; i < aiGameObjectsInSections[limit].areaLockCollider.Length; i++)
                    {
                        if (aiGameObjectsInSections[limit].areaLockCollider[i] != null)
                            aiGameObjectsInSections[limit].areaLockCollider[i].SetActive(false);
                    }
					activateAISpawn [limit] = false;
					stageCompleted [limit] = true;

					GameGlobalVariablesManager.isCameraLocked = false;
                    Debug.Log("index: " + index + "limit :" + limit + "len" + activateAISpawn.Length);
				}
				else
				{
                    switch (GameGlobalVariablesManager.currentLevelnumber)
					{
					case 0: 
						InGameHUD.instance.EnableDialogueHUD (dialogueHUDTextLevelEnd);
						portal.SetActive (true);
                        isPortalOpen = true;
						break;

					case 2:
						InGameHUD.instance.EnableDialogueHUD (dialogueHUDTextLevelEnd);
						portal.SetActive (true);
                        isPortalOpen = true;
						break;

					case 4:
						InGameHUD.instance.EnableDialogueHUD (dialogueHUDTextLevelEnd);
						portal.SetActive (true);
                        isPortalOpen = true;
						break;

					case 6:
                        Debug.Log("portal open");
						InGameHUD.instance.EnableDialogueHUD (dialogueHUDTextLevelEnd);
						portal.SetActive (true);
                        isPortalOpen = true;
						break;
					}
				}
			}
		}
	}

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
		if(activateAISpawn[0])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			cameraRef.transform.position = aiGameObjectsInSections [0].cameraLockPos;
			helpText.text = aiGameObjectsInSections [0].textToBeDisplayed;
            helpTextShadow.text = helpText.text;
            SpawnAI(0);
		}
		if(activateAISpawn[1])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			cameraRef.transform.position = aiGameObjectsInSections [1].cameraLockPos;
			helpText.text = aiGameObjectsInSections [1].textToBeDisplayed;
            helpTextShadow.text = helpText.text;
            SpawnAI(1);
		}
		if(activateAISpawn[2])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			cameraRef.transform.position = aiGameObjectsInSections [2].cameraLockPos;
			helpText.text = aiGameObjectsInSections [2].textToBeDisplayed;
            helpTextShadow.text = helpText.text;
            SpawnAI(2);
		}
		if(activateAISpawn[3])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			cameraRef.transform.position = aiGameObjectsInSections [3].cameraLockPos;
			helpText.text = aiGameObjectsInSections [3].textToBeDisplayed;
            helpTextShadow.text = helpText.text;
            SpawnAI(3);
		}
		if(activateAISpawn[4])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			cameraRef.transform.position = aiGameObjectsInSections [4].cameraLockPos;
			helpText.text = aiGameObjectsInSections [4].textToBeDisplayed;
            helpTextShadow.text = helpText.text;
            SpawnAI(4);
		}
		if(activateAISpawn[5])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			cameraRef.transform.position = aiGameObjectsInSections [5].cameraLockPos;
			helpText.text = aiGameObjectsInSections [5].textToBeDisplayed;
            helpTextShadow.text = helpText.text;
            SpawnAI(5);
		}
		if(activateAISpawn[6])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			cameraRef.transform.position = aiGameObjectsInSections [6].cameraLockPos;
			helpText.text = aiGameObjectsInSections [6].textToBeDisplayed;
            helpTextShadow.text = helpText.text;
            SpawnAI(6);
		}
	}


    public bool OpenPortal()
    {
        return isPortalOpen;
    }

    public void ClosePortal()
    {
        isPortalOpen = false;
    }
}


