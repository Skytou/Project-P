using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public enum AIThrow
{
	SPEAR,
	BOMB
	 
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

	public AIThrow aiThrow;

	public float aiMoveSpeed;
	public int aiLevel;
	public float aiHealth;
	public float aiMaxHitTaken;
	public float aiAttackTime ,aiThrowTime;
	//public float aiHealthDegrageRate;

	public GameObject playerRef;
	public GameObject healthBar;
	public float distanceToAttack , distanceToThrow;
	public bool isInPlayerRadius;
	private Animator aiAnimator;
	public AnimatorStateInfo aiAnimatorState;

	public SpriteRenderer aiSpriteRenderer;
	public GameObject enemyDeathParticleEffect;
	private Animator enemyDeathParticleAnimator;

	public GameObject selectionMarker;

	public GameObject throwStartPoint;
	public GameObject throwPrefab;

	Vector3 playerPos;

	bool isAiInRange;
	RectTransform healthBarRectTransform; 
	public float distanceToPlayer;
	string layerName;
	float a_timer,t_timer;

	float idleDirection,moveDirection , prevMoveDirection;
	
	float xComponent;
	float yComponent;
	float angle;
	public float hitsTaken;
	bool isInMove;
	public bool isDead;
	public bool isAIOverLapped;

	float healthBarScaleFactor;
	Vector3 healthBarRectT;
	GameObject spear;
	 
	public bool canStun =false;
	public float bTimer;

    CoinAnim coinAnim;

	void Awake()
	{
		//playerRef = GameObject.FindGameObjectWithTag("Player");
		Debug.Log (playerRef);
		aiAnimator = GetComponent<Animator>();
		healthBarRectTransform = healthBar.GetComponent<RectTransform>();
		healthBarScaleFactor = healthBarRectTransform.localScale.x/ aiMaxHitTaken;

		enemyDeathParticleAnimator = enemyDeathParticleEffect.GetComponent<Animator> ();
		//aiMaxHitTaken = 1;
		//Debug.Log(healthBarScaleFactor);
	}

	// Use this for initialization
	void Start () 
	{
		aiSpriteRenderer.enabled = true;
		enemyDeathParticleEffect.SetActive (false);
		selectionMarker.SetActive (false);

		isDead = false;
		bTimer = GameGlobalVariablesManager.stunTime;
        GameObject curObj = GameObject.FindGameObjectWithTag("CoinParticles") as GameObject;
        if (curObj != null)
        {
            coinAnim = curObj.GetComponent<CoinAnim>() as CoinAnim;
        }
		//aiMaxHitTaken = 2;
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
				moveDirection =1;
				prevMoveDirection=1;
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
				moveDirection =1;
				prevMoveDirection=1;
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
				moveDirection =5;
				prevMoveDirection=5;
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
				moveDirection =5;
				prevMoveDirection=5;
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

		//Debug.Log (playerPos);
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
				if(!GameGlobalVariablesManager.isBombActivated)
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
		aiAnimator.SetInteger("AttackRandom",r);
		aiAnimator.SetTrigger("Attack");
	}


	void HitPlayer()
	{
		 
		playerRef.GetComponent<PlayerMovement>().React ();
	}
	 

	 

	void OnCollisionMove()
	{
		Debug.Log ("On collision move");

		this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2( playerRef.transform.position.x + Random.insideUnitCircle.x *5, playerRef.transform.position.y *5 + Random.insideUnitCircle.y) ,aiMoveSpeed * Time.deltaTime);
	}

	public void React()
	{
		Debug.Log ("Calling, react");
		if (hitsTaken >= aiMaxHitTaken-1)
		{ 	
			//Debug.Log("Calling Dead state");
			healthBar.SetActive(false);
			hitsTaken++;
			Death ();
			return;
		}
		else
		{
			hitsTaken++;

			healthBarRectT = healthBarRectTransform.localScale;
			healthBarRectT.x = healthBarRectT.x -healthBarScaleFactor;
			healthBarRectTransform.localScale =healthBarRectT;

			//int r = Random.Range(1,5);
			//Debug.Log("Random value "+ 4);
			aiAnimator.SetFloat("idleDirection",idleDirection);
			aiAnimator.SetFloat("moveDirection",moveDirection);
			aiAnimator.SetInteger("ReactRandom",1);
			aiAnimator.SetTrigger("React");
			return;
		 	//Debug.Log(hitsTaken);
		}

	}


	public void Death()
	{
        if (!aiAnimatorState.IsTag(("DeathTag")) && !isDead)
		{
			isDead = true;
			Debug.Log ("Triggeringdeath");

			selectionMarker.SetActive (false);
			aiAnimator.SetFloat("idleDirection",idleDirection);
			aiAnimator.SetFloat("moveDirection",moveDirection);
			int r = Random.Range(1,3);
			aiAnimator.SetTrigger("Death");
			aiAnimator.SetInteger("DeathRandom",r);

            if (coinAnim != null)
                coinAnim.PlayCoinAnim(transform.position + new Vector3(0,4,0));
            GameGlobalVariablesManager.totalNumberOfCoins += 10;
            AudioMgr.Inst.PlaySfx(SfxVals.EnemyDeath);
        
            if (playerRef != null)
                playerRef.GetComponent<PlayerMovement>().AttackToIdleState();
        }
	}

 	 
	public void DestroyEnemy()
	{ 
		enemyDeathParticleEffect.SetActive (true);
		aiSpriteRenderer.enabled = false;
		Destroy (this.gameObject, 0.5f);
	} 
 

	void ThrowSpears()
	{
		if(!isDead)
		{
			aiAnimator.SetTrigger("Throw");
			//if(aiAnimator.)
			//LaunchSpear ();
			spear = Instantiate (throwPrefab, throwStartPoint.transform.position, Quaternion.identity) as GameObject;
			spear.SetActive (false);
		}
	}


	public void LaunchSpear()
	{
		Debug.Log ("throwing spear");

		spear.SetActive (true);
		spear.GetComponent<ThrowableObject> ().ThowObjectTo (playerRef.transform.position, true);
	}


	void MoveTowardsPlayerToThrow()
	{
		playerPos = new Vector3(playerRef.transform.position.x, playerRef.transform.position.y,0);

		//Debug.Log (playerPos);
		if( (distanceToPlayer<distanceToThrow) )
		{
			StopAndThrow();
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

	void StopAndThrow()
	{
		//Debug.Log("Calling IDle");
		isInMove = false;

		idleDirection =prevMoveDirection;

		aiAnimator.SetBool("isInMove",isInMove);

		aiAnimator.SetFloat("idleDirection",idleDirection);
		aiAnimator.SetFloat("moveDirection",moveDirection);

		if(aiThrow == AIThrow.SPEAR)
		//if (isInPlayerRadius)
		{
			if (t_timer <= 0f) 
			{
				// call Attack()
				if(!GameGlobalVariablesManager.isBombActivated) 
				ThrowSpears ();
				t_timer = aiThrowTime;
			}
			t_timer -= Time.deltaTime;
		} 
		else
		{
			if(GameGlobalVariablesManager.isFireBallThrown==false)
			{
				if (t_timer <= 0f) 
				{
					// call Attack()

					ThrowSpears ();
					t_timer = aiThrowTime;
				}
				t_timer -= Time.deltaTime;
			}

		}
	}
	 
	void Stun()
	{
		if (bTimer >= 0f) 
		{  
			isInMove = false;

			idleDirection =prevMoveDirection;

			aiAnimator.SetBool("isInMove",isInMove);

			aiAnimator.SetFloat("idleDirection",idleDirection);
			aiAnimator.SetFloat("moveDirection",moveDirection);


			 

		}

		else
		{
			Debug.Log ("time over");
			GameGlobalVariablesManager.isBombActivated = false;
			canStun = false;
			bTimer = GameGlobalVariablesManager.stunTime;
		 

		}

	}

	// Update is called once per frame
	void Update () 
	{
		distanceToPlayer = Vector2.Distance(this.transform.position, playerRef.transform.position);
		 
		aiAnimatorState =  aiAnimator.GetCurrentAnimatorStateInfo (0);
		//if((distanceToPlayer>distanceToAttack)  )
		switch(aiBehaviour)
		{
		case AIBehaviour.ATTACK:
			if(  (!GameGlobalVariablesManager.isBombActivated)&& !aiAnimatorState.IsTag (("DeathTag")))// && !GameGlobalVariablesManager.isKnifeThrow )
			MoveTowardsPlayer ();
			break;


		case AIBehaviour.RANGED:
			if(  (!GameGlobalVariablesManager.isBombActivated)&& !aiAnimatorState.IsTag (("DeathTag") ))//&& !GameGlobalVariablesManager.isKnifeThrow )
			MoveTowardsPlayerToThrow ();
			break;
		}
		 
		if (GameGlobalVariablesManager.isBombActivated) 
		{
			canStun = true;
			bTimer -= Time.deltaTime;
			Stun ();

		}
/*
		if(Input.GetMouseButtonDown(1))
		{
			React();
		}*/
	}
}
