using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour 
{
	public GameObject gameManager;			//GameManager prefab to instantiate.
	
	
	void Awake ()
	{
		if (GameManager.instance == null)
		{
			Instantiate(gameManager);
		}
			
	}

	void Update()
	{
		if (!secondInput && Input.GetKeyUp(KeyCode.Escape))
		{
			GameManager.instance.uiManager.ShowSplash("Press Escape to quit\nEnter to Continue\nR to Restart\nM to mute");
			StartCoroutine(WaitForInput());
		}
	}
	private	bool enter = false;
	private bool escape = false;
	private bool r = false;
	private bool secondInput = false;

	IEnumerator WaitForInput()
	{
		secondInput = true;
		yield return new WaitForSeconds(0.1f);
		GameManager.instance.enabled = false;
		enter = false;
		r = false;
		escape = false;
		while (!(enter || escape || r))
		{
			enter = Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter);
			escape = Input.GetKeyUp(KeyCode.Escape);
			r = Input.GetKeyUp(KeyCode.R);
			yield return null;
		}
		secondInput = false;
		if (r)
		{
			GameManager.instance.enabled = true;
            GameManager.instance.RestartGame();

		}
		else if (enter)
		{
			GameManager.instance.enabled = true;
			GameManager.instance.uiManager.HideSplash();
		}
		else if (escape)
		{
			Application.Quit();
		}
		
	}
}
