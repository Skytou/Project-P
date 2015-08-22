using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HorseLevelManager : MonoBehaviour 
{
	public GameObject bridge;

	SpriteRenderer bridgeSprite;
	public GameObject horseRef;

	GameObject newBridge;

	Vector3 initialPos;

	public float pieceNum , pieceLength =7;

	// Use this for initialization
	void Start () 
    {

		bridgeSprite = bridge.GetComponent<SpriteRenderer> ();
		initialPos = new Vector3 (4, 0, 15);
		newBridge = TrashMan.spawn (bridge, initialPos, Quaternion.identity);
		/*var recycleBin = new TrashManRecycleBin()
		{
			prefab = bridge
				// any other options can be placed here
		};*/
		Debug.Log (bridgeSprite.bounds.extents.x);
	}

	void CreateBridge()
	{
		//var segmentPos = initialPos + Vector3.forward * (pieceNum * pieceLength);
		var n = TrashMan.spawn (bridge, new Vector3 ( (newBridge.transform.position.x + ( bridgeSprite.bounds.extents.x *2) ),initialPos.y,initialPos.z) , Quaternion.identity);
		pieceNum++;
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Debug.Log (horseRef.transform.position);
		/*if(horseRef.transform.position.x >-3f)
		{
			TrashMan.despawnAfterDelay (newBridge, 1f);
		}*/
		if(Input.GetMouseButtonDown(1))
		{
			CreateBridge ();
			//TrashMan.despawnAfterDelay (newBridge, 1f);
		}
	}
}
