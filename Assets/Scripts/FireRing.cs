using UnityEngine;
using System.Collections;

public class FireRing : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameGlobalVariablesManager.isFireBallThrown ==false)
		{
			Destroy ((this.gameObject));
		}
	}
}
