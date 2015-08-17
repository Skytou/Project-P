using UnityEngine;
using System.Collections;

public class HorseManager : MonoBehaviour 
{


	public GameObject horseRef;
	private Animator horseAnimator;
	public float speed;


	Rigidbody2D rBody;



	void Awake()
	{
		rBody = GetComponent<Rigidbody2D> ();
	}
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		//rBody.velocity = new Vector2 (speed, this.gameObject.transform.position.y);
		//rBody.AddForce(transform.right * speed ,ForceMode2D.Impulse);
		//rBody.MovePosition (Vector2.right * speed * Time.deltaTime );



		if(Input.GetKey(KeyCode.Space))
		{
			Debug.Log ("Space");
			this.gameObject.transform.Translate (Vector3.up * speed * Time.deltaTime, Space.Self);
		}

		//else
		{
			this.gameObject.transform.Translate (Vector3.right * speed * Time.deltaTime, Space.Self);

		}
	
	}
}
