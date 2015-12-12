using UnityEngine;
using System.Collections;

public class Invulnerable : Enemy {

	public string invulnerableMessage;
	public string cantGetOverMessage;
	public string getToTheEdge;

	private void CheckReachedEnd()
	{
		if (target.boardPosition >= GameManager.instance.boardManager.Length() - 1)
		{
			GameManager.instance.NextLevel();
		}
		if (boardPosition >= GameManager.instance.boardManager.Length() - 1)
		{
			GameManager.instance.uiManager.DisplayMessage(cantGetOverMessage);
		}
		if (target.boardPosition > boardPosition)
		{
			GameManager.instance.uiManager.DisplayMessage(getToTheEdge);
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
