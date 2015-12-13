using UnityEngine;
using System.Collections;

public class Move2 : Card {

	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		int distanceToEnemy = Mathf.Abs(player.boardPosition - player.target.boardPosition);
		if (GameManager.instance.level == 0)
		{
			if (player.boardPosition < player.target.boardPosition && distanceToEnemy == 2)
			{
				player.CheckMove(2 * -player.facing);
			}
			else
			{
				player.CheckMove(2);
			}
		}
		else
		{
			if (distanceToEnemy == 2)
			{
				player.CheckMove(2 * -player.facing);
			}
			else if (player.boardPosition + 2 * player.facing >= GameManager.instance.boardManager.board.Length ||
				player.boardPosition + 2 * player.facing < 0)
			{
				player.CheckMove(2 * -player.facing);
			}
			else
			{
				player.CheckMove(2 * player.facing);
			}
		}
	}
}
