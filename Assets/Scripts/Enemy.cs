using UnityEngine;
using System.Collections;

public class Enemy : MovingObject {

	public int startPosition = 8;

	public virtual void ChooseAction()
	{
		if (Mathf.Abs(target.boardPosition - boardPosition) == 1)
		{
			CheckMove(-facing);
		}
		else
		{
			CheckMove(facing);
		}
	}

	protected override void Start()
	{
		target = FindObjectOfType<Player>();
		base.Start();
	}
}
