using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScroller : MonoBehaviour
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
		ScrollMaterial();
	}

	void ScrollMaterial()
	{
		material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
