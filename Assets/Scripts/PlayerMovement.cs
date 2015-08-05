using UnityEngine;
using System.Collections;
//using Prime31;

public class PlayerMovement : MonoBehaviour
{

	public float speed =1.5f;
	private Vector3 target;
	private Animator characterAnimator;
	 
	private AnimatorStateInfo animatorStateInfo;
 
 
	private Vector3 mouseClickPos;

	float idleDirection,moveDirection , prevMoveDirection;
	
	float xComponent;
	float yComponent;
	float angle;

	string layerName;

	bool isInMove;

	void Awake()
	{
		target = transform.position;
		characterAnimator = GetComponent<Animator>();
	}

	void Start()
	{
		prevMoveDirection =5;
		moveDirection=-1;
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);
	}
 

	void CalculateAngle(float angle, Vector3 traget)
	{
		if(angle>0)
		{
			if(angle > 75 && angle <= 105)
			{
				// up 
				moveDirection =1;
				prevMoveDirection =1;
				idleDirection =-1;

			}
			else if(angle > 15 && angle <= 75)
			{
				// up right
				moveDirection =2;
				prevMoveDirection=2;
				idleDirection =-1;

			}
			else if(angle >= 0 && angle <= 15)
			{
				// right
				moveDirection =3;
				prevMoveDirection=3;
				idleDirection =-1;
			}
			else if(angle > 105 && angle <= 165)
			{
				// up left
				moveDirection =8;
				prevMoveDirection=8;
				idleDirection =-1;
				 
			}
			else //if(angle>165 && angle<=180)
			{
				// left
				moveDirection =7;
				prevMoveDirection=7;
				idleDirection =-1;
				 
			}
			 
		}
		
		// Mapping angle to 8 directions 0 - -180
		else
		{
			if(angle < -75 && angle >= -105)
			{
				// down
				moveDirection =5;
				prevMoveDirection=5;
				idleDirection =-1;
				 
			}
			else if(angle < -15 && angle >= -75)
			{
				//down right
				moveDirection =4;
				prevMoveDirection=4;
				idleDirection =-1;
			 
			}
			else if(angle <= 0 && angle >= -15)
			{
				// right
				moveDirection =3;
				prevMoveDirection=3;
				idleDirection =-1;
				 
			}
			else if(angle < -105 && angle >= -165)
			{
				//down left
				moveDirection =6;
				prevMoveDirection=6;
				idleDirection =-1;
				 
			}
			else //if(angle<-165 && angle>=-180)
			{
				// left 
				moveDirection =7;
				prevMoveDirection=7;
				idleDirection =-1;
				 
			}

		}
 
		//transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if(transform.position ==  target)
		{
			  
			idleDirection =prevMoveDirection;
			moveDirection=-1;


		}
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);

	}

	 
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.name);
		//characterInQuicksand = true;
	}

	void Update()
	{
		animatorStateInfo = characterAnimator.GetCurrentAnimatorStateInfo (0);

		//Movement();
		if (Input.GetMouseButtonDown(0)) 
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = transform.position.z;
			mouseClickPos = Input.mousePosition;
		 
			xComponent = -transform.position.x + target.x;
			yComponent = -transform.position.y + target.y;

			angle = Mathf.Atan2(yComponent, xComponent) * Mathf.Rad2Deg;

			//Debug.Log("Angle " + angle);
			isInMove = true;
	  		 
		}


		//if(isInMove)
		CalculateAngle(angle , target);
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

	
		//CallAnimation();
	}    

	void FixedUpdate()
	{

	}

	void LateUpdate()
	{

	}
	
}
