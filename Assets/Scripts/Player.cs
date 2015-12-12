using UnityEngine;
using System.Collections;

public class Player : MovingObject {
	public int startPosition = 1;
	private bool cardPlaying = false;
	
	void Update () {
		if (!GameManager.instance.playerTurn || cardPlaying)
		{
			return;
		}
		cardPlaying = true;
		StartCoroutine(WaitForInput());

	}
	protected override void Start()
	{
		target = FindObjectOfType<Enemy>();
		base.Start();
	}

	IEnumerator WaitForInput()
	{
		GameManager.instance.uiManager.DisplayMessage("Pick a card to play");

		while (!GameManager.instance.inputManager.HasInput())
		{
			yield return null;
		}
		int input = GameManager.instance.inputManager.InputValue;
		if (input == -1)
		{
			input = 0;
		}
		Card played = GameManager.instance.cardManager.Play(input);
		while(played.playing)
		{
			yield return null;
		}
		CheckReachedEnd();
		GameManager.instance.cardManager.Draw();
        GameManager.instance.playerTurn = false;
		cardPlaying = false;
	}

	private void CheckReachedEnd()
	{
		if (boardPosition >= GameManager.instance.boardManager.Length()-1)
		{
			GameManager.instance.NextLevel();
		}
	}
}
