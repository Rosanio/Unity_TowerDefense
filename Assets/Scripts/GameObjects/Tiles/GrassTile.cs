using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : BaseTile {

	private bool hasTower = false;

	// Use this for initialization
	protected override void Start () {

	}

	public override void handleClick() {
		if(!hasTower && !GameManager.instance.isMenuOpen()) {
			GameManager.instance.openMenu(transform.position);
			GameManager.instance.setSelectedTile(this);
		} else {
			base.handleClick();
		}
	}

	public void trySpawningTower() {
		if(!hasTower) {
			GameManager.instance.spawnTower(transform.position);
			hasTower = true;
		}
	}
}
