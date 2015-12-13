using UnityEngine;
using System.Collections;

public class AdvanceAndAttack : Card {
	public int range;
	public int damage;

	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		if (stage == 0)
		{
			player.CheckMove(1 * player.facing);
		}
		else
		{
			if (Mathf.Abs(player.boardPosition - player.target.boardPosition) <= range)
			{
				player.target.TakeDamage(damage);
			}
		}

	}
}
