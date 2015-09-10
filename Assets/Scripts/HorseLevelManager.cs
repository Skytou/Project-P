using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HorseLevelManager : MonoBehaviour 
{
	public HorseManager horse;
	public GameObject bridge;

	SpriteRenderer bridgeSprite;
	public GameObject horseRef;

	GameObject newBridge;

	Vector3 initialPos , nextPos;

	public GameObject newObj;
	public List<GameObject>  spawnObject;



	void Awake()
	{
		spawnObject = new List<GameObject> ();
	}
	 	// Use this for initialization
	void Start () 
    {
		// if you plan on listening to the spawn/despawn events, Start is a good time to add your listeners.
		TrashMan.recycleBinForGameObject( bridge ).onSpawnedEvent += go => Debug.Log( "spawned object: " + go );
		TrashMan.recycleBinForGameObject( bridge ).onDespawnedEvent += go => Debug.Log( "DEspawned object: " + go );
	 
		initialPos = new Vector3 (0, 0, 20);
		nextPos = new Vector3 (42.3f, -24.6f, 20);
		newBridge = TrashMan.spawn (bridge, initialPos, Quaternion.identity);

		spawnObject.Add (newBridge);
		 
		//Debug.Log (bridgeSprite.bounds.extents.x);
	}

	void SpawnBridge()
	{
		

		{
			 
			HorseManager.instance.distanceTravelled = 1;

			Debug.Log ("Spawning");
			newObj = TrashMan.spawn (bridge, initialPos+ nextPos, Quaternion.identity);
			spawnObject.Add (newObj);

				
			//TrashMan.despawnAfterDelay (bridge, 10f);

		}
		initialPos += nextPos;
	}


	void DeSpawnBridge()
	{
		if(spawnObject.Count==3)
			TrashMan.despawn (newObj);
	}
	// Update is called once per frame
	void Update ()
    {
 
		Debug.Log (HorseManager.instance.distanceTravelled);

		switch((int)HorseManager.instance.distanceTravelled%40)
		{

		case 0:
			
			SpawnBridge ();

			break;

		}
		 

		if(Input.GetMouseButtonDown(0))
		{
			SpawnBridge ();
		}

		//DeSpawnBridge ();
		 
		 
	}
}
