using UnityEngine;
using System.Collections;

public class BullRush : Card {
	public int range;
	public int damage;
	private int direction;
	private bool skipStage;
	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		if (stage == 0)
		{
			int distanceToEnemy = Mathf.Abs(player.boardPosition - player.target.boardPosition);
			if (distanceToEnemy <= 1)
			{
				skipStage = true;
			}
			else
			{
				skipStage = false;
				int enemyPos = player.target.boardPosition;
				direction = enemyPos > player.boardPosition ? 1 : -1;
				if (distanceToEnemy <= 2)
				{
					player.CheckMove(1 * direction);
				}
				else
				{
					player.CheckMove(2 * direction);
				}
			}
		}
        else if (!skipStage)
		{
			int distanceToEnemy = Mathf.Abs(player.boardPosition - player.target.boardPosition);
			if (distanceToEnemy <= range)
			{
				player.target.TakeDamage(damage);
				player.target.CheckMove(1*direction);
			}
		}
	}
}
