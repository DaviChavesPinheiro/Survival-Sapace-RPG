using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    Material meterialFundo;
	Vector2 offset;
	PlayerController player;
    [SerializeField] float velocity;
	
	void Start () {
		meterialFundo = GetComponent<Renderer>().material;
		player = FindObjectOfType (typeof(PlayerController)) as PlayerController;
	}

	void FixedUpdate () {
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);

		offset += player.GetComponent<Rigidbody2D>().velocity * Time.deltaTime * 0.001f * velocity;
		meterialFundo.SetTextureOffset ("_MainTex", offset);
	}
}
