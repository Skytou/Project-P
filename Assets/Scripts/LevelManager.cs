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
	public Vector3 cameraLockPos;
	public int totalNumberOfAIVisible;
}


/* Level Manager - Takes care of the properties of the level */
public class LevelManager : MonoBehaviour 
{
	public static LevelManager instance = null;
	//public GameObject[] aiPrefab;
	//public GameObject[] spawnPoiints;

	public float playerHealth;
	public int levelNumber;
	public string  dialogueHUDTextLevelStart ,dialogueHUDTextLevelEnd ;

	public List<AIGameObjectInSections> aiGameObjectsInSections;

	//public bool spawnAI1, spawnAI2, spawnAI3, spawnAI4;
	public GameObject[] doorsToBeOpened; 

	public bool[] activateAISpawn;

	public Text helpText;

	public GameObject cameraRef;


	 
	public bool[] stageCompleted;

	public GameObject portal;
	 
	 
	public bool levelCompleted;

	void Awake()
	{
		Time.timeScale = 1.0f;
		instance = this;
		activateAISpawn = new bool[10];
		stageCompleted = new bool[10];
		GameGlobalVariablesManager.isCameraLocked = false;
		portal.SetActive (false);
		GameGlobalVariablesManager.playerHealth =playerHealth;
	}


	void Start()
	{
		if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		} 

		InGameHUD.instance.EnableDialogueHUD (dialogueHUDTextLevelStart);

		helpText = GameObject.FindGameObjectWithTag ("HelpText").GetComponent<Text>();
		 
		switch(levelNumber)
		{
		case 1:
			helpText.text = "Find the document about the old wisp!";
			break;

		case 2:
			helpText.text = "Crack the pots to find extra coins";
			break;

		case 3:
			helpText.text = "Game On!";
			break;

		case 4:
			helpText.text = "Game On!";
			break;
		}

	}

	public void SpawnAI(int index)
	{
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
				Debug.Log ("Limit reached");
				// TODO: unlock the next area collider  , call camera movement
				if(limit<aiGameObjectsInSections.Count )
				{
					for (int i = 0; i < aiGameObjectsInSections [limit].areaLockCollider.Length; i++)
						aiGameObjectsInSections [limit].areaLockCollider [i].SetActive (false);
					activateAISpawn [index] = false;
					stageCompleted [index] = true;
					//doorsToBeOpened [index].GetComponent<Doors> ().OpenDoor ();
					//helpText.text = "Crack the pot!";
					GameGlobalVariablesManager.isCameraLocked = false;
				}



				// for level 3 we have index = 4
				// for level 4 we have index = 5

				switch(levelNumber)
				{
				case 1:

					if(index==6)
					{

						InGameHUD.instance.EnableDialogueHUD (dialogueHUDTextLevelEnd);
						GameGlobalVariablesManager.currentLevelnumber = levelNumber;
						portal.SetActive (true);

					}

					break;


				case 2:

					break;


				case 3:

					break;


				case 4:

					break;
				}

			}
		}
	}

	void Update()
	{
		//if(Input.GetKeyDown(KeyCode.Space))
		if(activateAISpawn[0])
		{
			//GameGlobalVariablesManager.isCameraLocked = true;
			//cameraRef.transform.position = aiGameObjectsInSections [0].cameraLockPos;
			helpText.text = aiGameObjectsInSections [0].textToBeDisplayed;
			SpawnAI (0);
		}
		if(activateAISpawn[1])
		{
			//GameGlobalVariablesManager.isCameraLocked = true;
			//cameraRef.transform.position = aiGameObjectsInSections [1].cameraLockPos;
			helpText.text = aiGameObjectsInSections [1].textToBeDisplayed;
			SpawnAI (1);
		}
		if(activateAISpawn[2])
		{
			//GameGlobalVariablesManager.isCameraLocked = true;
			//cameraRef.transform.position = aiGameObjectsInSections [2].cameraLockPos;
			helpText.text = aiGameObjectsInSections [2].textToBeDisplayed;
			SpawnAI (2);
		}
		if(activateAISpawn[3])
		{
			//GameGlobalVariablesManager.isCameraLocked = true;
			//cameraRef.transform.position = aiGameObjectsInSections [3].cameraLockPos;
			helpText.text = aiGameObjectsInSections [3].textToBeDisplayed;
			SpawnAI (3);
		}
		if(activateAISpawn[4])
		{
			//GameGlobalVariablesManager.isCameraLocked = true;
			//cameraRef.transform.position = aiGameObjectsInSections [4].cameraLockPos;
			helpText.text = aiGameObjectsInSections [4].textToBeDisplayed;
			SpawnAI (4);
		}
		if(activateAISpawn[5])
		{
			//GameGlobalVariablesManager.isCameraLocked = true;
			//cameraRef.transform.position = aiGameObjectsInSections [5].cameraLockPos;
			helpText.text = aiGameObjectsInSections [5].textToBeDisplayed;
			SpawnAI (5);
		}
		if(activateAISpawn[6])
		{
			//GameGlobalVariablesManager.isCameraLocked = true;
			//cameraRef.transform.position = aiGameObjectsInSections [6].cameraLockPos;
			helpText.text = aiGameObjectsInSections [6].textToBeDisplayed;
			SpawnAI (6);
		}
	}
}


