using UnityEngine;
using System.Collections;

//static class which contains all game global variables

public class GameGlobalVariablesManager : MonoBehaviour 
{

	public static int[] castleLocked = new int[4];// leve2Locked , level3Locked, level4Locked;

	public static bool isCameraLocked = false;

	public static float playerHealth;

	public static bool isPlayerSpin = false;

	public static bool isKnifeThrow = false;

	public static bool isSoundMuted = false;

	public static bool isBombActivated =false;

	public static bool isFireBallThrown;

	public static int totalNumberOfCoins;
	public static float stunTime =10;

	public static int currentLevelnumber;
	public static bool level1Completed, level2Completed, level3Completed, level4Completed;

	public static bool isSwordSelected, isKnifeSelected;
	public static bool isTimerSelected, isCycloneSelected;
 
	public static float numberOfKnives = 100;

	public static bool isFreezeTimerOn;

    public static string StoreScene = "StoreScene";
    public static string MainMenu = "MainMenu";
    public static string LevelSelection = "LevelSelection";

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
