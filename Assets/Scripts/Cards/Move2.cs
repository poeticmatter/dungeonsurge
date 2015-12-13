using UnityEngine;
using System.Collections;

public class Move2 : Card {

	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		if (GameManager.instance.level == 0)
		{
			player.CheckMove(2);
		}
		else
		{
			player.CheckMove(2 * player.facing);
		}
	}
}
