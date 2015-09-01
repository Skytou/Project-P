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
	public float distanceToPlayer;
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
		//playerRef = GameObject.FindGameObjectWithTag("Player");
		Debug.Log (playerRef);
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
		
		playerPos = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y,0);

		Debug.Log (playerPos);
		if( (distanceToPlayer<distanceToAttack) )
		{
			Stop();
 		}
		else  
		{

			xComponent = -transform.position.x + playerPos.x;
			yComponent = -transform.position.y + playerPos.y;
			angle = Mathf.Atan2(yComponent, xComponent) * Mathf.Rad2Deg;
			CalculateAngle(angle);
			this.transform.position = Vector2.MoveTowards(this.transform.position,playerRef.transform.position,aiMoveSpeed * Time.deltaTime);

			isInMove = true;
			aiAnimator.SetBool("isInMove",isInMove);
			aiAnimator.SetFloat("idleDirection",idleDirection);
			aiAnimator.SetFloat("moveDirection",moveDirection);
		}
		  

		if(transform.position ==  playerRef.transform.position)
		{ 
			idleDirection =prevMoveDirection;
			moveDirection=-1;
			
		}



	}


	void Stop()
	{
		 
		Idle ();
		 


	}


	void Idle()
	{
		//Debug.Log("Calling IDle");
		isInMove = false;
		 
		idleDirection =prevMoveDirection;
		
		aiAnimator.SetBool("isInMove",isInMove);
		 
		aiAnimator.SetFloat("idleDirection",idleDirection);
		aiAnimator.SetFloat("moveDirection",moveDirection);

		//if (isInPlayerRadius)
		{
			if (a_timer <= 0f) 
			{
				// call Attack()

				Attack ();
				a_timer = aiAttackTime;
			}
			a_timer -= Time.deltaTime;
		} 

		 
	  
	}

	void Attack()
	{
		//Debug.Log("Calling AttackAnimation");
		aiAnimator.SetFloat("idleDirection",idleDirection);
		aiAnimator.SetFloat("moveDirection",moveDirection);
		int r = Random.Range(1,5);
		//Debug.Log("Random value "+ 4);
		aiAnimator.SetInteger("AttackRandom",1);
		aiAnimator.SetTrigger("Attack");
	}


	void HitPlayer()
	{
		Debug.Log ("Triggering player react");

		playerRef.GetComponent<PlayerMovement>().React ();
	}
	 

	//playerRef.transform.position + Random.insideUnitCircle

	void OnCollisionMove()
	{
		Debug.Log ("On collision move");

		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2( playerRef.transform.position.x + Random.insideUnitCircle.x *5, playerRef.transform.position.y *5 + Random.insideUnitCircle.y) ,aiMoveSpeed * Time.deltaTime);
	}

	public void React()
	{
		if (hitsTaken >= aiMaxHitTaken-1)
		{ 	
			Debug.Log("Calling Dead state");
			healthBar.SetActive(false);
			hitsTaken++;
			Destroy (this.gameObject);
 
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
			//isInPlayerRadius = true;
			//Debug.Log ("Player in Radius");
			break;

		case "AI":
			//if (other.GetComponent<AIComponent> ().isInPlayerRadius == true )
			{
				//isAIOverLapped = true;
				 
			}
			break;
		case "Ground":

			//other.gameObject.GetComponent<PolygonCollider2D> ().enabled = false;

			break;

		/*else
			{
				isAIOverLapped = false;
			}*/

		 
		}
	//	Debug.Log(layerName);
		//characterInQuicksand = true;
	}

	/*void OnTriggerStay2D(Collider2D other) 
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);

		Debug.Log (layerName);
		switch(layerName)
		{ 	

		case "Player":
			isInPlayerRadius = true;

			 
			break;
			case "AI":
		 	if (other.GetComponent<AIComponent> ().isInPlayerRadius == true )
			{
				isAIOverLapped = true;
			}
				
		 
		 	 
		 
				break;
		 
		}
	}
*/
	void OnTriggerExit2D(Collider2D other) 
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);
		
		/*switch(layerName)
		{ 	

		case "Player":
			isInPlayerRadius = false;
			isAIOverLapped = false;
		//	Debug.Log ("resetting player in radius");

			break;
		case "AI":
			 
			isAIOverLapped = false;
			 

			break;
	 

			Debug.Log (layerName);
		}*/
	}
 
	void CallAnimation()
	{
		
	}

	void AIDead()
	{

	}

	 
	 
	// Update is called once per frame
	void Update () 
	{
		distanceToPlayer = Vector2.Distance(this.transform.position, playerRef.transform.position);

		//Debug.Log ("Distance to player " + distanceToPlayer);
		/*if(  (distanceToPlayer>distanceToAttack)  && !isInPlayerRadius)
			{
				MoveTowardsPlayer();
			Debug.Log ("Update");
			aiAnimator.SetBool("isInMove",isInMove);
			aiAnimator.SetFloat("idleDirection",idleDirection);
			aiAnimator.SetFloat("moveDirection",moveDirection);
			}*/
		 
		//CastRay ();
		 

		//if((distanceToPlayer>distanceToAttack)  )
		{
			MoveTowardsPlayer ();
		}
		/*else
		{
			Idle ();
		}*/
		/*switch(aiBehaviour)
		{
			case AIBehaviour.ATTACK:
				MoveTowardsPlayer();
			break;

		case AIBehaviour.IDLE:
			Idle();
			break;
		}*/

		if(Input.GetMouseButtonDown(1))
		{
			React();
		}
	}
}
