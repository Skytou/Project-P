﻿using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
	public GameObject[] castle;

	bool isTouched;

	public RectTransform levelBG;
	public float dragSpeed;

	public GameObject loadingScreen;
	//public Slider loadingSlider;

	public Vector2 bgRectTransform;

	//bool isShowProgressBar = false;

	// Use this for initialization
	void Start () 
	{
		//isTouched = false;
		loadingScreen.SetActive ((false));
		for(int i =1;i<castle.Length;i++)
		{
			if(GameGlobalVariablesManager.castleLocked[i]==1)
			{
				Color c = castle [i].GetComponent<Image> ().color;
				c.a = 1;
				castle [i].GetComponent<Image> ().color = c;
				//castle [i].GetComponent<Image> ().color.a = new float (255);// 255;
			}
			else
			{
				Color c = castle [i].GetComponent<Image> ().color;
				c.a = 0.5f;
				castle [i].GetComponent<Image> ().color = c;
				//castle [i].GetComponent<Image> ().color.a = 150;
			}
		}
	
	}


	public void Level1()
	{
		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/
		loadingScreen.SetActive (true);
		StartCoroutine (LoadLevel1());
		//LoadLevel1 ();
		//LoadSceneManager.instance.LoadSceneWithTransistion (3,SceneTransition.FishEyeToScene);
		//Application.LoadLevel (3);
	}

	IEnumerator LoadLevel1()
	{
		

		AsyncOperation async = Application.LoadLevelAsync(3);
		yield return async;
		Debug.Log("Loading complete");
	}

	public void Level2()
	{
		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/
		LoadSceneManager.instance.LoadSceneWithTransistion (4,SceneTransition.FishEyeToScene);
		//Application.LoadLevel (4);
	}

	public void Level3()
	{
		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/


		LoadSceneManager.instance.LoadSceneWithTransistion (5,SceneTransition.FishEyeToScene);
		//Application.LoadLevel (5);
	}

	public void Level4()
	{
		/*if(Advertisement.IsReady())
		{
			Advertisement.Show ();
			Debug.Log ("Showing ad");
		}*/
		LoadSceneManager.instance.LoadSceneWithTransistion (6,SceneTransition.FishEyeToScene);
	}


	public void OnPointerDown( BaseEventData data)
	{
		var pointData = (PointerEventData)data;
		isTouched = true;
	}


	public void OnPointerDrag(  BaseEventData data)
	{
		var pointData = (PointerEventData)data;
		if(isTouched)
		{
			if(pointData.delta.x >0)
			{
				
				bgRectTransform.x+= dragSpeed;
				//bgRectTransform.x = Mathf.Clamp (bgRectTransform.x, -1463, 1521);
				levelBG.anchoredPosition = bgRectTransform;
			}
			else if(pointData.delta.x < 0)
			{
				//bgRectTransform = levelBG.anchoredPosition;
				bgRectTransform.x-= dragSpeed;
				//bgRectTransform.x = Mathf.Clamp (bgRectTransform.x, -1463, 1521);
				levelBG.anchoredPosition = bgRectTransform;
			}
			//Debug.Log (pointData.delta);
		}
	}

	public void OnPointerUp( BaseEventData data)
	{
		var pointData = (PointerEventData)data;
		isTouched = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		bgRectTransform = levelBG.anchoredPosition;
		bgRectTransform.x = Mathf.Clamp (bgRectTransform.x, -1463, 1471);
		
		//levelBG.anchoredPosition = new Vector2( Mathf.Clamp(levelBG.anchoredPosition.x,20f,-2400f) ,levelBG.anchoredPosition.y);

		/*if(isShowProgressBar)
		{
			if(loadingSlider.value<=loadingSlider.maxValue)
			loadingSlider.value += Time.deltaTime;

		}*/
	}
	
}
