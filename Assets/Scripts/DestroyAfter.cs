using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField]
    float destroyAfter = 2f;

    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}
