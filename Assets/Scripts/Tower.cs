using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
			spawnBullet();
		}
	}

	private void spawnBullet() {
		Instantiate(GameManager.instance.bullet, transform.position, Quaternion.identity);
	}
}
