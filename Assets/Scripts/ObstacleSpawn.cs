using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
	[SerializeField]
    GameObject obstacleSpawnPoint;

	[SerializeField]
	GameObject obstacle;

	[SerializeField]
    float minSpawnTimer = 2f;

	[SerializeField]
	float maxSpawnTimer = 5f;

	float currentTimer = 0;

	float timeToSpawn;

	void Start()
	{
		timeToSpawn = Random.Range(minSpawnTimer, maxSpawnTimer);
	}

	void Update()
	{
		CheckSpawnTimer();
	}

	void CheckSpawnTimer()
	{
		currentTimer += Time.deltaTime;

        if (currentTimer >= timeToSpawn)
        {
			SpawnObstacle();
			timeToSpawn = Random.Range(minSpawnTimer, maxSpawnTimer);
			currentTimer = 0;
		}
    }

	void SpawnObstacle()
	{
		Debug.Log("Spawned obstacle after " + timeToSpawn + " seconds");
		Instantiate(obstacle, obstacleSpawnPoint.transform.position, Quaternion.identity);
	}

}
