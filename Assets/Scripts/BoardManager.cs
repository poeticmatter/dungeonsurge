using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	public GameObject [] boardSprites;

	public MovingObject[] board;
	public List<Enemy> enemies;

	public Player playerPrefab;
	public Enemy tutorialEnemy;
	public Enemy[] firstStageEnemies;
	public Enemy[] secondStageEnemies;


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
		Enemy enemyToSpawn = null;
		if (level == 0)
		{
			enemyToSpawn = tutorialEnemy;
		}
		else if (1 <= level && level <= 4)
		{
			enemyToSpawn = firstStageEnemies[Random.Range(0, firstStageEnemies.Length)];
		}
		else
		{
			enemyToSpawn = secondStageEnemies[Random.Range(0, secondStageEnemies.Length)];
		}
		Enemy enemy = Instantiate(enemyToSpawn);
		RegisterOnBoard(enemy, enemy.startPosition);
		enemies.Add(enemy);
	}

	public void SpawnPlayer()
	{
		Player playerInstance = Instantiate(playerPrefab);
		RegisterOnBoard(playerInstance, playerInstance.startPosition);
	}
}
