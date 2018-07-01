using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : BaseTile {

	private bool hasTower = false;

	// Use this for initialization
	protected override void Start () {

	}

	// Update is called once per frame
	protected override void Update () {
		checkForClick();
	}

	private void checkForClick() {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D[] hits = Physics2D.RaycastAll (mousePosition, new Vector2(0, 0), 0.01f);
			foreach(RaycastHit2D hit in hits) {
				if(hit.transform.position == transform.position) {
					handleClick();
				}
			}
		}
	}

	private void handleClick() {
		if(!hasTower) {
			Debug.Log("Spawning tower");
			GameManager.instance.spawnTower(transform.position);
			hasTower = true;
		}
	}
}
