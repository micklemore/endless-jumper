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

    void MoveObstacle()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
