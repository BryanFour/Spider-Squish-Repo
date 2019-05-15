using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpraySpawner : MonoBehaviour
{
	public int spawnRate = 2; //------------- probally not needed.
	public GameObject Spray;
	public Transform[] spawnPoints;

	//	Number of seconds between spray spawns
	private float spraySpawnInterval = 150;


	void Start()
    {   //	The +4 is to account for the count down timer witch is 4 seconds long.
		//InvokeRepeating("Spawn", spawnRate + 4, spawnRate);							----------- remove me

		StartCoroutine(TimeToSpawnChecker());
    }


	private void SpawnSpray()
	{
		//if(LevelManager.Instance.countDownHasFinished == false && LevelManager.Instance.gameOver == false)
		if(LevelManager.Instance.countDownHasFinished == false && Time.timeScale == 1)
		{
			return;
		}
		//	Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		
		//	Set the spray last spawned player pref value to 0
		PlayerPrefs.SetFloat("SprayLastSpawned", 0);
		
		//	Instanciate a spray can at a random spawn points position.
		Instantiate(Spray, spawnPoints[spawnPointIndex].position, Spray.transform.rotation);
	}

	IEnumerator TimeToSpawnChecker()
	{
		yield return new WaitForSecondsRealtime(1);
		if (PlayerPrefs.GetFloat("SprayLastSpawned", 0) >= spraySpawnInterval)
		{
			SpawnSpray();
			StartCoroutine(TimeToSpawnChecker());
		}
		else
		{
			StartCoroutine(TimeToSpawnChecker());
		}
	}
}
