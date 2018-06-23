using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardManager : MonoBehaviour {

	public static BoardManager instance = null;
	public GameObject grassTile;
	public GameObject pathTile;

	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		loadScene();
	}

	private void loadScene() {
		string path = Application.dataPath + "/LevelData/level1.json";
		if(File.Exists(path)) {
			string levelString = File.ReadAllText(path);
			BoardLayout boardLayout = JsonUtility.FromJson<BoardLayout>(levelString);
			foreach (Tile tile in boardLayout.layout) {
				Vector2 position = new Vector2(tile.x, tile.y);
				instantiateTile(position, tile.type);
			}
		}
	}

	private void instantiateTile(Vector2 position, string type) {
		switch(type) {
			case "grass":
				Instantiate(grassTile, position, Quaternion.identity);
				break;
			case "path":
				Instantiate(pathTile, position, Quaternion.identity);
				break;
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
