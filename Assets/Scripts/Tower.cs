using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	private CircleCollider2D collider;

	void Start () {
		collider = GetComponent<CircleCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
			spawnBullet();
		}
	}

	private void spawnBullet() {
		Instantiate(GameManager.instance.bullet,GetComponent<Transform>().position, Quaternion.identity);
	}
}
