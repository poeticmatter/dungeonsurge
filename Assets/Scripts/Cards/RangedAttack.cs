using UnityEngine;
using System.Collections;

public class RangedAttack : Card {

	public int minRange;
	public int maxRange;
	public int damage;
	protected override void ExecuteStage(Player player, int stage)
	{
		int distance = Mathf.Abs(player.boardPosition - player.target.boardPosition);
        if (distance >= minRange && distance <= maxRange)
		{
			GameManager.instance.uiManager.DisplayMessage("Ha! Got him");
			player.target.TakeDamage(damage);
		}

	}
}
