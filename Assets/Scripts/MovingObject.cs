using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {

	[HideInInspector]
	public bool moving;
	[HideInInspector]
	public int boardPosition;
	[HideInInspector]
	public int facing;
	public int spriteFacing = -1;
	public MovingObject target = null;
	BoardManager boardManager;
	public float moveTime = 0.1f;

	private float inverseMoveTime;
	private Rigidbody2D rb2D;

	protected virtual void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
		inverseMoveTime = 1f / moveTime;
	}

	protected virtual void Start()
	{
		CheckFacting();
	}
	
	public void CheckMove(int distance)
	{
		GameManager.instance.boardManager.CheckMove(this, distance);
	}

	public IEnumerator SmoothMovement(Vector3 end, int distance)
	{
		moving = true;
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		while (sqrRemainingDistance > float.Epsilon)
		{
			Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
			rb2D.MovePosition(newPostion);

			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			yield return null;
		}
		moving = false;
		CheckFacting();
	}

	private void CheckFacting()
	{
		facing = boardPosition > target.boardPosition ? -1 : 1;
		transform.localScale = new Vector3(facing * spriteFacing, 1, 1);
	}

	public virtual void TakeDamage(int damage)
	{
		GameManager.instance.uiManager.FloatText("-" + damage, transform.position);
	}

}
