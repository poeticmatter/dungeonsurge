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
	public float restartLevelDelay = 0.5f;

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
		DontDestroyOnLoad(gameObject);
		FindComponents();
		InitDeck();
		InitGame();
	}


	void OnLevelWasLoaded(int index)
	{
		level++;
		FindComponents();
		InitGame();
	}

	private void FindComponents()
	{
		uiManager = FindObjectOfType<UIManager>();
		boardManager = GetComponent<BoardManager>();
		cardManager = GetComponent<CardManager>();
		inputManager = GetComponent<InputManager>();
	}

	private void InitGame()
	{
		boardManager.GenerateBoard(10);
		boardManager.SpawnPlayer();
		boardManager.SpawnEnemy(level);
		cardManager.ShuffleHandAndDiscardIntoDeck();
		cardManager.Draw();
	}

	private void InitDeck()
	{
		cardManager.InitDeck(startingDeck);
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
		uiManager.DisplayInput("Enemy Turn");
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

	public void NextLevel()
	{
		Invoke("Restart", restartLevelDelay);
	}

	public void GameOver()
	{
		enabled = false;
		Debug.Log("Game Over");
	}

	private void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

}


