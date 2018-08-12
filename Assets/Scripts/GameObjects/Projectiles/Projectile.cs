using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public int damage;

	private Rigidbody2D rigidBody;
	[HideInInspector] public Transform target;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update () {
		if(!GameManager.instance.gameActive)
			return;
		if(target != null) {
			Vector3 position = transform.position;
			Vector2 newVelocity = new Vector2((target.position.x - position.x),
											(target.position.y - position.y));
			newVelocity.Normalize();

			//Move
			rigidBody.velocity = newVelocity * speed;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
			GameManager.instance.bullets.Remove(gameObject);
			Destroy(gameObject, 0.1f);
		}
	}
}
