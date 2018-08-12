using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : BaseTower {

	public override Transform FindTarget() {
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

	public override void spawnProjectile() {
		GameManager.instance.spawnBomb(transform.position, target);
	}

}
