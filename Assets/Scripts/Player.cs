using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MovingObject
{
	public int startPosition = 1;
	private bool inputCoroutineRunning = false;
	public int playerHP = 3;
	public int shield = 1;
	public int playerXP = 0;
	public int playerLevel = 1;
	public string[] damageMessages;
	public string shieldBlockedMessage;
	public string shieldHelpedMessage;
	public string dodgedMessage;
	public bool dodge = false;

	public Card[] cardsToBuy;

	void Update()
	{
		if (!GameManager.instance.playerTurn || inputCoroutineRunning)
		{
			return;
		}
		inputCoroutineRunning = true;
		dodge = false;
		StartCoroutine(PlayCard());

	}
	protected override void Start()
	{
		target = FindObjectOfType<Enemy>();
		base.Start();
		GameManager.instance.uiManager.SetHP(playerHP, shield);
		AddXP(0);
	}

	private bool HasXPToLevel()
	{
		return playerXP >= XPToNextLevel();
	}

	private int XPToNextLevel()
	{
		return playerLevel * 10;
    }

	IEnumerator PlayCard()
	{
		GameManager.instance.uiManager.DisplayInput("Pick a card to play");

		while (!GameManager.instance.inputManager.HasInput())
		{
			yield return null;
		}
		int input = GameManager.instance.inputManager.InputValue;
		if (input == -1)
		{
			input = 0;
		}
		Card played = GameManager.instance.cardManager.Play(input);
		while (played.playing)
		{
			yield return null;
		}
		AddXP(played.xp);
		if (HasXPToLevel())
		{
			StartCoroutine(BuyCard());
		} else
		{
			GameManager.instance.cardManager.Draw();
			GameManager.instance.playerTurn = false;
			inputCoroutineRunning = false;
		}
	}

	IEnumerator BuyCard()
	{
		Card[] cardChoices = new Card[2];
		int firstIndex = Random.Range(0, cardsToBuy.Length);
        cardChoices[0] = cardsToBuy[firstIndex];
		int secondIndex = (firstIndex + Random.Range(1, cardsToBuy.Length)) % cardsToBuy.Length; //Assures two different cards.
		cardChoices[1] = cardsToBuy[secondIndex];
		GameManager.instance.uiManager.updateHand(cardChoices);
		GameManager.instance.uiManager.DisplayCardToBuy();

		while (!GameManager.instance.inputManager.HasInput())
		{
			yield return null;
		}
		int input = GameManager.instance.inputManager.InputValue;
		if (input == -1)
		{
			input = 0;
		}
		GameManager.instance.uiManager.UndisplayCardtoBuy();
		GameManager.instance.cardManager.AddCardToTopOfDeck(cardChoices[input]);
		AddXP(-XPToNextLevel());
		playerLevel++;
		GameManager.instance.cardManager.Draw();
		GameManager.instance.playerTurn = false;
		inputCoroutineRunning = false;
	}

	private void AddXP(int xpGain)
	{
		playerXP += xpGain;
		GameManager.instance.uiManager.playerXP.text = "XP: " + playerXP + " out of " + XPToNextLevel();
	}



	public override void TakeDamage(int damage)
	{
		string message = "";
		if (dodge)
		{
			message = dodgedMessage;
		}
		else
		{
			message = damageMessages[damage - 1];
			base.TakeDamage(damage);
			if (shield > 0)
			{
				if (shield >= damage)
				{
					message += "\n" + shieldBlockedMessage;
				}
				else
				{
					message += "\n" + shieldHelpedMessage;
				}
				int temp = shield;
				shield -= damage;
				damage -= temp;
			}
			if (damage > 0)
			{
				playerHP -= damage;
				if (playerHP <= 0)
				{
					GameManager.instance.GameOver();
				}
			}
		}
		GameManager.instance.uiManager.DisplayMessage(message);
		GameManager.instance.uiManager.SetHP(playerHP, shield);
	}


}
