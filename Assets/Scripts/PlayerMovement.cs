using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Prime31;
using UnityEngine.EventSystems;

public enum PlayerBehaviour
{
	IDLE, 
	MOVE,
	ATTACK,
	REACT
}

public class PlayerMovement : MonoBehaviour
{

	public PlayerBehaviour playerBehaviour;


	public float speed ;
	public Vector2 velocity;
	private Vector3 target;
	private Animator characterAnimator;
	 
	private AnimatorStateInfo animatorStateInfo;
  	float idleDirection,moveDirection , prevMoveDirection;

	float initialSpeed, intialDistanceToAttack;
	float xComponent;
	float yComponent;
	float angle;

	Vector3 touchPos;
	string layerName;

	bool isInMove;
 	bool isRun;

	Collider2D collider2D;

	bool isEnemySpotted;

	//string selectedObject;
	GameObject selectedEnemy, selectedObject;

 	public float distanceToPoint , distanceToAttack;

	public float attackTime;

	public GameObject knife;

	RaycastHit2D hit ;
	RaycastHit hit3D;
	float a_timer;

	public bool throwKnifeSelected;
	public bool isKnifeThrow;
	public GameObject knifePrefab ;
	private Vector3 knifeThrowPoint;

	public iTween.EaseType easeType;
	public float interpolationScale;
	BezierCurve bezierCurve;
	 
	public List<Vector3> movementPath;


	//float tempDistanceToAttack;

	void Awake()
	{
		target = transform.position;
		characterAnimator = GetComponent<Animator>();
		bezierCurve = new BezierCurve ();
	}

