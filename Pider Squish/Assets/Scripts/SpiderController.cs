using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
	public float speed = 1;
	private Vector3 targetPosition;

	void Start()
    {
		//	Create a vector3 (Where the lady bug will be.)
		targetPosition = new Vector3(0, 0, 0);
		//	Have the spider face the lady bug/Target Position on creation.
		transform.LookAt(targetPosition);
	}

	void Update()
	{
		//	----- Move the spider a step closer to the lady bug/Target Position.
		// Calculate the distance to move/step.
		float step = speed * Time.deltaTime;
		// Move towards the lady bug/Target Position.
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, step );
		
		//	Game Over Stuff.
		//	If a spider reaches the target position.
		if(transform.position == targetPosition)
		{
			//	Run the game over method in the level manager.
			LevelManager.Instance.GameOver();
		}
	}

	// Destroy with mouse click --DEBUG Input--
	private void OnMouseDown()
	{
		Destroy(gameObject);
	}
}
