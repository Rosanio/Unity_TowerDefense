using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardManager : MonoBehaviour {

	[HideInInspector] public BoardLayout boardLayout;
	[HideInInspector] public Vector2 enemyStartPosition;
	[HideInInspector] public Vector2 enemyEndPosition;
	[HideInInspector] public List<Tile> tiles;

	public void loadScene() {
		string path = Application.dataPath + "/LevelData/level1.json";
		if(File.Exists(path)) {
			string levelString = File.ReadAllText(path);
			boardLayout = JsonUtility.FromJson<BoardLayout>(levelString);
			tiles = boardLayout.layout;
			foreach (Tile tile in boardLayout.layout) {
				Vector2 position = new Vector2(tile.x, tile.y);
				instantiateTile(position, tile.type);
			}
			enemyStartPosition = new Vector2(boardLayout.enemyStartPosition[0],
																				boardLayout.enemyStartPosition[1]);
			enemyEndPosition = new Vector2(boardLayout.enemyEndPosition[0],
																				boardLayout.enemyEndPosition[1]);
		}
	}

	private void instantiateTile(Vector2 position, string type) {
		switch(type) {
			case "grass":
				Instantiate(GameManager.instance.grassTile, position, Quaternion.identity);
				break;
			case "path":
				Instantiate(GameManager.instance.pathTile, position, Quaternion.identity);
				break;
		}
	}

	public Vector2 getTargetTile(int currentTargetIndex) {
		if(boardLayout.path.Count > currentTargetIndex) {
			int pathIndex = boardLayout.path[currentTargetIndex];
			Tile targetTile = tiles[pathIndex];
			return new Vector2(targetTile.x, targetTile.y);
		} else {
			return new Vector2(enemyEndPosition[0],
														enemyEndPosition[1]);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
