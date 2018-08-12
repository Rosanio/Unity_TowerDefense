using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTower: MonoBehaviour {
    public float bulletSpawnWaitTime;

	private Timer bulletSpawnTimer = null;
	protected List<Collider2D> enemiesInRange = new List<Collider2D>();
	protected Transform target;

	void Start () {
		bulletSpawnTimer = new Timer(true);
	}

	void Update() {
		if(!GameManager.instance.gameActive)
			return;
		if(shouldSpawnProjectile()) {
			if(target != null)
				spawnProjectile();
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
					spawnProjectile();
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

	private bool shouldSpawnProjectile() {
		return bulletSpawnTimer.isComplete();
	}

	private float getTime() {
		return Time.time * 1000;
	}

    public abstract Transform FindTarget();

	public abstract void spawnProjectile();
}
