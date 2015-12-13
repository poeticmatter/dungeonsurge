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
	[HideInInspector]
	public bool levelWon = false;

	public static GameManager instance = null;
	public Card[] startingDeck;

	private int playerXP;
	private int playerHP;
	private int playerShield;
	private int playerLevel;

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
		Invoke("HideSplash", restartLevelDelay);
	}


	void OnLevelWasLoaded(int index)
	{
		level++;
		FindComponents();
		InitGame();
		LoadPlayerStats();
		if (level >= 1)
		{
			uiManager.ShowSplash("Level " + level);
		}
		else
		{
			uiManager.ShowSplash("Tutorial Level");
		}
		Invoke("HideSplash", restartLevelDelay);
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
		levelWon = false;
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
		if (playerTurn || enemyTurn || levelWon)
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
		yield return new WaitForSeconds(0.5f);
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
		if (!levelWon)
		{
			playerTurn = true;
		}
	}

	public void LevelWon()
	{
		levelWon = true;
		Player player = FindObjectOfType<Player>();
		player.BuyCard();
	}

	public void NextLevel()
	{
		SavePlayerStats();
		Invoke("Restart", restartLevelDelay);
	}

	public void RestartGame()
	{
		level = -1;
		Invoke("Restart", restartLevelDelay);
	}

	private void SavePlayerStats()
	{
		if (level == 0)
		{
			return;
		}
		Player player = FindObjectOfType<Player>();
		playerHP = player.playerHP;
		playerShield = Mathf.Min(2, player.shield + 1);
	}

	private void LoadPlayerStats()
	{
		if (level <=1)
		{
			return;
		}
		Player player = FindObjectOfType<Player>();
		player.playerHP = playerHP;
		player.shield = playerShield;
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
	private void HideSplash()
	{
		uiManager.HideSplash();
	}

}


