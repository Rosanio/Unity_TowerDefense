using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : BaseProjectile {

    protected override void Update() {
        base.Update();
        if(target != null) {
			calculateVelocity();
		}
    }

    protected override void handleEnemyCollision(Enemy enemy) {
        enemy.doDamage(damage);
        GameManager.instance.bullets.Remove(gameObject);
        Destroy(gameObject, 0.1f);
    }
}
