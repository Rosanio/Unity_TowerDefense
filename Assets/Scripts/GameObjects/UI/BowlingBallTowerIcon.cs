﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallTowerIcon : BaseUIElement {

	// Use this for initialization
	protected override void Start () {

	}

	public override void handleClick() {
		GrassTile tile = GameManager.instance.getSelectedTile();
		tile.trySpawningTower("bowlingball");
		GameManager.instance.closeMenu();
	}
}
