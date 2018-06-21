using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public float bulletSpawnWaitTime;

	private float bulletSpawnStartTime = 0;
	private float bulletSpawnCurrentTime = 0;
	private Collider2D target;

	void Start (){

	}

	void Update() {
		if(target != null && shouldSpawnBullet()) {
			spawnBullet();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		// target = other;
		if(other.tag == "Enemy") {
			target = other;
		}
	}

	private void spawnBullet() {
		bulletSpawnStartTime = getTime();
		GameManager.instance.spawnBullet(transform.position);
	}

	private bool shouldSpawnBullet() {
		bulletSpawnCurrentTime = getTime();
		return (bulletSpawnCurrentTime - bulletSpawnStartTime) > bulletSpawnWaitTime;
	}

	private float getTime() {
		return Time.time * 1000;
	}
}
