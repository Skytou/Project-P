using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour 
{

	public float moveSpeed;
	private Vector2 tempPos;
	private bool canThrow;
	// Use this for initialization
	void Start () 
	{
	
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
				Destroy (this.gameObject);
		}
	}
}
