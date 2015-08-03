using UnityEngine;
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
	public int aiAttackTime;
	public float aiHealthDegrageRate;

	public GameObject playerRef;
	public float distanceToAttack;
	public bool isInPlayerRadius;
	private Animator aiAnimator;
	private AnimatorStateInfo aiAnimatorState;


	float distanceToPlayer;
	string layerName;
	float a_timer;

	void Awake()
	{
		playerRef = GameObject.FindGameObjectWithTag("Player");

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

	void MoveTowardsPlayer()
	{
		distanceToPlayer = Vector2.Distance(this.transform.position, playerRef.transform.position);
		//Debug.Log(distanceToPlayer);

		if(distanceToPlayer<distanceToAttack)
		{
			Stop();
			// stop
			// call hit animation for an interval

		}
		else
		{
			this.transform.position = Vector2.MoveTowards(this.transform.position,playerRef.transform.position,aiMoveSpeed * Time.deltaTime);
		}
	}


	void Stop()
	{
		// call idle animation
		// call stop animation
		if(a_timer <=0f)
		{
			// call Attack()
			Attack();
			a_timer = aiAttackTime;
		}
		a_timer -= Time.deltaTime;
	}

	void Attack()
	{
		Debug.Log("Calling AttackAnimation");
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		layerName =  LayerMask.LayerToName(other.gameObject.layer);

		switch(layerName)
		{
		case "Player":
			isInPlayerRadius = true;
			break;
		}
		Debug.Log(layerName);
		//characterInQuicksand = true;
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
		switch(aiBehaviour)
		{
			case AIBehaviour.ATTACK:
				MoveTowardsPlayer();
			break;
		}
	}
}
