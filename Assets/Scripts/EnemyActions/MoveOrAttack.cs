using UnityEngine;
using System.Collections;

public class MoveOrAttack : EnemyAction {
	public int damage;
	public int maxAdvance;
	public int minAdvance;
	public int maxRetreat;
	public int minRetreat;
	public int minRange;
	public int maxRange;
	public override void ExecuteAction(Enemy enemy, Player player)
	{
		base.ExecuteAction(enemy, player);
		int distanceToPlayer = Mathf.Abs(player.boardPosition - enemy.boardPosition);
		Debug.Log(distanceToPlayer);
		if (distanceToPlayer > maxRange)
		{
			enemy.CheckMove(enemy.facing * Random.Range(minAdvance, maxAdvance + 1));
		}
		else if (distanceToPlayer < minRange)
		{
			enemy.CheckMove(enemy.facing * -1 * Random.Range(minRetreat, maxRetreat + 1));
		}
		else
		{
			player.TakeDamage(1);
		}
	}
}
