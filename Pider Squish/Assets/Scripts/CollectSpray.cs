using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSpray : MonoBehaviour
{
	// Spray Count Stuff.
	private int sprayCount;
	//	
	public float fallSpeed = 5;

	void Start()
    {
		//	Collect Spray Can Stuff.
		sprayCount = PlayerPrefs.GetInt("SprayCount");
	}

    void Update()
    {
		//	Spray can fall stuff.
		transform.Translate(new Vector3(0, 0, -fallSpeed) * Time.deltaTime, Space.World);

		//	Collect Spray Can Stuff.
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			if (Physics.Raycast(ray, out hit))
				if (hit.collider.gameObject.tag == "Spray")
				{   //	Add 1 to the spray count.
					sprayCount += 1;
					//	Save the playerprefs
					PlayerPrefs.SetInt("SprayCount", sprayCount);
					//	Update the Spray Count Text.
					GameManager.Instance.UpdateSprayCount();
					//	Destroy the collected spray can.
					Destroy(hit.transform.gameObject);
				}
		}
	}

	// Collect then Destroy with mouse click --DEBUG Input--
	public void OnMouseDown()
	{
		//	Add 1 to the spray count.
		sprayCount += 1;
		//	Save the playerprefs
		PlayerPrefs.SetInt("SprayCount", sprayCount);
		//	Update the Spray Count Text.
		GameManager.Instance.UpdateSprayCount();
		//	Destroy the collected spray can.
		Destroy(gameObject);
	}

	
}