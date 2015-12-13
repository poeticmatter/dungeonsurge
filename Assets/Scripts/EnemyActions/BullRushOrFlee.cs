using UnityEngine;
using System.Collections;

public class BullRushOrFlee : EnemyAction {

	public override void ExecuteAction(Enemy enemy, Player player)
	{
		base.ExecuteAction(enemy, player);
		int distanceToPlayer = Mathf.Abs(player.boardPosition - enemy.boardPosition);

		if (distanceToPlayer == 3)
		{
			enemy.CheckMove(enemy.facing *2);
			enemy.MovePlayer(0.3f, enemy.facing * 1);
			player.TakeDamage(1);
		}
		else if (distanceToPlayer <= 2)
		{
			enemy.CheckMove(-enemy.facing*2);
		} else
		{
			enemy.CheckMove(enemy.facing * 1);
		}
	}

	
}
