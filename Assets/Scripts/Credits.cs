using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	void Start () {
	
	}

	
	void Update () {
	
	}


    public void OnBack()
    {
        Application.LoadLevel(GameGlobalVariablesManager.MainMenu);
    }


}
