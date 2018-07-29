using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	public float bulletSpawnWaitTime;

	private Timer bulletSpawnTimer = null;
	private List<Collider2D> enemiesInRange = new List<Collider2D>();
	private Collider2D target;

	void Start () {
		bulletSpawnTimer = new Timer(true);
	}

	void Update() {
		if(!GameManager.instance.gameActive)
			return;
		if(shouldSpawnBullet()) {
			if(target != null)
				spawnBullet();
			else
				bulletSpawnTimer.stop();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		// target = other;
		if(other.tag == "Enemy") {
			enemiesInRange.Add(other);
			if(target == null) {
				target = other;
				if(!bulletSpawnTimer.isActive()) {
					bulletSpawnTimer.start(bulletSpawnWaitTime);
					spawnBullet();
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Enemy") {
			if(enemiesInRange.Contains(other)) {
				enemiesInRange.Remove(other);
				if(target == other) {
					if(enemiesInRange.Count > 0) {
						target = enemiesInRange[0];
					} else {
						target = null;
					}
				}
			}
		}
	}

	private void spawnBullet() {
		GameManager.instance.spawnBullet(transform.position);
	}

	private bool shouldSpawnBullet() {
		return bulletSpawnTimer.isComplete();
	}

	private float getTime() {
		return Time.time * 1000;
	}
}
