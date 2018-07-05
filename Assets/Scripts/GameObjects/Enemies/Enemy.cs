using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public int health;

	private Rigidbody2D rigidBody;
	private Vector2 target;
	private int currentTargetIndex = 0;
	private Vector2 direction;

	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		getNewTarget();
	}

	private void getNewTarget() {
		target = GameManager.instance.getEnemyTargetTile(currentTargetIndex);
		currentTargetIndex++;
		direction = getDirection();
	}

	private Vector2 getDirection() {
		Vector2 position = transform.position;
		return (target - position);
	}

	void Update() {
		move();
		if(health <= 0) {
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
		Vector2 newDirection = getDirection();
		if(dominantDirection(newDirection) != dominantDirection(direction)) {
			getNewTarget();
		}
		rigidBody.velocity = direction.normalized * speed;
	}

	private Vector2 dominantDirection(Vector2 vector2) {
		if(vector2.x > vector2.y) {
			if(vector2.x > 0) {
				return Vector2.right;
			} else {
				return Vector2.left;
			}
		} else {
			if(vector2.y > 0) {
				return Vector2.up;
			} else {
				return Vector2.down;
			}
		}
	}
}
