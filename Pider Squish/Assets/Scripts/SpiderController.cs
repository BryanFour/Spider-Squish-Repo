using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
	private Vector3 targetPosition;
	//	Create a float to hold the LevelManagers gamePlayHasStarted value.
	private float gamePlayHasStarted;
	//	The float to be given the speed from the LevelManagers spider speed coroutine.
	private float spiderSpeed;
	//	Get access to the spider animation clip, Drop the spider prefab in here to get it.
	public Animation spiderAnim;
	//
	public GameObject bloodSplatter;
	

	void Start()
    {
		//	Get the gamePlayHasStarted value from the level managers GamePlayHasStarted.
		gamePlayHasStarted = LevelManager.Instance.gamePlayHasStarted;
		//	Create a vector3 (Where the lady bug will be.)
		targetPosition = new Vector3(0, 0, 0);
		//	Have the spider face the lady bug/Target Position on creation.
		transform.LookAt(targetPosition);
	}

	void Update()
	{
		if (LevelManager.Instance.countDownHasFinished == false)
		{
			return;
		}
		//	Get the spiders speed from the LevelManagers spider speed coroutine.
		spiderSpeed = LevelManager.Instance.spiderStartSpeed;
		//	Set the spiders animation clips speed to the same value as the spider move speed.
		spiderAnim["walk"].speed = spiderSpeed;
		//	----- Move the spider a step closer to the lady bug/Target Position.
		// Calculate the distance to move/step.
		float step = spiderSpeed * Time.deltaTime;
		// Move towards the lady bug/Target Position.
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, step );
		
		//	Game Over Stuff.
		//	If a spider reaches the target position.
		if(transform.position == targetPosition)
		{
			//	Stop the spray can sound if its playing.
			SoundManager.Instance.StopSpraySFX();
			//	Run the game over method in the level manager.
			LevelManager.Instance.GameOver();
		}
	}
	
	// Destroy with mouse click --DEBUG Input--
	private void OnMouseDown()
	{
		SoundManager.Instance.PlaySquishSFX();
		//	Instanciate the splatterFX
		Instantiate(bloodSplatter, gameObject.transform.position, bloodSplatter.transform.rotation);
		Destroy(gameObject);
	}
	
}
