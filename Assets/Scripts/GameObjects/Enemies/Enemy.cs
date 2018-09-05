using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	public int health;
	public int goldPerKill;
	public int damage;

	private Rigidbody2D rigidBody;
	private Vector2 target;
	private int currentTargetIndex = 0;
	private Vector2 direction;
	private Vector2 lastPosition;

	[HideInInspector] public float distanceTraveled;

	void Start () {
		distanceTraveled = 0;
		lastPosition = transform.position;
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
		if(!GameManager.instance.gameActive)
			return;
		move();
		distanceTraveled += Vector2.Distance(transform.position, lastPosition);
		lastPosition = transform.position;
	}

	public void doDamage(int amount) {
		health = health - amount;
		if(health <= 0) {
			GameManager.instance.incrementGold(goldPerKill);
			GameManager.instance.enemies.Remove(gameObject);
			Destroy(gameObject, 0.0f);
		}
	}

	private void move() {
		Vector2 newDirection = getDirection();
		if(dominantDirection(newDirection) != dominantDirection(direction)) {
			checkIfLastTile();
			getNewTarget();
		}
		rigidBody.velocity = direction.normalized * speed;
	}

	private void checkIfLastTile() {
		if(GameManager.instance.boardManager.enemyEndPosition == target) {
			GameManager.instance.takeDamage(damage);
			GameManager.instance.enemies.Remove(gameObject);
			Destroy(gameObject, 0.0f);
		}
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
