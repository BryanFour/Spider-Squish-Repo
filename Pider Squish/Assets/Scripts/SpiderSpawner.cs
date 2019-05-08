using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//	Spawning enemys - https://unity3d.com/learn/tutorials/projects/survival-shooter/more-enemies

public class SpiderSpawner : MonoBehaviour
{ 
	public GameObject[] spiders;
	public Transform[] spawnPoints;
	private float spawnRate = 1.5f;

	private void Start()
	{
		StartCoroutine(SpawnSpiders());
		StartCoroutine(DecreaseSpawnRate());
	}

	IEnumerator DecreaseSpawnRate()
	{
		if(spawnRate > 0.5f && LevelManager.Instance.countDownHasFinished == true)
		{
			yield return new WaitForSecondsRealtime(15);
			spawnRate = spawnRate - 0.25f;
			StartCoroutine(DecreaseSpawnRate());
		}
		else if (LevelManager.Instance.countDownHasFinished == false)
		{
			yield return new WaitForSecondsRealtime(0.5f);
			StartCoroutine(DecreaseSpawnRate());
		}
	}

	IEnumerator SpawnSpiders()
	{
		if(LevelManager.Instance.countDownHasFinished == true)
		{
			// Choose a random spider.
			int randomSpider = Random.Range(0, spiders.Length);
			// Find a random index between zero and one less than the number of spawn points.
			int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
			//
			Instantiate(spiders[randomSpider], spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
			//
			yield return new WaitForSecondsRealtime(spawnRate);
			//
			StartCoroutine(SpawnSpiders());
		}
		else if (LevelManager.Instance.countDownHasFinished == false)
		{
			yield return new WaitForSecondsRealtime(1);
			StartCoroutine(SpawnSpiders());
		}
			
	}
}
