using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed;
	public int damage;

	private Rigidbody2D rigidBody;
	private Transform target = null;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		FindTarget();
	}

	private void FindTarget() {
		float minDistance = -1.0f;
		for(int i = 0; i < GameManager.instance.enemies.Count; i++) {
			Vector2 enemyPosition = GameManager.instance.enemies[i].position;
			float distance = Vector2.Distance(transform.position, enemyPosition);
			if(minDistance < 0 || distance < minDistance) {
				minDistance = distance;
				target = GameManager.instance.enemies[i];
			}
		}
	}

	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		//Calculate new trajectory
		float xDiff = target.position.x - position.x;
		Vector2 newVelocity = new Vector2((target.position.x - position.x), (target.position.y - position.y));
		newVelocity.Normalize();

		//Move
		rigidBody.velocity = newVelocity * speed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
			Debug.Log("Enemy hit");
			Destroy(gameObject, 0.1f);
		}
	}
}
