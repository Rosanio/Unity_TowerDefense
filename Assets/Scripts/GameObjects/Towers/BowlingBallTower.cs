using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallTower : MonoBehaviour {

	public float bulletSpawnWaitTime;

	private Timer bulletSpawnTimer = null;
	private List<Collider2D> enemiesInRange = new List<Collider2D>();
	private Transform target;

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
				target = FindTarget();
				if(!bulletSpawnTimer.isActive()) {
					bulletSpawnTimer.start(bulletSpawnWaitTime);
					spawnBullet();
				}
			} else {
				target = FindTarget();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Enemy") {
			if(enemiesInRange.Contains(other)) {
				enemiesInRange.Remove(other);
				if(target == other.GetComponent<Transform>()) {
					if(enemiesInRange.Count > 0) {
						target = FindTarget();
					} else {
						target = null;
					}
				}
			}
		}
	}

	private Transform FindTarget() {
		float maxDistanceTraveled = -1;
		Transform newTarget = null;
		for(int i = 0; i < enemiesInRange.Count; i++) {
			if(enemiesInRange[i].GetComponent<Enemy>().distanceTraveled > maxDistanceTraveled) {
				maxDistanceTraveled = enemiesInRange[i].GetComponent<Enemy>().distanceTraveled;
				newTarget = enemiesInRange[i].GetComponent<Transform>();
			}
		}
		return newTarget;
	}

	private void spawnBullet() {
		GameManager.instance.spawnBullet(transform.position, target);
	}

	private bool shouldSpawnBullet() {
		return bulletSpawnTimer.isComplete();
	}

	private float getTime() {
		return Time.time * 1000;
	}
}
