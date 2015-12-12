using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {

	public string title;
	public string description;
	public int xp;
	public int stages;

	[HideInInspector] public bool playing = false;

	public int[] inputs;
	public string[] inputMessages;
	

	public void Play()
	{
		playing = true;
		StartCoroutine(PlayInternal());
		
	}

	IEnumerator PlayInternal()
	{
		for (int i = 0; i < inputs.Length; i++)
		{
			GameManager.instance.uiManager.DisplayInput(inputMessages[i]);
			while (!GameManager.instance.inputManager.HasInput())
			{
				yield return null;
			}
			inputs[i] = GameManager.instance.inputManager.InputValue;
			GameManager.instance.uiManager.ClearInput();
		}
		Player player = FindObjectOfType<Player>();
        for (int i = 0; i < stages; i++)
		{
			ExecuteStage(player, i);
			yield return new WaitForSeconds(0.1f);
		}

		playing = false;
	}

	protected virtual void ExecuteStage(Player player, int stage)
	{
		
	}

	
}
