using UnityEngine;
using System.Collections;
//using Prime31;

public class PlayerMovement : MonoBehaviour
{

	public float speed =1.5f;
	public Vector2 velocity;
	private Vector3 target;
	private Animator characterAnimator;
	 
	private AnimatorStateInfo animatorStateInfo;
 
 
	 
	float idleDirection,moveDirection , prevMoveDirection;
	
	float xComponent;
	float yComponent;
	float angle;

	Vector3 touchPos;
	string layerName;

	bool isInMove;
	float distance;
	bool isRun;

	Collider2D collider2D;

	bool isEnemySpotted;

//	GameObject selected

	Vector2 enemyOffset;

	void Awake()
	{
		target = transform.position;
		characterAnimator = GetComponent<Animator>();
	}

	void Start()
	{
		/*prevMoveDirection =5;
		moveDirection=-1;
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);*/
	}
 

	float CalculateAngle(float angle)
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

		return moveDirection;
 
	 
	}

	 
	void OnTriggerEnter2D(Collider2D other)
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);
		
		switch(layerName)
		{
		case "Player":

			break;
			
		case "AI":
			
			Debug.Log("Ai In AI range");
			break;
		default:
			
			break;
		}
		Debug.Log(layerName);
		//characterInQuicksand = true;
	}

	void Update()
	{
		animatorStateInfo = characterAnimator.GetCurrentAnimatorStateInfo (0);

		 
		if (Input.GetMouseButtonDown(0))
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
 
			RaycastHit2D hit   = Physics2D.Raycast(target, Vector2.zero);
			
			if(hit.collider != null ) // set layer for player to check 
			{
				target = hit.collider.gameObject.transform.position ;
				//touchPos = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y,0);
				//Debug.Log(hit.collider.transform.position);
				Debug.Log("object clicked: "+hit.collider.name);
			}
			else
			{
				//touchPos = new Vector3(target.x, target.y,0);
				target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			} 

			touchPos = new Vector3(target.x, target.y,0);
			xComponent = -transform.position.x + touchPos.x;
			yComponent = -transform.position.y + touchPos.y;
			
			angle = Mathf.Atan2(yComponent, xComponent) * Mathf.Rad2Deg;

			isInMove = true;
		}

		if(isInMove)
			CalculateAngle(angle);

		distance = Vector2.Distance(transform.position, touchPos);
		 
		//Debug.Log(distance);
		if(transform.position ==  touchPos)
		{
			isInMove = false;
			isRun = false;
			idleDirection =prevMoveDirection;
		
		}



		if(distance<=10)
		{
			isRun = false;
			speed = 5;
		}
		else
		{
			isRun = true;
			speed =10;
		}

		characterAnimator.SetBool("isInMove",isInMove);
		characterAnimator.SetBool("isRun",isRun);
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);
		transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.A))
		{
			characterAnimator.SetFloat("idleDirection",idleDirection);
			characterAnimator.SetFloat("moveDirection",moveDirection);
			int r = Random.Range(1,5);
			characterAnimator.SetInteger("AttackRandom",r);
			characterAnimator.SetTrigger("Attack");
		}
	
		//CallAnimation();
	}    

	void FixedUpdate()
	{
		//this.GetComponent<Rigidbody2D>().MovePosition(target + velocity * Time.fixedDeltaTime);
	}

	void LateUpdate()
	{

	}
	
}
