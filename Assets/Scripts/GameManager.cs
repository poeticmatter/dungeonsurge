using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
	public CardManager cardManager;
	public BoardManager boardManager;
	public UIManager uiManager;
	public InputManager inputManager;

	[HideInInspector] public bool playerTurn = true;
	private bool enemyTurn;

	public static GameManager instance = null;
	public Card[] startingDeck;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		boardManager = GetComponent<BoardManager>();
		cardManager = GetComponent<CardManager>();
		uiManager = GetComponent<UIManager>();
		inputManager = GetComponent<InputManager>();
		DontDestroyOnLoad(gameObject);
		InitGame();
		InitDeck();
	}


	void OnLevelWasLoaded(int index)
	{
		InitGame();
	}

	private void InitGame()
	{
		boardManager.GenerateBoard(10);
    }

	private void InitDeck()
	{
		cardManager.InitDeck(startingDeck);
		cardManager.Draw();
	}

	void Update()
	{
		if (playerTurn || enemyTurn)
		{
			return;
		}
		Debug.Log("update1");
		StartCoroutine(EnemyTurn());
		Debug.Log("update2");
	}

	IEnumerator EnemyTurn()
	{
		enemyTurn = true;
		Debug.Log("Enemy Turn Start");
		yield return new WaitForSeconds(1f);
		//Do enemy stuff
		Debug.Log("Enemy Turn Over");

		enemyTurn = false;
		playerTurn = true;
	}

}


