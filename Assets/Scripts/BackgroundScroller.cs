using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scrollSpeed;

    float offset;

    Material material;

	void Start()
	{
		material = GetComponent<Renderer>().material;
	}

	void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        
        material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
