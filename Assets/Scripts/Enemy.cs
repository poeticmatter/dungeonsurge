using UnityEngine;
using System.Collections;

public class Enemy : MovingObject {

	public int startPosition = 8;

	public virtual void ChooseAction()
	{
		CheckMove(-1);
	}
}
