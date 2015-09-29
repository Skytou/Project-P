using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Prime31;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PlayerBehaviour
{
	IDLE, 
	MOVE,
	MOVEANDTHROW,
	ATTACK,
	REACT
}

public class PlayerMovement : MonoBehaviour
{
	public PlayerBehaviour playerBehaviour;
    private Animator CoinAnimUI;
    public GameObject coinUI;
    public GameObject coinParticles;
    public List<GameObject> coinParticlesList;
    public GameObject normalSelectionCircle;
	public float playerHealth;
	public float speed ,knifeThrowSpeed ;
	public Vector2 velocity;
	private Vector3 target;
	private Animator characterAnimator;
	 
	private AnimatorStateInfo animatorStateInfo;
  	float idleDirection,moveDirection , prevMoveDirection , knifeThrowDirection;

	float initialSpeed, intialDistanceToAttack , initialDistanceToThrow;
	float xComponent;
	float yComponent;
	float angle;

	Vector3 touchPos;
	string layerName;

	bool isInMove;
 	bool isRun;

    public GameObject PotParticleObj;

	//string selectedObject;
	GameObject selectedEnemy, selectedObject;

 	public float distanceToPoint, distanceToAttack, distanceToThrow;

	public float attackTime;

	public GameObject spinSelectionCircle;
	public float spinRange;
	public float spinTime;
	public  float sTime;

	public GameObject knife;

	RaycastHit2D hit ;
	RaycastHit hit3D;
	float a_timer;

	public bool canThrow;
	public bool isKnifeThrow;
	public GameObject[] knifePrefab ;
	public  GameObject knifeThrowPoint;

	public float fireBallTimer;
	public GameObject fireBallPrefab;
	public GameObject fireBall;
	public float fTimer;

	public float interpolationScale;

	bool canSpin;
	bool throwed;

	public float spinAttackDistance;

	void Awake()
	{
		target = transform.position;
		characterAnimator = GetComponent<Animator>();
        PotParticleObj = Instantiate(PotParticleObj) as GameObject;
        PotParticleObj.SetActive(false);	 
	}

