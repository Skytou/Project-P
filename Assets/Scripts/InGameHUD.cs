using UnityEngine;
using System.Collections;
using System; 

public class InGameHUD : MonoBehaviour
{

	public GameObject pauseScreen,gameHUD;



	 

	// Use this for initialization
	void Start ()
	{
		pauseScreen.SetActive (false);
		gameHUD.SetActive (true);
	
	}

//	public void 


	public void Knife()
	{
		
	}

	public void Timer()
	{
		 
	}

	public void Bomb()
	{
		 
	}

	public void Cyclone()
	{
		
		if (!GameGlobalVariablesManager.isPlayerSpin)
		{
			//Debug.Log ("cyclone");
			GameGlobalVariablesManager.isPlayerSpin = true;
		}
			
	}

	public void Pause()
	{

		//LoadSceneManager.instance.LoadSameSceneWithTransistion (SceneTransition.RipplesToSameScene);
		pauseScreen.SetActive (true);
		gameHUD.SetActive (false);
		Time.timeScale = 0f;
	}

	public void Resume()
	{
		var values = Enum.GetValues (typeof(SceneTransistionSelf));
	
		//SceneTransistionSelf s = (SceneTransistionSelf)values ( Random.Range (0, values.Length));
		LoadSceneManager.instance.LoadSameSceneWithTransistion (SceneTransistionSelf.RipplesToSameScene );//  SceneTransistionSelf.RipplesToSameScene);
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
		Application.LoadLevel ("LevelSelection");
	}

	 
}
