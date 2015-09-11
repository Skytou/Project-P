using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	void Awake()
	{
		DontDestroyOnLoad( gameObject ); // TODO Uncomment once Game Manager is enabled
		instance = this;
		
	}
	// Use this for initialization
	void Start () 
	{
	
	}

	/*void OnGUI()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android )
		{
			// bigger buttons for higher res mobile devices
			if( Screen.width >= 1500 || Screen.height >= 1500 )
				GUI.skin.button.fixedHeight = 60;
		}
		if( GUILayout.Button( "Test" ) )
		{
			LoadSceneManager.instance.LoadSceneWithTransistion(2,SceneTransition.FishEyeToScene);
		}
	}*/
	
	 
}
