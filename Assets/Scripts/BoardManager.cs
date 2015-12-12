using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject [] boardSprites;
	private Vector3[] boardPositions;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void GenerateBoard(int length)
	{
		int x = -length / 2;
		boardPositions = new Vector3[length];
		for (int i = 0; i < length; i++)
		{
			boardPositions[i] = new Vector3(x, 0, 0);
			AddSpriteAtPosition(boardPositions[i]);
			x++;
		}
	}

	private void AddSpriteAtPosition(Vector3 position)
	{
		GameObject sprite = boardSprites[Random.Range(0, boardSprites.Length)];
		Instantiate(sprite, position, Quaternion.identity);
	}
}
