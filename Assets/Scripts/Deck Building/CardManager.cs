using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardManager
{

	public int handSize { get; private set; }

	private List<Card> deck;
	private List<Card> hand;
	private List<Card> discard;

	public CardManager(int handSize)
	{
		this.handSize = handSize;
		deck = new List<Card>();
		hand = new List<Card>();
		discard = new List<Card>();
	}

	public void InitDeck(List<Card> cards)
	{
		deck.AddRange(cards);
	}

	public void Play(int cardIndex)
	{
		Card temp = hand[cardIndex];
		discard.AddRange(hand);
		hand.Clear();
		temp.Play();
	}

	public void AddCardToTopOfDeck(Card card)
	{
		deck.Add(card);
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
	}

	public void ShuffleHandAndDiscardIntoDeck()
	{
		deck.AddRange(hand);
		deck.AddRange(discard);
		Shuffle(deck);
	}
	
	public void ShuffleDiscardIntoDeck()
	{
		deck.AddRange(discard);
		discard.Clear();
		Shuffle(deck);
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


}
