using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse {

	private static Dictionary<string, int> tagPriority;

	public static void initialize() {
		tagPriority = new Dictionary<string, int>();
		tagPriority[""] = 0;
		tagPriority["Enemy"] = 1;
		tagPriority["Tower"] = 2;
		tagPriority["Tile"] = 3;
		tagPriority["UI"] = 4;
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
			handleObjectClick(clickedObject);
		}
	}

	private static void handleObjectClick(RaycastHit2D clickedObject) {
		Clickable clickable = clickedObject.collider.GetComponent<Clickable>();
		if(clickable != null) {
			clickable.handleClick();
		}
	}

	private static bool wasClick() {
		if(Input.GetMouseButtonDown(0)) {
			return true;
		}
		return false;
	}
}
