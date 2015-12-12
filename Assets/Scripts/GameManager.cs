using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour
{
	public CardManager cardManager;
	public BoardManager boardManager;
	public static GameManager instance = null;

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
		DontDestroyOnLoad(gameObject);
		InitGame();
	}

	void OnLevelWasLoaded(int index)
	{
		InitGame();
	}

	void InitGame()
	{
		cardManager = new CardManager(2);
		boardManager.GenerateBoard(10);
    }

}


