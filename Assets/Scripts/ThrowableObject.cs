using UnityEngine;

using System.Collections;

public enum ThrowObjectType
{
	BOMB,
	SPEAR
}

public class ThrowableObject : MonoBehaviour 
{

	public ThrowObjectType throwableObjectType;
	public float moveSpeed;
	private Vector2 tempPos;
	private bool canThrow;
	public GameObject explode;
	public SpriteRenderer ballrenderer;

	public GameObject fireRingPrefab;
	// Use this for initialization
	void Start () 
	{
		ballrenderer = this.gameObject.GetComponent<SpriteRenderer> ();
		explode.SetActive (false);
	
	}


	 

	public void  ThowObjectTo(Vector2 throwDestinationPosition,bool canMove)
	{
		if (canMove)
		{
			tempPos = throwDestinationPosition;
			canThrow = canMove;
		}
	}

	// Update is called once per frame
	void Update ()
	{
	  
		if(canThrow)
		{
			this.gameObject.transform.position = Vector2.MoveTowards (this.transform.position, new Vector2(tempPos.x,tempPos.y), moveSpeed * Time.deltaTime);

			if(throwableObjectType == ThrowObjectType.BOMB)
			{
				if (this.transform.position.x == tempPos.x)
				{

					ballrenderer.enabled = false;
					explode.SetActive (true);
					GameGlobalVariablesManager.isFireBallThrown = true;
				//	Debug.Log ("Enabling fire ring");
					Destroy (this.gameObject,0.5f);

				}
			}

			else
			{

				if (this.transform.position.x == tempPos.x)
				{

					ballrenderer.enabled = false;
					explode.SetActive (true);
					Destroy (this.gameObject,0.5f);
				}
			}

				
		}
	}
}
