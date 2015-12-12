using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
	private int _inputValue = 0;

	public int InputValue
	{
		get {
			int temp = _inputValue;
			_inputValue = 0;
			return temp; }
	}

	public bool HasInput()
	{
		return _inputValue == 1 || _inputValue == -1;
	}


	void Update () {
		
		if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetMouseButtonUp(0))
		{
			_inputValue = -1;
		}
		else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetMouseButtonUp(1))
		{
			_inputValue = 1;
		}
		else
		{
			_inputValue = 0;
		}
	}

}
