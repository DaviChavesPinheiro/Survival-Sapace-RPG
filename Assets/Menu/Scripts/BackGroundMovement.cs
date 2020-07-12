using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    [SerializeField] Vector2 velocity;
    Material material;
	Vector2 offset;
	
	void Start () {
		material = GetComponent<Renderer>().material;
	}

	void FixedUpdate () {
		offset += Time.deltaTime * 0.001f * velocity;
		material.SetTextureOffset ("_MainTex", offset);
	}
}
