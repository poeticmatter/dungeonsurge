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
	protected MovingObject target = null;
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
		//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
		//Square magnitude is used instead of magnitude because it's computationally cheaper.
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		//While that distance is greater than a very small amount (Epsilon, almost zero):
		while (sqrRemainingDistance > float.Epsilon)
		{
			//Find a new position proportionally closer to the end, based on the moveTime
			Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
			//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
			rb2D.MovePosition(newPostion);

			//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;

			//Return and loop until sqrRemainingDistance is close enough to zero to end the function
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

}
