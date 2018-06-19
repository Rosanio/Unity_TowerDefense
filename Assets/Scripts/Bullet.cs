using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;

	private Rigidbody2D rigidBody;
	private Transform target = null;
	private Vector2 position;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		position = GetComponent<Transform>().position;
		FindTarget();
	}

	private void FindTarget() {
		float minDistance = -1.0f;
		for(int i = 0; i < GameManager.instance.enemies.Count; i++) {
			Vector2 enemyPosition = GameManager.instance.enemies[i].position;
			float distance = Vector2.Distance(position, enemyPosition);
			if(minDistance < 0 || distance < minDistance) {
				minDistance = distance;
				target = GameManager.instance.enemies[i];
			}
		}
	}

	// Update is called once per frame
	void Update () {
		position = GetComponent<Transform>().position;
		//Calculate new trajectory
		Vector2 newVelocity = new Vector2((target.position.x - position.x), (target.position.y - position.y));
		newVelocity.Normalize();

		//Move
		rigidBody.velocity = newVelocity * speed;
	}
}
