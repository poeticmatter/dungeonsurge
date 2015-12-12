using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public GameObject [] boardSprites;

	private MovingObject[] board;
	public List<Enemy> enemies;

	public Player playerPrefab;
	public Enemy enemyPrefab;
	

	public int Length()
	{
		return board.Length;
	}

	public void GenerateBoard(int length)
	{
		enemies = new List<Enemy>();
        board = new MovingObject[length];
		for (int i = 0; i < length; i++)
		{
			board[i] = null;
			AddSpriteAtPosition(new Vector3(i, 0, 0));
		}
	}

	public void RegisterOnBoard(MovingObject o, int index)
	{
		o.transform.position = new Vector3(index, 0, 0);
		board[index] = o;
		o.boardPosition = index;
	}

	public void CheckMove(MovingObject o, int distance)
	{
		int initialPosition = o.boardPosition;
		int lastLegalMove = 0;
		for (int i = 0; Mathf.Abs(i) <= Mathf.Abs(distance); i += distance>0 ? +1 : -1)
		{
			int newPos = i + initialPosition;
			if (newPos < 0 || newPos >= board.Length)
			{
				break;
			}
			if (board[newPos] == null)
			{
				lastLegalMove = i;
			}
		}
		int newPosition = initialPosition + lastLegalMove;
        if (lastLegalMove !=0)
		{
			board[initialPosition] = null;
			board[newPosition] = o;
			o.boardPosition = newPosition;
			StartCoroutine(o.SmoothMovement(new Vector3(newPosition, 0, 0), Mathf.Abs(lastLegalMove)));
		}
		
	}


	private void AddSpriteAtPosition(Vector3 position)
	{
		GameObject sprite = boardSprites[Random.Range(0, boardSprites.Length)];
		Instantiate(sprite, position, Quaternion.identity);
	}

	public void SpawnEnemy(int level)
	{
		Enemy enemy = Instantiate(enemyPrefab);
		RegisterOnBoard(enemy, enemy.startPosition);
		enemies.Add(enemy);
	}

	public void SpawnPlayer()
	{
		Player playerInstance = Instantiate(playerPrefab);
		RegisterOnBoard(playerInstance, playerInstance.startPosition);
	}
}
