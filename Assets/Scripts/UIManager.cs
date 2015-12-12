using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text message;
	public Text[] titles;
	public Text[] xp;
	public Text[] descriptions;

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
				titles[i].text = handCards[i].title;
				xp[i].text = "XP: " + handCards[i].xp;
				descriptions[i].text = handCards[i].description;
			}

		}
	}

	public void DisplayMessage(string text)
	{
		message.enabled = true;
		message.text = text;
		
	}

	public void ClearMessage()
	{
		message.enabled = false;
	}

}
