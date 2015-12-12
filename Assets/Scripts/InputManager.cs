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
		int x = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
		if (x < 0 || Input.GetMouseButtonUp(0))
		{
			_inputValue = -1;
		}
		else if (x > 0 || Input.GetMouseButtonUp(1))
		{
			_inputValue = 1;
		}
		else
		{
			_inputValue = 0;
		}
	}

}
