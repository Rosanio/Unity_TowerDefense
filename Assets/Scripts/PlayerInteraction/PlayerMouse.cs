using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse {

	private static Dictionary<string, int> tagPriority;

	public static void initialize() {
		tagPriority = new Dictionary<string, int>();
		tagPriority[""] = 0;
		tagPriority["Tower"] = 1;
		tagPriority["Tile"] = 2;
		tagPriority["UI"] = 3;
	}

	public static void checkForClick () {
		if(wasClick()) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D clickedObject = new RaycastHit2D();
			string topTag = "";
			RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0), 0.01f);
			foreach(RaycastHit2D hit in hits) {
				if(tagPriority[hit.collider.tag] > tagPriority[topTag]) {
					topTag = hit.collider.tag;
					clickedObject = hit;
				}
			}
			if(topTag == "Tile") {
				GrassTile grassTile = clickedObject.collider.GetComponent<GrassTile>();
				if(grassTile != null) {
					grassTile.handleClick();
				}
			} else if(topTag == "UI") {
				GrassTile tile = GameManager.instance.getSelectedTile();
				tile.trySpawningTower();
				GameManager.instance.closeMenu();
			}
		}
	}

	private static bool wasClick() {
		if(Input.GetMouseButtonDown(0)) {
			return true;
		}
		return false;
	}
}
