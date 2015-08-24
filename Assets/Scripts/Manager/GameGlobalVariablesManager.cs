using UnityEngine;
using System.Collections;

//static class which contains all game global variables

public class GameGlobalVariablesManager : MonoBehaviour 
{

	public static int[] castleLocked = new int[4];// leve2Locked , level3Locked, level4Locked;



	void Awake()
	{
		for(int i =0;i<castleLocked.Length;i++)
		{
			castleLocked[i] = 0;
		}
	}
}
