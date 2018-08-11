using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTowerIcon : BaseUIElement {

	// Use this for initialization
	protected override void Start () {

	}

	public override void handleClick() {
		GrassTile tile = GameManager.instance.getSelectedTile();
		tile.trySpawningTower("bomb");
		GameManager.instance.closeMenu();
	}
}
