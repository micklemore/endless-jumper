using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    [SerializeField]
    float speed = 15;

	void Update()
	{
		MoveObstacle();
	}
	void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void MoveObstacle()
    {
        gameObject.transform.position -= Vector3.right * speed * Time.deltaTime;
    }
}
