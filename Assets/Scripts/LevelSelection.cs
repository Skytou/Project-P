using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelection : MonoBehaviour
{
	public GameObject[] castle;


	// Use this for initialization
	void Start () 
	{
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
		Application.LoadLevel (3);
	}

	public void Level2()
	{
		Application.LoadLevel (4);
	}

	public void Level3()
	{
		
	}

	public void Level4()
	{
		
	}



	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