	void Start()
	{
		initialSpeed =speed;
		intialDistanceToAttack = distanceToAttack;
		idleDirection =5;
		//moveDirection=-1;
		characterAnimator.SetFloat("idleDirection",idleDirection);
		//characterAnimator.SetFloat("moveDirection",moveDirection);
	}
 

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

	 
	void OnTriggerEnter2D(Collider2D other)
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);
		
		switch(layerName)
		{
		case "Player":

			break;
			
		case "AI":
			
			//Debug.Log("Ai In AI range");

			isEnemySpotted = true;
		//	Debug.Log("IsEnemySpotted" + isEnemySpotted);
			break;

		case "EnemyTrigger0":
			Debug.Log ("touched trigger " + other.gameObject.name);
			LevelManager.instance.activateAISpawn [0] = true;
			other.gameObject.SetActive (false);
			break;
		case "EnemyTrigger1":
			Debug.Log ("touched trigger " + other.gameObject.name);
			LevelManager.instance.activateAISpawn [1] = true;
			other.gameObject.SetActive (false);
			break;
		case "EnemyTrigger2":
			Debug.Log ("touched trigger " + other.gameObject.name);
			LevelManager.instance.activateAISpawn [2] = true;
			other.gameObject.SetActive (false);
			break;
		case "EnemyTrigger3":
			Debug.Log ("touched trigger " + other.gameObject.name);
			LevelManager.instance.activateAISpawn [3] = true;
			other.gameObject.SetActive (false);
			break;
		case "EnemyTrigger4":
			Debug.Log ("touched trigger " + other.gameObject.name);
			LevelManager.instance.activateAISpawn [4] = true;
			other.gameObject.SetActive (false);
			break;

		case "Door0":

			if(LevelManager.instance.stageCompleted[0])
			{
				LevelManager.instance.doorsToBeOpened [0].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door1":

			if(LevelManager.instance.stageCompleted[1])
			{
				LevelManager.instance.doorsToBeOpened [1].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door2":

			if(LevelManager.instance.stageCompleted[2])
			{
				LevelManager.instance.doorsToBeOpened [2].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door3":

			if(LevelManager.instance.stageCompleted[3])
			{
				LevelManager.instance.doorsToBeOpened [3].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door4":

			if(LevelManager.instance.stageCompleted[4])
			{
				LevelManager.instance.doorsToBeOpened [4].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door5":

			if(LevelManager.instance.stageCompleted[5])
			{
				LevelManager.instance.doorsToBeOpened [5].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door6":

			if(LevelManager.instance.stageCompleted[6])
			{
				LevelManager.instance.doorsToBeOpened [6].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		default:
			
			break;
		}
		//Debug.Log(layerName);
		//characterInQuicksand = true;
	}
	
	void OnMouseDown()
	{
		Debug.Log ("On mouse down");
	}

	void Update()
	{
		animatorStateInfo = characterAnimator.GetCurrentAnimatorStateInfo (0);
		 
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()  )
		{ 
			 
			 
			selectedEnemy = null;
			selectedObject = null;
			distanceToAttack = intialDistanceToAttack;
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			hit   = Physics2D.Raycast(target, Vector2.zero);

			//Debug.Log ("hit " + hit.collider.name);
			 
			if(hit.collider != null)
			{
				layerName =  LayerMask.LayerToName(hit.collider.gameObject.layer);

				Debug.Log (layerName);
				 
				switch(layerName)
				{
				 
				case "AI":

					selectedObject = hit.collider.gameObject;
					touchPos = selectedObject.transform.position;
					//Debug.Log ("touch pos is generated");
					playerBehaviour = PlayerBehaviour.MOVE;
					break;

				case "Objects":
					selectedObject = hit.collider.gameObject;
					touchPos = selectedObject.transform.position;
					Debug.Log ("touch pos is generated");
					playerBehaviour = PlayerBehaviour.MOVE;
					break;

				case "WallLightLayer":
					touchPos = this.transform.position;

					break;

				case "AreaLock":
					touchPos = this.transform.position;
					break;
					 
				default:

					touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					playerBehaviour = PlayerBehaviour.MOVE;
					break;
				}
				 
			}

			else
			{
				touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				playerBehaviour = PlayerBehaviour.MOVE;
			}

			 

			 
			 
		}

		if(isKnifeThrow)
		{
			knifePrefab.transform.position = Vector2.MoveTowards(knifePrefab.transform.position,knifeThrowPoint , speed * Time.deltaTime);
			foreach(var kp in movementPath)
			{
				if (kp == knifeThrowPoint)
					Debug.Log ("reached");
			}
			movementPath.Add(knifePrefab.transform.position);
			bezierCurve.Interpolate (movementPath, 1f);

		}
		 
		switch(playerBehaviour)
		{
		case PlayerBehaviour.IDLE:

			break;
		case PlayerBehaviour.MOVE:
			MoveTowardsPoint();

			break;

		case PlayerBehaviour.ATTACK:

			break;

		case PlayerBehaviour.REACT:

			break;
		}
	 
		if( throwKnifeSelected)
		ThrowKnives ();
	}

	void ThrowKnives()
	{
		if(Input.GetMouseButtonDown(1))

		{
			Vector3 sp = Camera.main.ScreenToWorldPoint(transform.position);
			Vector3 dir = (Input.mousePosition - sp).normalized;

			knifePrefab =  Instantiate (knife, this.transform.position, Quaternion.identity) as GameObject;
		
			knifeThrowPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//knifePrefab.transform.position = Vector2.MoveTowards(knifePrefab.transform.position,knifeThrowPoint , speed * Time.deltaTime);
			//isKnifeThrow = true;
			iTween.MoveTo (knifePrefab, iTween.Hash ("position", knifeThrowPoint ,"easeType",easeType));

			//rigidbody2D.AddForce (dir * amount);
		}
	}

	void MoveTowardsPoint()
	{ 
		distanceToPoint = Vector2.Distance(transform.position, touchPos);
		 

 		if(distanceToPoint<distanceToAttack)
		{
			Stop ();
			//Debug.Log ("Stopping");
		}

		else
		{
 			xComponent = -transform.position.x + touchPos.x;
			yComponent = -transform.position.y + touchPos.y;
			
			angle = Mathf.Atan2(yComponent, xComponent) * Mathf.Rad2Deg;
			transform.position = Vector2.MoveTowards(transform.position, touchPos, speed * Time.deltaTime);

			isInMove = true;
			 
			if(selectedObject==null)
			{
				isRun = true;
				distanceToAttack =1;
				speed = initialSpeed;
			}
			else
			{
				distanceToAttack= initialSpeed;
				if(distanceToPoint<=distanceToAttack *2)
				{
					isRun = false;
					speed = initialSpeed/2;
				}
				else
				{
					isRun = true;
					speed =initialSpeed;
				}
			}

			isInMove = true;
 		}

		if(isInMove)
		{
			characterAnimator.StopPlayback();
			CalculateAngle(angle);
		}
			

		if(transform.position ==  touchPos )
		{
			isInMove = false;
			isRun = false;
			idleDirection =prevMoveDirection;
			distanceToAttack = 0;
			
		}
		 

		characterAnimator.SetBool("isInMove",isInMove);
		characterAnimator.SetBool("isRun",isRun);
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);

 	 
	}

	void Stop()
	{
		
	 	Idle();

		if(selectedObject!=null )
		{
			if(!animatorStateInfo.IsTag("AttackTag") && (!animatorStateInfo.IsTag("ReactTag")))
			{
				//characterAnimator.StopPlayback();


		 
			if(a_timer <=0f)
			{
				// call Attack()
				Attack();
				a_timer = attackTime;
			}
			a_timer -= Time.deltaTime;

			}
		}
		else
			return;
 		 
	}

 
	void Idle()
	{

		if (!animatorStateInfo.IsTag ("ReactTag")) 
		{
			
		 
			xComponent = -transform.position.x + touchPos.x;
			yComponent = -transform.position.y + touchPos.y;
		
			angle = Mathf.Atan2 (yComponent, xComponent) * Mathf.Rad2Deg;
			 
			CalculateAngle (angle);

			isInMove = false;
			isRun = false;
			idleDirection = prevMoveDirection;


			characterAnimator.SetBool ("isInMove", isInMove);
			characterAnimator.SetBool ("isRun", isRun);
			characterAnimator.SetFloat ("idleDirection", idleDirection);
			characterAnimator.SetFloat ("moveDirection", moveDirection);
		}
	}

	void Attack()
	{
		//if(Input.GetKeyDown(KeyCode.A))

		{
			characterAnimator.SetFloat("idleDirection",idleDirection);
			characterAnimator.SetFloat("moveDirection",moveDirection);
			int r = Random.Range(1,5);
			//Debug.Log("Random value "+ 4);
			characterAnimator.SetInteger("AttackRandom",r);
			characterAnimator.SetTrigger("Attack");

			//Debug.Log( "Event "+ characterAnimator.fireEvents );
		}
	}

	public void React()
	{
		Debug.Log ("Play react anim");
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);
		int r = Random.Range(1,5);
		//Debug.Log("Random value "+ 4);
		characterAnimator.SetInteger("ReactRandom",1);
		characterAnimator.SetTrigger("React");
	}

	public void AttackEnemy()
	{
		if(selectedObject!=null)
		{
			switch(LayerMask.LayerToName (selectedObject.layer) )
			{

			case "AI":
				selectedObject.GetComponent<AIComponent>().React();
				break;


			case "Objects":
				Destroy (selectedObject.gameObject);
				break;
			}
		 

		}

	}
	void FixedUpdate()
	{
		//this.GetComponent<Rigidbody2D>().MovePosition(target + velocity * Time.fixedDeltaTime);
	}

	void LateUpdate()
	{

	}
	
}
