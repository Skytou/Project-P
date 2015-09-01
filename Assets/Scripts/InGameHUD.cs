using UnityEngine;
using System.Collections;

public class InGameHUD : MonoBehaviour
{

	public GameObject pauseScreen,gameHUD;


	// Use this for initialization
	void Start ()
	{
		pauseScreen.SetActive (false);
		gameHUD.SetActive (true);
	
	}

	public void Timer()
	{
		 
	}

	public void Bomb()
	{
		 
	}

	public void Cyclone()
	{
		 
	}

	public void Pause()
	{
		pauseScreen.SetActive (true);
		gameHUD.SetActive (false);
		Time.timeScale = 0.1f;
	}

	public void Resume()
	{
		pauseScreen.SetActive (false);
		gameHUD.SetActive (true);
		Time.timeScale = 1.0f;
	}

	public void Restart()
	{
		Time.timeScale = 1.0f;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Menu()
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (GameGlobalVariablesManager.isHUDClicked);
	}
}
