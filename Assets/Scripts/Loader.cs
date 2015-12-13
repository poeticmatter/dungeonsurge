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
		Cursor.visible = false;
			
	}

	void Update()
	{
		if (!secondInput && (Input.GetKeyUp(KeyCode.Escape) || GameManager.instance.gameOver))
		{
			if (GameManager.instance.gameOver)
			{
				GameManager.instance.uiManager.ShowSplash("Game Over, you survived to level " + GameManager.instance.level + "\nPress Escape to quit\nEnter to restart\nR to retry level");
			} else
			{
				GameManager.instance.uiManager.ShowSplash("Press Escape to quit\nEnter to Continue\nR to Restart");
			}
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
			if (GameManager.instance.gameOver)
			{
				GameManager.instance.RestartLevel();
			}
			else
			{
				GameManager.instance.RestartGame();
			}
			GameManager.instance.enabled = true;
			GameManager.instance.gameOver = false;

		}
		else if (enter)
		{
			if (GameManager.instance.gameOver)
			{
				GameManager.instance.RestartGame();
			}
			else
			{
				GameManager.instance.uiManager.HideSplash();
			}
			GameManager.instance.enabled = true;
			GameManager.instance.gameOver = false;
		}
		else if (escape)
		{
			Application.Quit();
		}
		
	}
}
