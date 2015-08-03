using UnityEngine;
using System.Collections;
//using Prime31;

public class PlayerMovement : MonoBehaviour
{

	public float speed =1.5f;
	private Vector3 target;
	private Animator characterAnimator;
	 
	private AnimatorStateInfo animatorState;
 
 
	private Vector3 mouseClickPos;

	int moveDirection;
	
	float xComponent;
	float yComponent;
	float angle;

	string layerName;

	void Awake()
	{
		target = transform.position;
		characterAnimator = GetComponent<Animator>();
	}

	void Start()
	{

	}
 

	void CalculateAngle(float angle, Vector3 traget)
	{
		if(angle>0)
		{
			if(angle > 75 && angle <= 105)
			{
				// up 
				moveDirection =1;

			}
			else if(angle > 15 && angle <= 75)
			{
				// up right
				moveDirection =2;

			}
			else if(angle >= 0 && angle <= 15)
			{
				// right
				moveDirection =3;
			}
			else if(angle > 105 && angle <= 165)
			{
				// up left
				moveDirection =8;
				 
			}
			else
			{
				// left
				moveDirection =7;
				 
			}
		}
		
		// Mapping angle to 8 directions 0 - -180
		else
		{
			if(angle < -75 && angle >= -105)
			{
				// down
				moveDirection =5;
				 
			}
			else if(angle < -15 && angle >= -75)
			{
				//down right
				moveDirection =4;
			 
			}
			else if(angle <= 0 && angle >= -15)
			{
				// right
				moveDirection =3;
				 
			}
			else if(angle < -105 && angle >= -165)
			{
				//down left
				moveDirection =6;
				 
			}
			else
			{
				// left 
				moveDirection =7;
				 
			}
		}

		characterAnimator.SetInteger("moveDirection",moveDirection);

		//transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}

	 
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.name);
		//characterInQuicksand = true;
	}

	void Update()
	{

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

	  		 
		}


		CalculateAngle(angle , target);
	
		//CallAnimation();
	}    

	void FixedUpdate()
	{

	}

	void LateUpdate()
	{

	}
	
}
