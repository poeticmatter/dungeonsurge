using UnityEngine;
using System.Collections;

public class PlaceTrap : Card {
	public Trap trapPrefab;
	private static Trap placed = null;
	protected override void ExecuteStage(Player player, int stage)
	{
		base.ExecuteStage(player, stage);
		int distanceToEnemy = Mathf.Abs(player.boardPosition - player.target.boardPosition);
		if (distanceToEnemy <= 1)
		{
			GameManager.instance.uiManager.DisplayMessage("Can't place trap, enemy too close");
		}
		else
		{
			Vector3 trapPos = player.transform.position + player.facing * Vector3.right;
            if (placed == null)
			{
				placed = (Trap)Instantiate(trapPrefab, trapPos, Quaternion.identity);
			}
			else
			{
				GameManager.instance.uiManager.DisplayMessage("Replacing previous trap");
				placed.transform.position = trapPos;
			}
			placed.boardPosition = player.boardPosition + player.facing;
			placed.enabled = true;
		}
	}
}
