using UnityEngine;
using System.Collections;

//static class which contains all game global variables

public class GameGlobalVariablesManager : MonoBehaviour 
{

	public static int[] castleLocked = new int[4];// leve2Locked , level3Locked, level4Locked;
    public static int LevelsCleared = 1;

	public static bool isCameraLocked = false;

	public static float playerHealth = 0;

	public static bool isPlayerSpin = false;

	public static bool isKnifeThrow = false;

	public static bool isSoundMuted = false;

	public static bool isBombActivated =false;

	public static bool isFireBallThrown;

	public static int totalNumberOfCoins = 1000;
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
    public static string TutorialScene = "TutorialScene";
    public static string Credits = "Credits";
    
    public static string SceneHorse1 = "horse1";
    public static string SceneHorse2 = "horse2";
    public static string SceneHorse3 = "horse3";

    public static string SceneCastle1 = "castle1";
    public static string SceneCastle2 = "castle2";
    public static string SceneCastle3 = "castle3";
    public static string SceneCastle4 = "castle4";

    public static int TotalEnergy = 3;
    public static int TotalKnife = 10;
    public static int TotalBombs = 5;
    public static int TotalCyclone = 5;

    public static int EnergyAvailable = 3;
    public static int KnifeCount = 5;
    public static int BombsCount = 3;
    public static int CycloneCount = 3;

    public static int StartEnergyAvailable = 3;
    public static int StartKnifeCount = 5;
    public static int StartBombsCount = 3;
    public static int StartCycloneCount = 3;
    public static int StartLevelsCleared = 1;

    public static int PlayerLevel = 1;
    public static int SwordLevel = 1;
    public static int KnifeLevel = 1;
    public static int BombLevel = 1;
    public static int CycloneLevel = 1;


    public static int Enemy1_Drop = 3;
    public static int Enemy2_Drop = 6;
    public static int Enemy3_Drop = 10;

    public static int GameStartCoins = 500;
    public static int DailyBonusCoins = 250;

    // to take a build with out ads
    public static bool IsShowAd = true;

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