	void Start()
	{
		initialSpeed = speed;
        if (coinUI != null)
            CoinAnimUI = coinUI.GetComponent<Animator>();
		intialDistanceToAttack = distanceToAttack;
		//Debug.Log (intialDistanceToAttack);
		initialDistanceToThrow = distanceToThrow;
		idleDirection =5;
		//moveDirection=-1;
		characterAnimator.SetFloat("idleDirection",idleDirection);
		sTime = spinTime;
		fTimer = fireBallTimer;
		spinSelectionCircle.SetActive ((false));
		normalSelectionCircle.SetActive (true);
		throwed = false;
		GameGlobalVariablesManager.isPlayerSpin = false;
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
	}

	 
	void OnTriggerEnter2D(Collider2D other)
	{
		layerName = LayerMask.LayerToName (other.gameObject.layer);
		
		switch (layerName)
		{
		case "Player":

			break;
			
		case "AI":
			if(canSpin)
			{
				//other.gameObject.GetComponent<AIComponent> ().healthBar.SetActive (false);
				//other.gameObject.GetComponent<AIComponent> ().Death ();
			}
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
		case "EnemyTrigger5":
			Debug.Log ("touched trigger " + other.gameObject.name);
			LevelManager.instance.activateAISpawn [5] = true;
			other.gameObject.SetActive (false);
			break;
		case "EnemyTrigger6":
			Debug.Log ("touched trigger " + other.gameObject.name);
			LevelManager.instance.activateAISpawn [6] = true;
			other.gameObject.SetActive (false);
			break;

		case "Door0":

			if (LevelManager.instance.stageCompleted [0]) {
				LevelManager.instance.doorsToBeOpened [0].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door1":

			if (LevelManager.instance.stageCompleted [1]) {
				LevelManager.instance.doorsToBeOpened [1].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door2":

			if (LevelManager.instance.stageCompleted [2]) {
				LevelManager.instance.doorsToBeOpened [2].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door3":

			if (LevelManager.instance.stageCompleted [3]) {
				LevelManager.instance.doorsToBeOpened [3].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door4":

			if (LevelManager.instance.stageCompleted [4]) {
				LevelManager.instance.doorsToBeOpened [4].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door5":

			if (LevelManager.instance.stageCompleted [5]) {
				LevelManager.instance.doorsToBeOpened [5].GetComponent<Doors> ().OpenDoor ();
			}

			break;
		case "Door6":

			if (LevelManager.instance.stageCompleted [6]) {
				LevelManager.instance.doorsToBeOpened [6].GetComponent<Doors> ().OpenDoor ();
			}

			break;

		case "Portal":

			switch(GameGlobalVariablesManager.currentLevelnumber)
			{
			case 1:
				GameGlobalVariablesManager.level1Completed = true;
				break;
			case 2:
				GameGlobalVariablesManager.level2Completed = true;
				Application.LoadLevel (3);
				break;
			case 3:
				GameGlobalVariablesManager.level3Completed = true;
				break;
			case 4:
				GameGlobalVariablesManager.level4Completed = true;
				break;

			}
			break;

		default:
			
			break;
		}
	}
	 
	 




	void Spin()
	{
		if (sTime >= 0f) 
		{  
			if (!animatorStateInfo.IsName ("VijaySpin"))
			{
				characterAnimator.SetBool ("isSpin", canSpin);
				normalSelectionCircle.SetActive (false);
				spinSelectionCircle.SetActive ((true));
				//playerSpinCircleCollider.radius = 8f;
			}

			if (animatorStateInfo.IsName ("VijaySpin")) 
			{ 
				GameObject[] enemyList = GameObject.FindGameObjectsWithTag ("AI");
				foreach (var e in enemyList) {
					Debug.Log (e.gameObject.name);
					Debug.Log (Vector2.Distance (e.gameObject.transform.position, this.transform.position));
					if (Vector2.Distance (e.gameObject.transform.position, this.transform.position) < spinRange) {
						e.gameObject.GetComponent<AIComponent> ().healthBar.SetActive (false);

						e.gameObject.GetComponent<AIComponent> ().Death ();

					}
				}
			}
				
			
		}
 
		else
		{
			Debug.Log ("time over");
			GameGlobalVariablesManager.isPlayerSpin = false;
			canSpin = false;
			sTime = spinTime;
			normalSelectionCircle.SetActive (true);
			spinSelectionCircle.SetActive ((false));
			//playerSpinCircleCollider.radius = 3.5f;
			characterAnimator.SetBool ("isSpin", canSpin);

		}



	}

	void ShowFireBallCircle()
	{
		if (fTimer >= 0f) 
		{
			//Debug.Log ("show circle");
			//fireBallPrefab.SetActive (true);
			if(fireBall==null)
			fireBall = GameObject.Instantiate (fireBallPrefab, this.transform.position, Quaternion.identity) as GameObject; 
			 
  

		}

		else
		{
			Debug.Log ("hide circle");
			GameGlobalVariablesManager.isFireBallThrown = false;
			fTimer = fireBallTimer;
			Destroy (fireBall.gameObject);
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
				
				//distanceToAttack= intialDistanceToAttack;
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

	void MoveTowardsThrowPoint()
	{
		distanceToPoint = Vector2.Distance(transform.position, touchPos);


		if(distanceToPoint<distanceToThrow)
		{
			StopAndThrow ();
			Debug.Log ("Stopping");
		}

		else
		{
			Debug.Log ("ins");
			xComponent = -transform.position.x + touchPos.x;
			yComponent = -transform.position.y + touchPos.y;

			angle = Mathf.Atan2(yComponent, xComponent) * Mathf.Rad2Deg;
			transform.position = Vector2.MoveTowards(transform.position, touchPos, speed * Time.deltaTime);

			isInMove = true;
			isRun = true;
			//distanceToThrow =1;
			speed = initialSpeed;
				
			 
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
			distanceToThrow = 0;

		}


		characterAnimator.SetBool("isInMove",isInMove);
		characterAnimator.SetBool("isRun",isRun);
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);

	}

	void StopAndThrow()
	{
		Idle ();
		if(canThrow)
		ThrowKnife ();
	}


	void Stop()
	{
		
	 	Idle();

		if(selectedObject!=null )
		{
			if(!animatorStateInfo.IsTag("AttackTag") && (!animatorStateInfo.IsTag("ReactTag")))
			{
				if(!GameGlobalVariablesManager.isKnifeThrow)
				{
					if(a_timer <=0f)
					{
						// call Attack()
						if(selectedObject!=null  )
						Attack();

						a_timer = attackTime;
					}
					a_timer -= Time.deltaTime;
				}

				else
				{
					//ThrowKnife ();

				}


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
        Debug.Log("Attack()");
		//if(Input.GetKeyDown(KeyCode.A))
		if(selectedObject!=null)
		{
			characterAnimator.SetFloat("idleDirection",idleDirection);
			characterAnimator.SetFloat("moveDirection",moveDirection);
			int r = Random.Range(1,4);
			//Debug.Log("Random value "+ 4);
			characterAnimator.SetInteger("AttackRandom",r);
			characterAnimator.SetTrigger("Attack");

			//Debug.Log( "Event "+ characterAnimator.fireEvents );
		}
	}

	void ThrowKnife()
	{
		//Idle ();
		characterAnimator.SetFloat("idleDirection",idleDirection);
		characterAnimator.SetFloat("moveDirection",moveDirection);
		characterAnimator.SetTrigger ("Throw");
		canThrow = false;

        AudioMgr.Inst.PlaySfx(SfxVals.Knife);

		//	knife = Instantiate (knifePrefab, knifeThrowPoint.transform.position, Quaternion.identity) as GameObject;
		//knife.SetActive (false);

	}


	public void LaunchKnife()
	{
		Debug.Log ("throwing Knife");
		knife = Instantiate (knifePrefab[(int)moveDirection],  knifeThrowPoint.transform.position, Quaternion.identity) as GameObject;
		GameGlobalVariablesManager.numberOfKnives--;
		knife.SetActive (true);
		throwed = true;
		//Debug.Log (touchPos);
		//knife.transform.position = Vector2.MoveTowards(knife.transform.position,touchPos, knifeThrowSpeed * Time.deltaTime);

		knife.GetComponent<ThrowKnife>().ThowKnifeTo(touchPos,selectedObject, true);
		//GameGlobalVariablesManager.isKnifeThrow = false;
		//isKnifeThrow = false;

	}

	void DestroyUsingKnife()
	{
		if (knife != null) 
		{ 
			knife.transform.position = Vector2.MoveTowards (knife.transform.position, new Vector2 (selectedObject.transform.position.x, selectedObject.transform.position.y + 5), knifeThrowSpeed * Time.deltaTime);
			if (knife.transform.position.x == selectedObject.transform.position.x) {
				if (selectedObject.GetComponent<AIComponent> () != null) {
					selectedObject.GetComponent<AIComponent> ().Death ();
					if (knife != null)
						Destroy (knife.gameObject);
					throwed = false;
					GameGlobalVariablesManager.isKnifeThrow = false;
					//isKnifeThrow = false;
				} else {
					Destroy (selectedObject);
					if (knife != null)
						Destroy (knife.gameObject);
					throwed = false;
					GameGlobalVariablesManager.isKnifeThrow = false;
					//isKnifeThrow = false;

				}
			}
		}
	}

	public void React()
	{
		if(!animatorStateInfo.IsTag("ReactTag") || !animatorStateInfo.IsTag("MovementTag") )
		{
			GameGlobalVariablesManager.playerHealth--;
			//InGameHUD.instance.healthBarSlider.value--;
			playerHealth--;
			//Debug.Log ("Play react anim");
			characterAnimator.SetFloat("idleDirection",idleDirection);
			characterAnimator.SetFloat("moveDirection",moveDirection);
			int r = Random.Range(1,5);
			//Debug.Log("Random value "+ 4);
			characterAnimator.SetInteger("ReactRandom",1);
			characterAnimator.SetTrigger("React");
		}

	}

	public void PlayerDead()
	{
		if(GameGlobalVariablesManager.playerHealth<=0)
		{
			Debug.Log ("pop up for game over scene");
            // ponz.2do
		}
	}

	public void AttackEnemy()
	{
        Debug.Log("AttackEnemy()");
        Debug.Log("executing attack in player script");
		if(selectedObject!=null)
		{
            switch (LayerMask.LayerToName(selectedObject.layer))
            {
                case "AI":
                    AudioMgr.Inst.PlaySfx(SfxVals.Sword);
                    CoinAnimUI.SetTrigger("CanBlink");
                    selectedObject.GetComponent<AIComponent>().React();
                    //selectedObject.GetComponent<AIComponent>().aiAnimatorState
                    break;


                case "Objects":
                    PotParticleObj.SetActive(true);
                    PotParticleObj.transform.position = selectedObject.transform.position + new Vector3(0, 1.3f, 0);
                    StartCoroutine(HideAfterTime(1.0f));

                    Destroy(selectedObject.gameObject);
                    if (CoinAnimUI != null)
                        CoinAnimUI.SetTrigger("CanBlink");

                    StartCoroutine(PlayCoinAnim(selectedObject.transform.position));

                    AudioMgr.Inst.PlaySfx(SfxVals.PotCrash);
                    AudioMgr.Inst.PlaySfx(SfxVals.CoinCollect);
                    GameGlobalVariablesManager.totalNumberOfCoins += 20;
                    break;
            }
		}
	}


    IEnumerator HideAfterTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PotParticleObj.SetActive(false);
    }

	void Update()
	{
		animatorStateInfo = characterAnimator.GetCurrentAnimatorStateInfo (0);

		if (!GameGlobalVariablesManager.isKnifeThrow) 
		{
			if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ())
			{
				target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				hit = Physics2D.Raycast (target, Vector2.zero);


				if (hit.collider != null) 
				{
					layerName = LayerMask.LayerToName (hit.collider.gameObject.layer);

					//	Debug.Log (layerName);

					switch (layerName)
					{

					case "AI":

						selectedObject = hit.collider.gameObject;
						distanceToAttack = initialSpeed;
						if (selectedObject.GetComponent<AIComponent> ().selectionMarker != null) 
						{
							selectedObject.GetComponent<AIComponent> ().selectionMarker.SetActive (true);
						}
						touchPos = selectedObject.transform.position;

						playerBehaviour = PlayerBehaviour.MOVE;
						break;

					case "Objects":
						selectedObject = hit.collider.gameObject;
						distanceToAttack = intialDistanceToAttack / 2;
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


						{
							touchPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
							if (selectedObject != null) {
                                //ponz.2do null check
								//selectedObject.GetComponent<AIComponent>().selectionMarker.SetActive (false);

                                if(selectedObject.GetComponent<AIComponent>() != null)
								    selectedObject.GetComponent<AIComponent>().selectionMarker.SetActive (false);

							}
							playerBehaviour = PlayerBehaviour.MOVE;
						}
                        break;
					}

				} else {
					touchPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					if (selectedObject != null) {
                        // ponz.2do
                        if (selectedObject.GetComponent<AIComponent>() != null)
                            selectedObject.GetComponent<AIComponent>().selectionMarker.SetActive (false);
						selectedObject = null;
					}
					playerBehaviour = PlayerBehaviour.MOVE;
				}
			} 

			if (GameGlobalVariablesManager.isPlayerSpin) 
			{
				canSpin = true;
				sTime -= Time.deltaTime;
				Spin ();
				 
			}
		}
		// else condition for player throwing knife...
		else
		{
			if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()) 
            {
				distanceToThrow = initialDistanceToThrow;
				target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				hit = Physics2D.Raycast (target, Vector2.zero);

				if (hit.collider != null) {
					layerName = LayerMask.LayerToName (hit.collider.gameObject.layer);

					Debug.Log (layerName);

					switch (layerName) {

					case "AI":

						selectedObject = hit.collider.gameObject;
						if (selectedObject.GetComponent<AIComponent> ().selectionMarker != null) {
							selectedObject.GetComponent<AIComponent> ().selectionMarker.SetActive (true);
						}
						touchPos = selectedObject.transform.position;
						canThrow = true;
						playerBehaviour = PlayerBehaviour.MOVEANDTHROW;
						break;

					case "Objects":
						selectedObject = hit.collider.gameObject;
						touchPos = selectedObject.transform.position;

						Debug.Log ("touch pos is generated");
						canThrow = true;
						playerBehaviour = PlayerBehaviour.MOVEANDTHROW;
						break;

					case "WallLightLayer":
						touchPos = this.transform.position;

						break;

					case "AreaLock":
						touchPos = this.transform.position;
						break;

					default:
						{
							touchPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
							if (selectedObject != null) {
								selectedObject.GetComponent<AIComponent> ().selectionMarker.SetActive (false);
							}
							canThrow = true;
							playerBehaviour = PlayerBehaviour.MOVEANDTHROW;
						}
						break;
					}

				} else {

					touchPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					if (selectedObject != null) {
						selectedObject.GetComponent<AIComponent> ().selectionMarker.SetActive (false);
						selectedObject = null;
					}
					//canThrow = true;
					playerBehaviour = PlayerBehaviour.MOVEANDTHROW;
				}
			} 
		} 

		switch (playerBehaviour) 
		{
		case PlayerBehaviour.IDLE:

			break;
		case PlayerBehaviour.MOVE:
			if(!GameGlobalVariablesManager.isFireBallThrown)
			MoveTowardsPoint ();

			break;
		case PlayerBehaviour.MOVEANDTHROW:
			 
			MoveTowardsThrowPoint ();

			break;
		case PlayerBehaviour.ATTACK:

			break;

		case PlayerBehaviour.REACT:

			break;
		}

		if(GameGlobalVariablesManager.isFireBallThrown)
		{
			//Debug.Log ("countdowntimer");
			fTimer -= Time.deltaTime;
			ShowFireBallCircle ();
		}

		PlayerDead ();
		 
		/*if(isKnifeThrow)
		{
			Idle ();
		}*/

		/*if (throwed)
			DestroyUsingKnife();*/
	}


    IEnumerator PlayCoinAnim(Vector3 newPos)
    {
        coinParticles.SetActive(true);
        coinParticles.transform.position = newPos;
        float curTime = 0;
        
        for (int i = 0; i < coinParticlesList.Count; i++)
        {
            coinParticlesList[i].transform.position = newPos + new Vector3(Random.Range(1, 3), Random.Range(1, 2), 0);
        }
                    
        while (curTime < 5f)
        {
            curTime += Time.deltaTime;
            for (int i = 0; i < coinParticlesList.Count; i++)
            {
                coinParticlesList[i].transform.position 
                    += new Vector3(0, Time.deltaTime * 10, 0);
            }
            yield return null;
        }
        coinParticles.SetActive(false);
    }

   
}
