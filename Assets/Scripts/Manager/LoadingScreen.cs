using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
	Animator animator;

	public Slider loadingSlider;

	// Use this for initialization

	void Awake()
	{
		//animator = GetComponent<Animator> ();

	}
	void Start () 
	{
		//animator.Play ("HorseRun");
		//Debug.Log ("playing anim");
	}
	
	// Update is called once per frame
	void Update () 
	{
		loadingSlider.value += Time.deltaTime;
	}
}
