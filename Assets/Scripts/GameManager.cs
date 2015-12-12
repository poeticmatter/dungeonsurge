using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
	[HideInInspector]
	public CardManager cardManager;
	[HideInInspector]
	public BoardManager boardManager;
	[HideInInspector]
	public UIManager uiManager;
	[HideInInspector]
	public InputManager inputManager;
	public int level;

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
		boardManager.SpawnPlayer();
		boardManager.SpawnEnemy(level);
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
		StartCoroutine(EnemyTurn());
	}

	private Enemy currentEnemy = null;

	IEnumerator EnemyTurn()
	{
		enemyTurn = true;
		uiManager.DisplayMessage("Enemy Turn");
		for (int i = 0; i < boardManager.enemies.Count; i++)
		{
			currentEnemy = boardManager.enemies[i];
			currentEnemy.ChooseAction();
			while (currentEnemy!=null && currentEnemy.moving)
			{
				yield return null;
			}
			currentEnemy = null;
		}
		enemyTurn = false;
		playerTurn = true;
	}

}


