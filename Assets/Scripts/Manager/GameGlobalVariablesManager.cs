using UnityEngine;
using System.Collections;

//static class which contains all game global variables

public class GameGlobalVariablesManager : MonoBehaviour 
{

	public static int[] castleLocked = new int[4];// leve2Locked , level3Locked, level4Locked;

	public static bool isCameraLocked = false;


	public static bool isPlayerSpin = false;

	public static bool isKnifeThrow = false;


	public static bool isFireBallThrown = false;

	public float fireBallTimer;

	float fTimer;

	void Awake()
	{
		for(int i =0;i<castleLocked.Length;i++)
		{
			castleLocked[i] = 1;
		}
	}


	void Update()
	{
		if(isFireBallThrown)
		{
			fTimer -= Time.deltaTime;
		}
	}
}
