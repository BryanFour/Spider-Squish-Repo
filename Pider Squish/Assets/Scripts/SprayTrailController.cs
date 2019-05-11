using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayTrailController : MonoBehaviour
{
	public Transform nozzlePosition;


    void Start()
    {
		//gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

		transform.position = nozzlePosition.position;
    }
}
