using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text playerHP;
	public Text playerXP;
	public Text enemyName;
	public Text message;
	public Text inputMessage;
	public Text[] titles;
	public Text[] xp;
	public Text[] descriptions;
	public Image[] arrows;
	public Image[] background;

	private CardManager cardManager = null;

	public void updateHand()
	{
		if (cardManager == null)
		{
			cardManager = GameManager.instance.cardManager;
		}

		Card[] handCards = cardManager.GetHandCards();
		for (int i = 0; i < titles.Length; i++)
		{
			bool cardExists = i < handCards.Length;
            titles[i].enabled = cardExists;
			xp[i].enabled = cardExists;
			descriptions[i].enabled = cardExists;

			if (cardExists)
			{
				SetText(titles[i], handCards[i].title);
				SetText(xp[i], "XP: " + handCards[i].xp);
				SetText(descriptions[i], handCards[i].description);
				background[i].color = handCards[i].cardColor;
			}

		}
	}

	public void SelectedCard(int card)
	{
		for (int i = 0; i < arrows.Length; i++)
		{
			arrows[i].enabled = i == card;
		}
	}

	public void DisplayMessage(string text)
	{
		Debug.Log(message.enabled);
		message.enabled = true;
		SetText(message, text);

	}

	public void DisplayInput(string text)
	{
		inputMessage.enabled = true;
		SetText(inputMessage, text);

	}

	public void ClearInput()
	{
		inputMessage.enabled = false;
	}

	public void SetHP(int hp, int shield)
	{
		string hpString = hp.ToString();
		for (int i = 0; i < shield; i++)
		{
			hpString = "(" + hpString + ")";
		}
		hpString = "HP: " + hpString;
		SetText(playerHP, hpString);
	}

	private void SetText(Text textUI, string newText)
	{
		textUI.text = newText.Replace("\\n", "\n");
	}

}
