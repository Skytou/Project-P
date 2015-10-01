using UnityEngine;
using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HorseManager : MonoBehaviour 
{

	public static HorseManager instance = null;

	public GameObject horseRef;
    public int sceneLvl;

    private Animator horseAnimator;
	 
	public float distanceTravelled;
    

	private Vector3 lastPosition;

    int coinsCollected;
    float life;
    float timer;
    bool victory;
    public bool gameRunning;

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

    // UI Components
    public Text coinText;
    public Text timerText;
    public GameObject gameOverWin;
    public Text gameOverWinText;
    public GameObject gameOverLose;
    public Text gameOverLoseText;

    AudioSource audioSrc;
    public AudioClip CoinCollectSfx;
    public AudioClip CrashSfx;

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
		lastPosition = transform.position;
        life = 3;
        gameRunning = true;
        Time.timeScale = 0.0f;
        audioSrc = GetComponent<AudioSource>();
        GameGlobalVariablesManager.isCameraLocked = false;
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
        if(col.gameObject.tag=="Coin")
        {
            col.gameObject.SetActive(false);
            coinsCollected += 1;
            //Debug.Log(coinsCollected);
            if(!GameGlobalVariablesManager.isSoundMuted)
                audioSrc.PlayOneShot(CoinCollectSfx, 0.7f);
        }
        else if(col.gameObject.tag == "Obstacle")
        {
            life -= 1;
            HorseHUD.instance.SetLifeInGame();
            //health.value = life;
            if (life <= 0)
            {
                GameOver();
            }
            else
            {
                if (!GameGlobalVariablesManager.isSoundMuted)
                    audioSrc.PlayOneShot(CrashSfx, 0.8f);
            }
            StartCoroutine(FlashSprite(8.0f, 0.7f, new Color(1f, 1f, 1f, 0f)));
        }
        else
        {
            Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        }
	}


	void onTriggerExitEvent( Collider2D col )
	{
		//Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
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
		if((  _controller.isGrounded && Input.GetKeyDown( KeyCode.Space ) || _controller.isGrounded && Input.GetButtonDown("Fire1") ) && !EventSystem.current.IsPointerOverGameObject () )
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
        if (gameRunning)
        {
            HorseMovement();
            distanceTravelled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;

            // timer
            timer += Time.deltaTime;
            // UI
            timerText.text = "" + (int) timer;
            coinText.text = "" + (int) coinsCollected;

            TimeKeeper();

            //Debug.Log (this.transform.position);
            //Debug.Log ("Distance Travelled " + distanceTravelled);
            /*if((int)distanceTravelled==10f)
            {
                Debug.Log ("reset");
            }*/
        }
    }

    // check for gameOver
    void TimeKeeper()
    {
        if (sceneLvl == 1)
        {
            // condition for lvl 1
            if(timer > 60)
            {
                victory = true;
                GameOver();
            }
            if (timer > 30)
            {
               SpawnTrigger.maxRange = 10;
            }

        }
        else if (sceneLvl == 2)
        {
            // condition for lvl 2
            if (timer > 90)
            {
                victory = true;
                GameOver();
            }
            if (timer > 40)
            {
                SpawnTrigger.maxRange = 10;
            }
        }
        else if (sceneLvl == 3)
        {
            // condition for lvl 3
            if (timer > 120)
            {
                victory = true;
                GameOver();
            }
            if (timer > 60)
            {
                SpawnTrigger.maxRange = 10;
            }
        }
    }

    // gameOver function
    void GameOver()
    {
        gameRunning = false;        
        // activate gameover screen and pause game
        if (victory)
        {
            //gameOverWin.SetActive(true);
            //gameOverWinText.text = "Coins: "+coinsCollected;
            HorseHUD.instance.GameOverWin(coinsCollected);
        }
        else
        {
            //gameOverLose.SetActive(true);
            //gameOverLoseText.text = "Coins: " + coinsCollected;
            HorseHUD.instance.GameOverLose(coinsCollected);
        }
        GameGlobalVariablesManager.totalNumberOfCoins += coinsCollected;
        SavedData.Inst.SaveAllData();

        if (!GameGlobalVariablesManager.isSoundMuted)
            audioSrc.PlayOneShot(CrashSfx, 0.8f);
    }

    IEnumerator FlashSprite(float timeScale, float duration, Color blinkColor)
    {

        var sprite = GetComponentInChildren<SpriteRenderer>();
        Debug.Log(sprite);
        var elapsedTime = 0f;
        while (elapsedTime <= duration)
        {
            sprite.color = blinkColor;

            blinkColor.a = Mathf.PingPong(elapsedTime * timeScale, 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // revert to our standard sprite color
        blinkColor.a = 1f;
        sprite.color = blinkColor;

    }
}
