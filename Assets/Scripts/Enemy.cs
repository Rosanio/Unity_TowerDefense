using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Vector2 speed;
	public int health;

	private Rigidbody2D rigidBody;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		move();
		if(health <= 0) {
			Debug.Log("Boom!");
			Destroy(gameObject, 0.0f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Projectile") {
			Projectile projectile = other.gameObject.GetComponent<Projectile>();
			health = health - projectile.damage;
		}
	}

	private void move() {
		rigidBody.velocity = speed;
	}
}
