using UnityEngine;
using System.Collections;

public class Invulnerable : Enemy {

	public string invulnerableMessage;

	private void CheckReachedEnd()
	{
		if (target.boardPosition >= GameManager.instance.boardManager.Length() - 1)
		{
			GameManager.instance.NextLevel();
		}
	}

	public override void ChooseAction()
	{
		CheckReachedEnd();
		base.ChooseAction();
	}

	public override void TakeDamage(int damage)
	{
		GameManager.instance.uiManager.DisplayMessage(invulnerableMessage);
	}
}
