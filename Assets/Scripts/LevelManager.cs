using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AIGameObjectInSections
{
	public GameObject[] areaLockCollider;
	public List<GameObject> ai;
	public int totalNumberOfAIVisible;
}


/* Level Manager - Takes care of the properties of the level */
public class LevelManager : MonoBehaviour 
{
	public static LevelManager instance = null;
	//public GameObject[] aiPrefab;
	//public GameObject[] spawnPoiints;
	public int levelNumber;

	public List<AIGameObjectInSections> aiGameObjectsInSections;

	//public bool spawnAI1, spawnAI2, spawnAI3, spawnAI4;
	public GameObject[] doorsToBeOpened; 

	public bool[] activateAISpawn;



	GameObject[] temp;

	public bool[] stageCompleted;

	void Awake()
	{
		Time.timeScale = 1.0f;
		instance = this;
		activateAISpawn = new bool[10];
		stageCompleted = new bool[10];
	}


	void Start()
	{
		temp = new GameObject[10];
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
					Debug.Log ("removing ai form index");
				}
			}
			else
			{
				Debug.Log ("Limit reached");
				// TODO: unlock the next area collider  , call camera movement

				for (int i = 0; i < aiGameObjectsInSections [limit].areaLockCollider.Length; i++)
					aiGameObjectsInSections [limit].areaLockCollider [i].SetActive (false);
				activateAISpawn [index] = false;
				stageCompleted [index] = true;
				//doorsToBeOpened [index].GetComponent<Doors> ().OpenDoor ();
				GameGlobalVariablesManager.isCameraLocked = false;
			}
		}
	}

	void Update()
	{
		//if(Input.GetKeyDown(KeyCode.Space))
		if(activateAISpawn[0])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			SpawnAI (0);
		}
		if(activateAISpawn[1])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			SpawnAI (1);
		}
		if(activateAISpawn[2])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			SpawnAI (2);
		}
		if(activateAISpawn[3])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			SpawnAI (3);
		}
		if(activateAISpawn[4])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			SpawnAI (4);
		}
		if(activateAISpawn[5])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			SpawnAI (5);
		}
		if(activateAISpawn[6])
		{
			GameGlobalVariablesManager.isCameraLocked = true;
			SpawnAI (6);
		}
	}
}


