using UnityEngine;
using System.Collections;

//static class which contains all game global variables

public class GameGlobalVariablesManager : MonoBehaviour 
{

	public static int[] castleLocked = new int[4];// leve2Locked , level3Locked, level4Locked;

	public static bool isCameraLocked = false;


	public static bool isPlayerSpin = false;

	public static bool isKnifeThrow = false;


	public static bool isFireBallThrown;


	public static int currentLevelnumber;
	public static bool level1Completed, level2Completed, level3Completed, level4Completed;

	public static bool isSwordSelected, isKnifeSelected;
	public static bool isTimerSelected, isBombSelected, isCycloneSelected;
 
	public static float numberOfKnives = 100;

	public static bool isFreezeTimerOn;

	void Awake()
	{
		for(int i =0;i<castleLocked.Length;i++)
		{
			castleLocked[i] = 1;
		}
	}


	void Update()
	{
		 
	}
}
