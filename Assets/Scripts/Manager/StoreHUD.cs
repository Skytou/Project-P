using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoreHUD : MonoBehaviour 
{
	public int storeIndex;

	public Sprite life, cyclone, energy, bomb, sword, timer, armor, coins, throwKnife;


	public Image storeImage;

	//public GameObject storeDialogue;
	public Text storeText;

	public string selectedPowerUp;


	// Use this for initialization
	void Start ()
	{
	
	}

	public void BackButton()
	{
		
	}

	public void BuyLife()
	{
		storeImage.sprite = life;
		storeText.text = "The life of the hero can be upgraded to give more survival in-spite the attacks taken. Upgrade to get extra 20 survival capacity";
		selectedPowerUp = "Life";
	}

	public void BuyKnife()
	{
		storeImage.sprite = throwKnife;
		storeText.text = "This special weapon helps to kill the enemies from a distance. \nMore the throw knives more are the escape opportunities";
		selectedPowerUp = "Knife";
	}

	public void BuyCyclone()
	{
		storeImage.sprite = cyclone;
		storeText.text ="Master move by the player to kill the enemies with a circular spin";
		selectedPowerUp = "Cyclone";
	}

	public void BuyEnergy()
	{
		storeImage.sprite = energy;
		storeText.text = "The hero can sustain in the battle field only till his energy lasts. \nUpgrade to get extra 20 energy to give him more fighting time";
		selectedPowerUp = "Energy";
	}

	public void BuyBomb()
	{
		
		storeImage.sprite = bomb;
		storeText.text ="Stun bomb to stuns the enemies for a certain time. Purchase more bombs to freeze enemies at multiple instances";
		selectedPowerUp = "Bomb";
	}

	public void BuyTimerFreeze()
	{
		storeImage.sprite = timer;
		storeText.text ="An energy freeze to pause the energy depletion of the hero. This gives more fighting time";
		selectedPowerUp = "TimerFreeze";
	}

	public void BuySword()
	{
		storeImage.sprite =sword;
		storeText.text ="The power of the sword determines the hits to kill the enemy. \nUpgrade the sword to kill the enemy fast";
		selectedPowerUp = "Sword";
	}

	public void BuyCoins()
	{
		storeImage.sprite = coins;
		storeText.text = "Buy more coins and use them in store to get more upgrades";
		selectedPowerUp ="Coins";
	}


	public void BuyArmor()
	{
		storeImage.sprite = armor;
		storeText.text = "The armour protects the hero from attack by the enemies. \nUpgrade the armour to reduce damage ";
		selectedPowerUp = "Armor";
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
