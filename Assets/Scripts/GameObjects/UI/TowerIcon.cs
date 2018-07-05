using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIcon : BaseUIElement {

	// Use this for initialization
	protected override void Start () {

	}

	// Update is called once per frame
	protected override void Update () {

	}

	public override void handleClick() {
		GrassTile tile = GameManager.instance.getSelectedTile();
		tile.trySpawningTower();
		GameManager.instance.closeMenu();
	}
}
