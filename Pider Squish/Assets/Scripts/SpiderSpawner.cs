using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//	Spawning enemys - https://unity3d.com/learn/tutorials/projects/survival-shooter/more-enemies

public class SpiderSpawner : MonoBehaviour
{
	public GameObject[] spiders;
	public float spawnTime = 1;
	public Transform[] spawnPoints;

    void Start()
    {
		float randomSpawnTime = Random.Range(.5f, 2.5f);
		InvokeRepeating("Spawn", spawnTime, randomSpawnTime);
    }

    void Update()
    {
        
    }

	void Spawn()
	{
		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		// Choose a random spider.
		int randomSpider = Random.Range(0, spiders.Length);
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate(spiders[randomSpider], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}
