using UnityEngine;
using System.Collections;

public class Move2 : Card {

	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		player.CheckMove(2 * inputs[0]);
	}
}
