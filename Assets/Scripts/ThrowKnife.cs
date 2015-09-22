using UnityEngine;
using System.Collections;

public class ThrowKnife : MonoBehaviour 
{


	public float moveSpeed;
	private Vector2 tempPos;
	private bool canThrow;
	public GameObject knifeThrowGameObject;  

	public Vector2 reachPos;
	// Use this for initialization
	void Start () 
	{
	
	}

	public void  ThowKnifeTo(Vector2 throwDestinationPosition,GameObject throwObject, bool canMove)
	{
		if (canMove)
		{
			tempPos = throwDestinationPosition;
			canThrow = canMove;
			knifeThrowGameObject = throwObject;
			Debug.Log ((throwObject));
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		

		if (canThrow) 
		{
			reachPos = tempPos;
			this.gameObject.transform.position = Vector2.MoveTowards (this.transform.position, new Vector2 (tempPos.x, tempPos.y+5), moveSpeed * Time.deltaTime);

			if(knifeThrowGameObject!=null)   //if(throwableObjectType == ThrowObjectType.BOMB)
			{
				if (this.transform.position.x == tempPos.x) 
				{
					if (knifeThrowGameObject.GetComponent<AIComponent> () != null) 
					{
						knifeThrowGameObject.GetComponent<AIComponent> ().Death ();
						GameGlobalVariablesManager.isKnifeThrow = false;
						Destroy (this.gameObject);
					}
					else
					{
						GameGlobalVariablesManager.isKnifeThrow = false;
						Destroy (knifeThrowGameObject);
						Destroy (this.gameObject);
					}
					 


				}
			}
		}
	}
}
