using UnityEngine;
using System.Collections;

public class RetreatAndGuard : Card {


	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		int enemyPos = player.target.boardPosition;
		if (enemyPos > player.boardPosition)
		{
			player.CheckMove(-1);
		}
		else
		{
			player.CheckMove(1);
		}
		player.dodge = true;
	}
}
