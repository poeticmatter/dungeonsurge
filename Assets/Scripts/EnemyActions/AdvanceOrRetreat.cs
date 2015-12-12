using UnityEngine;
using System.Collections;

public class AdvanceOrRetreat : EnemyAction {

	public override void ExecuteAction(Enemy enemy, Player player)
	{
		base.ExecuteAction(enemy, player);
		if (player.boardPosition == enemy.boardPosition-1)
		{
			player.TakeDamage(1);
			enemy.CheckMove(-enemy.facing);
		}
		else
		{
			enemy.CheckMove(enemy.facing);
		}
	}
}
