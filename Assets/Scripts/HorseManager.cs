﻿using UnityEngine;
using Prime31;
using System.Collections;
using System.Collections.Generic;

public class HorseManager : MonoBehaviour 
{

	public static HorseManager instance = null;

	public GameObject horseRef;
	private Animator horseAnimator;
	 
	public float distanceTravelled;

	private Vector3 lastPosition;


	Rigidbody2D rBody;

	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;


	void Awake()
	{
		instance = this;
		rBody =  GetComponent<Rigidbody2D> ();
		_animator = horseRef.GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}
	// Use this for initialization
	void Start () 
	{
		lastPosition = this.transform.position;
	}

	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion

	void HorseMovement()
	{
		if( _controller.isGrounded )
			_velocity.y = 0;

		 
		 
			normalizedHorizontalSpeed = 1;

		if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "HorseRun" ) );
		


		// we can only jump whilst grounded
		if( _controller.isGrounded && Input.GetKeyDown( KeyCode.Space ) )
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );

			_animator.Play (Animator.StringToHash ("HorseJump"));
			//_animator.Play( Animator.StringToHash( "Jump" ) );
		}


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets uf jump down through one way platforms
		if( _controller.isGrounded && Input.GetKey( KeyCode.DownArrow ) )
		{
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}

	// Update is called once per frame
	void Update () 
	{

		HorseMovement();
		distanceTravelled += Vector3.Distance(transform.position, lastPosition);
		lastPosition = transform.position;

		//Debug.Log (this.transform.position);
		//Debug.Log ("Distance Travelled " + distanceTravelled);
		/*if((int)distanceTravelled==10f)
		{
			Debug.Log ("reset");
		}*/
	}
}
