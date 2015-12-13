using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{

	public int boardPosition;

	void Update()
	{
		if (!GameManager.instance.playerTurn)
		{
			return;
		}
		for (int i = 0; i < GameManager.instance.boardManager.enemies.Count; i++)
		{
			Enemy enemy = GameManager.instance.boardManager.enemies[i];
            if (enemy.transform.position.x - transform.position.x < float.Epsilon)
			{
				enemy.TakeDamage(2);
				enabled = false;
			}
		}
	}
}
