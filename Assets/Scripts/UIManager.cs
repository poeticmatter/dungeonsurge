using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text[] floatingText;
	private int floatingTextIndex = 0;
	public Text playerHP;
	public Text enemyName;
	public Text message;
	public Text inputMessage;
	public Text[] titles;
	public Text[] descriptions;
	public Image[] arrows;
	public Image[] cardImage;
	public Image splash;
	public Text splashText;
	public Text deck;
	public Text discard;

	public void updateHand(Card[] handCards)
	{
		for (int i = 0; i < handCards.Length; i++)
		{
			bool cardExists = i < handCards.Length;
			titles[i].enabled = cardExists;
			descriptions[i].enabled = cardExists;

			if (cardExists)
			{
				SetText(titles[i], handCards[i].title);
				SetText(descriptions[i], handCards[i].description);
				cardImage[i].color = handCards[i].cardColor;
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

	public void SetEnemyName(string name)
	{
		SetText(enemyName, name);
	}

	public void FloatText(string text, Vector3 worldPosition)
	{
		Vector2 screenPosition = Camera.current.WorldToScreenPoint( worldPosition);
		floatingText[floatingTextIndex].GetComponent<RectTransform>().position = screenPosition;
		floatingText[floatingTextIndex].text = text;
		floatingText[floatingTextIndex].GetComponent<FloatingText>().enabled = true;
		floatingTextIndex = (floatingTextIndex+1)%floatingText.Length;
		
    }

	public void ShowSplash(string text)
	{
		splashText.text = text;
		splashText.enabled = true;
		splash.enabled = true;

	}

	public void HideSplash()
	{
		splashText.enabled = false;
		splash.enabled = false;
	}

	private float cardImageYPos = 0;

	public void DisplayCardToBuy()
	{
		for (int i = 0; i < cardImage.Length; i++)
		{
			Vector3 pos = cardImage[i].rectTransform.position;
			cardImageYPos = pos.y;
			pos.y = cardImageYPos + 350;
			cardImage[i].rectTransform.position = pos;
			DisplayMessage("Pick a card to add to the top of your deck");
		}
		SelectedCard(-1);

	}

}
