using UnityEngine;
using System.Collections;

public class MoveOrAttack : EnemyAction {
	public int damage;
	public int maxMove;
	public int minMove;
	public int minRange;
	public int maxRange;
	public override void ExecuteAction(Enemy enemy, Player player)
	{
		base.ExecuteAction(enemy, player);
		int distanceToPlayer = Mathf.Abs(player.boardPosition - enemy.boardPosition);
		if (distanceToPlayer >=minRange && distanceToPlayer <= maxRange)
		{
			player.TakeDamage(1);
		}
		else
		{
			enemy.CheckMove(enemy.facing*Random.Range(minMove,maxMove+1));
		}
	}
}
