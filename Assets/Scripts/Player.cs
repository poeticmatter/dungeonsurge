using UnityEngine;
using System.Collections;

public class Player : MovingObject
{
	public int startPosition = 1;
	private bool cardPlaying = false;
	public int playerHP = 3;
	public int shield = 1;
	public int playerXP;
	public int playerLevel;
	public string[] damageMessages;
	public string shieldBlockedMessage;
	public string shieldHelpedMessage;
	public string dodgedMessage;
	public bool dodge = false;

	void Update()
	{
		if (!GameManager.instance.playerTurn || cardPlaying)
		{
			return;
		}
		cardPlaying = true;
		dodge = false;
		StartCoroutine(WaitForInput());

	}
	protected override void Start()
	{
		target = FindObjectOfType<Enemy>();
		base.Start();
		GameManager.instance.uiManager.SetHP(playerHP, shield);
	}

	IEnumerator WaitForInput()
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
		GameManager.instance.cardManager.Draw();
		GameManager.instance.playerTurn = false;
		cardPlaying = false;
	}

	private void GainXP(int xpGain)
	{
		playerXP += xpGain;
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
