using UnityEngine;
using System.Collections;

namespace Completed
{	
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
	}
}