using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool cardPlaying = false;
	void Start () {
	
	}
	
	void Update () {
		if (!GameManager.instance.playerTurn || cardPlaying)
		{
			return;
		}
		cardPlaying = true;
		StartCoroutine(WaitForInput());

	}

	IEnumerator WaitForInput()
	{
		while(!GameManager.instance.inputManager.HasInput())
		{
			yield return null;
		}
		int input = GameManager.instance.inputManager.InputValue;
		Card played = GameManager.instance.cardManager.Play(input);
		while(played.playing)
		{
			yield return null;
		}
		GameManager.instance.cardManager.Draw();
        GameManager.instance.playerTurn = false;
		cardPlaying = false;
	}
}
