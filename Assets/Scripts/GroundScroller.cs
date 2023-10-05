using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
	[SerializeField]
	float scrollSpeed;

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
		//material.mainTextureOffset = new Vector2(offset, 0);
	}
}
