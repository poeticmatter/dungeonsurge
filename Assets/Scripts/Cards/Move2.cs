using UnityEngine;
using System.Collections;

public class Move2 : Card {

	protected override void ExecuteStage(int stage)
	{
		base.ExecuteStage(stage);
		FindObjectOfType<Player>().CheckMove(2 * inputs[0]);
	}
}
