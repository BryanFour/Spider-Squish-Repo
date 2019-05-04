using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Follow Mouse - https://www.youtube.com/watch?v=7OJQ6MbHuvQ

public class SprayController : MonoBehaviour
{
	private float actualDistance;
	private bool canActive = false;

    void Start()
    {
		// Disable the can on runtime
		gameObject.SetActive(false);

		Vector3 toObjectVevtor = transform.position - Camera.main.transform.position;
		Vector3 linearDistanceVector = Vector3.Project(toObjectVevtor, Camera.main.transform.forward);
		actualDistance = linearDistanceVector.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = actualDistance;
		transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

	public void ActivateCan()
	{
		//	If the can is "True"
		if (canActive == true)
		{
			// Deactivate the can
			gameObject.SetActive(false);
			Debug.Log("can deactivated");
		}
		else if(!canActive)
		{
			// Activate the can
			gameObject.SetActive(true);
		}
	}
}
