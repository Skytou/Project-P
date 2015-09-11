using UnityEngine;

using System.Collections;

public enum ThrowObjectType
{
	Bomb,
	Spear
}

public class ThrowableObject : MonoBehaviour 
{

	public ThrowableObject throwableObject;
	public float moveSpeed;
	private Vector2 tempPos;
	private bool canThrow;
	public GameObject explode;
	public SpriteRenderer ballrenderer;
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
			this.gameObject.transform.position = Vector2.MoveTowards (this.transform.position, new Vector2(tempPos.x,tempPos.y+7), moveSpeed * Time.deltaTime);

			if (this.transform.position.x == tempPos.x)
			{

				ballrenderer.enabled = false;
				explode.SetActive (true);
				Destroy (this.gameObject,0.5f);
			}
				
		}
	}
}
