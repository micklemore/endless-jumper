using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
	[SerializeField]
    Transform obstacleSpawnPoint;

	[SerializeField]
	Obstacle obstacle;

	float currentTimer = 0;

	float timeToSpawn;

	void Start()
	{
		timeToSpawn = Random.Range(GameManager.instance.MinSpawnTimer, GameManager.instance.MaxSpawnTimer);
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
			timeToSpawn = Random.Range(GameManager.instance.MinSpawnTimer, GameManager.instance.MaxSpawnTimer);
			currentTimer = 0;
		}
    }

	void SpawnObstacle()
	{
		Instantiate(obstacle, obstacleSpawnPoint.transform.position, Quaternion.identity);
	}
}
