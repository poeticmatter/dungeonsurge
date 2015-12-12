using UnityEngine;
using System.Collections;

public class Enemy : MovingObject {

	public int startPosition = 8;
	public EnemyAction[] actions;
	public int[] actionWeights;
	private int hp;

	override protected void Awake()
	{
		base.Awake();
		actionWeights = new int[actions.Length];
		for (int i = 0; i < actionWeights.Length; i++)
		{
			actionWeights[i] = 1;
		}
	}

	public virtual void ChooseAction()
	{
		int weightTotal = CalculateWeightTotal();
		int randomActionWeight = Random.Range(0, weightTotal);
		int weightCounted = 0;
		int actionIndex = 0;
		while (randomActionWeight > weightCounted + actionWeights[actionIndex])
		{
			weightCounted += actionWeights[actionIndex++];
		}
		actions[actionIndex].ExecuteAction(this, (Player)target);
	}

	private int CalculateWeightTotal()
	{
		int weightTotal = 0;
		for (int i = 0; i < actionWeights.Length; i++)
		{
			weightTotal += actionWeights[i];
		}
		return weightTotal;
	}

	protected override void Start()
	{
		target = FindObjectOfType<Player>();
		base.Start();
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage(damage);
		hp -= damage;
		if (hp<=0)
		{
			GameManager.instance.NextLevel();
		}
	}
}
