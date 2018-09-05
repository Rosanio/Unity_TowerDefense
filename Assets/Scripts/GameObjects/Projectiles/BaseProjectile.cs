using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour {

	public float speed;
	public int damage;

	protected Rigidbody2D rigidBody;
	[HideInInspector] public Transform target;

	protected virtual void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	protected virtual void Update () {
		if(!GameManager.instance.gameActive)
			return;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
			handleEnemyCollision(other.GetComponent<Enemy>());
		}
	}

	protected void calculateVelocity() {
		Vector3 position = transform.position;
		Vector2 velocity = new Vector2((target.position.x - position.x),
										(target.position.y - position.y));
		velocity.Normalize();

		//Move
		rigidBody.velocity = velocity * speed;
	}

	protected abstract void handleEnemyCollision(Enemy enemy);
}
