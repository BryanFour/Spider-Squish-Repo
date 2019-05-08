using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpraySpawner : MonoBehaviour
{
	public int spawnRate = 2;
	public GameObject Spray;
	public Transform[] spawnPoints;
	
    void Start()
    {	//	The +4 is to account for the count down timer witch is 4 seconds long.
		InvokeRepeating("Spawn", spawnRate + 4, spawnRate);
    }

	private void Spawn()
	{
		if(LevelManager.Instance.countDownHasFinished == false && LevelManager.Instance.gameOver == false)
		{
			return;
		}
		//	Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		//	Instanciate a spray can at a random spawn points position.
		Instantiate(Spray, spawnPoints[spawnPointIndex].position, Spray.transform.rotation);
	}
}
