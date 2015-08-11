using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AIProperties
{

	 
}

public enum AIBehaviour
{
	IDLE,
	ATTACK,
	RANGED
}

public class AIComponent : MonoBehaviour 
{
	public AIBehaviour aiBehaviour;

	public float aiMoveSpeed;
	public int aiLevel;
	public float aiHealth;
	public float aiMaxHitTaken;
	public float aiAttackTime;
	//public float aiHealthDegrageRate;

	public GameObject playerRef;
	public GameObject healthBar;
	public float distanceToAttack;
	public bool isInPlayerRadius;
	private Animator aiAnimator;
	private AnimatorStateInfo aiAnimatorState;

	Vector3 playerPos;

	bool isAiInRange;
	RectTransform healthBarRectTransform; 
	float distanceToPlayer;
	string layerName;
	float a_timer;

	float idleDirection,moveDirection , prevMoveDirection;
	
	float xComponent;
	float yComponent;
	float angle;
	float hitsTaken;
	bool isInMove;

	public bool isAIOverLapped;

	float healthBarScaleFactor;
	Vector3 healthBarRectT;

 
	void Awake()
	{
		playerRef = GameObject.FindGameObjectWithTag("Player");
		aiAnimator = GetComponent<Animator>();
		healthBarRectTransform = healthBar.GetComponent<RectTransform>();
		healthBarScaleFactor = healthBarRectTransform.localScale.x/ aiMaxHitTaken;
		//Debug.Log(healthBarScaleFactor);
	}

	// Use this for initialization
	void Start () 
	{
		 
	}

	 

	void AttackPlayer(int id)
	{
		
	}

	void AIReact()
	{

	}
	//Calculate the angle and return the movedirection

	void CalculateAngle(float angle)
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
	 
		//return moveDirection;
	}

	void MoveTowardsPlayer()
	{
		distanceToPlayer = Vector2.Distance(this.transform.position, playerRef.transform.position);
		
		playerPos = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y,0);

		if( (distanceToPlayer<distanceToAttack))// || isAIOverLapped )
		{
			Stop();


		//	Debug.Log("Calling stop");
			// stop
			// call hit animation for an interval

		}
		else  
		{

			xComponent = -transform.position.x + playerPos.x;
			yComponent = -transform.position.y + playerPos.y;
			angle = Mathf.Atan2(yComponent, xComponent) * Mathf.Rad2Deg;
			CalculateAngle(angle);
			this.transform.position = Vector2.MoveTowards(this.transform.position,playerRef.transform.position,aiMoveSpeed * Time.deltaTime);

			isInMove = true;
		}

		// Physics2D.

		if(transform.position ==  playerRef.transform.position)
		{
			//isInMove = false;
			//idleDirection = moveDirection;
			idleDirection =prevMoveDirection;
			moveDirection=-1;
			
		}

		aiAnimator.SetBool("isInMove",isInMove);
		aiAnimator.SetFloat("idleDirection",idleDirection);
		aiAnimator.SetFloat("moveDirection",moveDirection);

	}


	void Stop()
	{
		// call idle animation
		// call stop animation
		Idle ();
		//aiBehaviour = AIBehaviour.IDLE;


	}


	void Idle()
	{
		Debug.Log("Calling IDle");
		isInMove = false;
		//isRun = false;
		//aiBehaviour = AIBehaviour.IDLE;
		idleDirection =prevMoveDirection;
		
		aiAnimator.SetBool("isInMove",isInMove);
		//aiAnimator.SetBool("isRun",isRun);
		aiAnimator.SetFloat("idleDirection",idleDirection);
		aiAnimator.SetFloat("moveDirection",moveDirection);

		if(isInPlayerRadius && !isAIOverLapped)
		{
			if(a_timer <=0f)
			{
				// call Attack()

				Attack();
				a_timer = aiAttackTime;
			}
			a_timer -= Time.deltaTime;
		}
		else if(isAIOverLapped &&!isInPlayerRadius)
		{
			Debug.Log("Calling IDle");
			isInMove = false;
			//isRun = false;
			//aiBehaviour = AIBehaviour.IDLE;
			idleDirection =prevMoveDirection;

			aiAnimator.SetBool("isInMove",isInMove);
			//aiAnimator.SetBool("isRun",isRun);
			aiAnimator.SetFloat("idleDirection",idleDirection);
			aiAnimator.SetFloat("moveDirection",moveDirection);

		}
	}

	void Attack()
	{
		Debug.Log("Calling AttackAnimation");
		aiAnimator.SetFloat("idleDirection",idleDirection);
		aiAnimator.SetFloat("moveDirection",moveDirection);
		int r = Random.Range(1,5);
		//Debug.Log("Random value "+ 4);
		aiAnimator.SetInteger("AttackRandom",r);
		aiAnimator.SetTrigger("Attack");
	}


	public void React()
	{
		if (hitsTaken >= aiMaxHitTaken-1)
		{ 	
			Debug.Log("Calling Dead state");
			healthBar.SetActive(false);
			hitsTaken++;
 
		}
		else
		{
			hitsTaken++;

			healthBarRectT = healthBarRectTransform.localScale;
			healthBarRectT.x = healthBarRectT.x -healthBarScaleFactor;
			healthBarRectTransform.localScale =healthBarRectT;
			//Debug.Log(hitsTaken);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);

		switch(layerName)
		{
		case "Player":
			isInPlayerRadius = true;
			Debug.Log ("Player in Radius");
			break;

		case "AI":

			//Idle();
			//Debug.Log("Ai In AI range");
			break;
		default:
			 
			break;
		}
		//Debug.Log(layerName);
		//characterInQuicksand = true;
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);
		
		switch(layerName)
		{ 	
			case "AI":
			isAIOverLapped = true;
			
			 
			//Debug.Log("Ai In AI range");
				break;
			default:
				
				break;
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);
		
		switch(layerName)
		{ 	
		case "AI":
			isAIOverLapped = false;
			
			
			//Debug.Log("Ai In AI range");
			break;
		default:
			
			break;
		}
	}

	void CallAnimation()
	{
		
	}

	void AIDead()
	{

	}

	void CastRay()
	{

	}

	void RevokeOverLap()
	{
		isAIOverLapped = false;
		Physics.IgnoreLayerCollision (LayerMask.NameToLayer ("AI"), LayerMask.NameToLayer ("AI"));

	}
	// Update is called once per frame
	void Update () 
	{
		/*if(!isAIOverLapped)
		{
			Debug.Log ("Invokeing");
			Invoke ("RevokeOverLap", 3);
		}*/

		switch(aiBehaviour)
		{
			case AIBehaviour.ATTACK:
				MoveTowardsPlayer();
			break;

		case AIBehaviour.IDLE:
			Idle();
			break;
		}

		if(Input.GetMouseButtonDown(1))
		{
			React();
		}
	}
}
