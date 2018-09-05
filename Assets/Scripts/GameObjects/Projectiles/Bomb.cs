using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BaseProjectile {
    private List<Enemy> enemiesInRange = new List<Enemy>();
    private Vector3 targetPosition;
    private Vector2 targetVector;

    protected override void Start() {
        base.Start();
        calculateVelocity();
        targetPosition = target.position;
        targetVector = (targetPosition - transform.position);
    }

    protected override void Update() {
        base.Update();
        Vector2 newTargetVector = (targetPosition - transform.position);
        if(targetReached(newTargetVector)) {
            detonate();
        }
        targetVector = newTargetVector;
    }

    protected override void handleEnemyCollision(Enemy enemy) {
        enemiesInRange.Add(enemy);
    }

    private bool targetReached(Vector2 newTargetVector) {
        if(dominantDirection(targetVector) != dominantDirection(newTargetVector)) {
            return true;
        }
        return false;
    }

    private void detonate() {
        for(int i = 0; i < enemiesInRange.Count; i++) {
            //todo: might be a bug here, keep an eye on this
            Enemy enemy = enemiesInRange[i];
            enemy.doDamage(damage);
        }
    }

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Enemy") {
			if(enemiesInRange.Contains(other.GetComponent<Enemy>())) {
				enemiesInRange.Remove(other.GetComponent<Enemy>());
			}
		}
	}

    private Vector2 dominantDirection(Vector2 vector2) {
		if(vector2.x > vector2.y) {
			if(vector2.x > 0) {
				return Vector2.right;
			} else {
				return Vector2.left;
			}
		} else {
			if(vector2.y > 0) {
				return Vector2.up;
			} else {
				return Vector2.down;
			}
		}
	}
}
