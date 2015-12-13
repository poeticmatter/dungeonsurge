using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{

	public int handSize = 2;

	private List<Card> deck;
	private List<Card> hand;
	private List<Card> discard;
	

	public void InitDeck(Card [] cards)
	{
		hand = new List<Card>();
		discard = new List<Card>();
		deck = new List<Card>();
		for (int i = 0; i < cards.Length; i++)
		{
			AddCardToTopOfDeck(cards[i]);
		}
		Shuffle(deck);
		UpdateUI();
	}

	public Card Play(int cardIndex)
	{
		Card temp = hand[cardIndex];
		discard.AddRange(hand);
		hand.Clear();
		temp.Play();
		GameManager.instance.uiManager.SelectedCard(cardIndex);
		UpdateUI();
		return temp;
	}

	public void AddCardToTopOfDeck(Card card)
	{
		Card instance = Instantiate(card);
		DontDestroyOnLoad(instance);
		deck.Add(instance);
		UpdateUI();
	}

	public void Draw()
	{
		while (hand.Count < handSize)
		{
			if (deck.Count <= 0)
			{
				ShuffleDiscardIntoDeck();
			}
			if (deck.Count > 0)
			{
				hand.Add(deck[0]);
				deck.RemoveAt(0);
			} else
			{
				Debug.LogError("No cards to draw!");
				break;
			}
		}
		GameManager.instance.uiManager.updateHand(GetHandCards());
		GameManager.instance.uiManager.SelectedCard(-1);
		UpdateUI();
	}

	public void ShuffleHandAndDiscardIntoDeck()
	{
		deck.AddRange(hand);
		deck.AddRange(discard);
		hand.Clear();
		discard.Clear();
		Shuffle(deck);
	}
	
	public void ShuffleDiscardIntoDeck()
	{
		deck.AddRange(discard);
		discard.Clear();
		Shuffle(deck);
	}

	public Card[] GetHandCards()
	{
		return hand.ToArray();
	}

	private void Shuffle(List<Card> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Card temp = list[i];
			int randomIndex = Random.Range(i, list.Count);
			list[i] = list[randomIndex];
			list[randomIndex] = temp;
		}
	}

	private void UpdateUI()
	{
		GameManager.instance.uiManager.deck.text = "Deck: " + deck.Count;
		GameManager.instance.uiManager.discard.text = "Discard: " + discard.Count;
	}

}
