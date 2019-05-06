using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpraySpawner : MonoBehaviour
{
	public int spawnRate = 2;
	public GameObject Spray;
	public Transform[] spawnPoints;
	
    void Start()
    {
		InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	private void Spawn()
	{
		//	Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		//	Instanciate a spray can at a random spawn points position.
		Instantiate(Spray, spawnPoints[spawnPointIndex].position, Spray.transform.rotation);
	}
}
