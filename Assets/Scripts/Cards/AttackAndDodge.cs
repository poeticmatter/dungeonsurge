using UnityEngine;
using System.Collections;

public class AttackAndDodge : Card {
	public int range;
	public int damage;
	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		if (stage == 0)
		{
			if ( Mathf.Abs(player.boardPosition - player.target.boardPosition) <= range) {
				player.target.TakeDamage(damage);
			}
		}
		else
		{
			player.CheckMove(1 * inputs[0]);
		}

	}
}
