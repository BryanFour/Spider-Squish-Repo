using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//	Destroy on touch - https://www.youtube.com/watch?v=SMxHtIKDfV4

public class DestroyManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
		//	If we are touching and not dragging.
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			if (Physics.Raycast(ray, out hit))
				if (hit.collider.gameObject.tag == "Spider")
				{
					Destroy(hit.transform.gameObject);
				}
		}
	}
}
