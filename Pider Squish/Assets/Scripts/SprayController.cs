using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Follow Mouse - https://www.youtube.com/watch?v=7OJQ6MbHuvQ

public class SprayController : MonoBehaviour
{
	//	Move With Mouse Stuff.
	private float actualDistance;
	private bool canActive = false;

    void Start()
    {
		// Disable the can on runtime
		gameObject.SetActive(false);    // Probally not needed.
		//	Move With Mouse Stuff.
		Vector3 toObjectVevtor = transform.position - Camera.main.transform.position;
		Vector3 linearDistanceVector = Vector3.Project(toObjectVevtor, Camera.main.transform.forward);
		actualDistance = linearDistanceVector.magnitude;
	}

	void Update()
    {	
		//	Move with Mouse Stuff.
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = actualDistance;
		transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public void ActivateCan()
	{	//	If the spray can is not active.
		if(!canActive && PlayerPrefs.GetInt("SprayCount") > 0)
		{
			// Activate the can
			gameObject.SetActive(true);
			//	Set the bool to true.
			canActive = true;
			//	PlayerPrefs Stuff.
			int sprayCount = PlayerPrefs.GetInt("SprayCount");
			sprayCount -= 1;
			PlayerPrefs.SetInt("SprayCount", sprayCount);
			LevelManager.Instance.UpdateSprayCount();
			//	PlayerPrefs Stuff End.
			StartCoroutine(SprayUseTime());
		}
	}

	IEnumerator SprayUseTime()
	{
		yield return new WaitForSecondsRealtime(5);
		//	Disable the can.
		gameObject.SetActive(false);
		//	Set the bool to false.
		canActive = false;
	}
}
