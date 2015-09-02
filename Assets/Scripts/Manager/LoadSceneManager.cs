using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;

public enum SceneTransition
{
	MaskToScene,
	FadeToScene,
	RipplesToScene,
	FishEyeToScene,
	FishEyeToSceneVariant,


	
}

public enum SceneTransistionSelf
{
	MaskToSameScene,
	FadeToSameScene,
	RipplesToSameScene,
	FishEyeToSameScene,
	FishEyeToSameSceneVariant
}

public class LoadSceneManager : MonoBehaviour 
{

	public static LoadSceneManager instance = null;
	public Texture2D maskTexture;
	private bool _isUiVisible = true;
	
	public SceneTransition sceneTransition;

	public SceneTransistionSelf sceneTrasitionSelf;
	public Color sceneTransitionColor;
	public float fadeDelay;
	
	void Awake()
	{ 
		instance = this;
	}
	
	
	
	public void LoadSceneWithTransistion( int _sceneNum, SceneTransition _st)
	{
		if( !_isUiVisible )
			return;
		
		switch(_st)
		{
		case SceneTransition.MaskToScene:
			var mask = new ImageMaskTransition()
			{
				maskTexture = maskTexture,
				backgroundColor = Color.red,
				nextScene =  _sceneNum
			};
			TransitionKit.instance.transitionWithDelegate( mask );
			Debug.Log("Changing");
			break; 
			
		case SceneTransition.FadeToScene:
			var fader = new FadeTransition()
			{
				nextScene = _sceneNum,
				fadedDelay = fadeDelay,
				fadeToColor = sceneTransitionColor
			};
			TransitionKit.instance.transitionWithDelegate( fader );
			
			break;
			
		case SceneTransition.RipplesToScene:
			
			var ripple = new RippleTransition()
			{
				nextScene = _sceneNum,
				duration = 2.0f,
				amplitude = 1500f,
				speed = 5f
			};
			TransitionKit.instance.transitionWithDelegate( ripple );
			break;
			
		case SceneTransition.FishEyeToScene :
			
			var fishEye = new FishEyeTransition()
			{
				nextScene = _sceneNum,
				duration = 1.0f,
				size = 0.08f,
				zoom = 10.0f,
				colorSeparation = 3.0f
			};
			TransitionKit.instance.transitionWithDelegate( fishEye );
			break;
			
		case SceneTransition.FishEyeToSceneVariant :
			
			var fishEye2 = new FishEyeTransition()
			{
				nextScene = _sceneNum,
				duration = 2.0f,
				size = 0.2f,
				zoom = 100.0f,
				colorSeparation = 0.1f
			};
			TransitionKit.instance.transitionWithDelegate( fishEye2 );
			Debug.Log("Transition with fish eye2");
			break;
			
			
		}
	}

	public void LoadSameSceneWithTransistion( SceneTransistionSelf _st)
	{
		if( !_isUiVisible )
			return;
		
		switch(_st)
		{
		case  SceneTransistionSelf.MaskToSameScene:
			var mask = new ImageMaskTransition()
			{
				maskTexture = maskTexture,
				backgroundColor = Color.red,
				 
			};
			TransitionKit.instance.transitionWithDelegate( mask );
			Debug.Log("Changing");
			break; 
			
		case SceneTransistionSelf.FadeToSameScene:
			var fader = new FadeTransition()
			{

				fadedDelay = fadeDelay,
				fadeToColor = sceneTransitionColor
			};
			TransitionKit.instance.transitionWithDelegate( fader );
			
			break;
			
		case SceneTransistionSelf.RipplesToSameScene:
			
			var ripple = new RippleTransition()
			{
				 
				duration = 2.0f,
				amplitude = 1500f,
				speed = 5f
			};
			TransitionKit.instance.transitionWithDelegate( ripple );
			break;
			
		case SceneTransistionSelf.FishEyeToSameScene :
			
			var fishEye = new FishEyeTransition()
			{
				 
				duration = 1.0f,
				size = 0.08f,
				zoom = 10.0f,
				colorSeparation = 3.0f
			};
			TransitionKit.instance.transitionWithDelegate( fishEye );
			break;
			
		case SceneTransistionSelf.FishEyeToSameSceneVariant :
			
			var fishEye2 = new FishEyeTransition()
			{
				 
				duration = 2.0f,
				size = 0.2f,
				zoom = 100.0f,
				colorSeparation = 0.1f
			};
			TransitionKit.instance.transitionWithDelegate( fishEye2 );
			Debug.Log("Transition with fish eye2");
			break;
			
			
		}
	}

	
	
	void OnEnable()
	{
		TransitionKit.onScreenObscured += onScreenObscured;
		TransitionKit.onTransitionComplete += onTransitionComplete;
	}
	
	
	void OnDisable()
	{
		// as good citizens we ALWAYS remove event handlers that we added
		TransitionKit.onScreenObscured -= onScreenObscured;
		TransitionKit.onTransitionComplete -= onTransitionComplete;
	}
	
	
	void onScreenObscured()
	{
		_isUiVisible = false;
	}
	
	
	void onTransitionComplete()
	{
		_isUiVisible = true;
	}
}
