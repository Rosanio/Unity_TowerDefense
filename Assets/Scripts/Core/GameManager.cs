using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject bullet;
	public GameObject enemy;
	public GameObject tower;
	public GameObject grassTile;
	public GameObject pathTile;

	[HideInInspector] public BoardManager boardManager;
	[HideInInspector] public List<Transform> bullets;
	[HideInInspector] public List<Transform> enemies;
	[HideInInspector] public List<Transform> towers;

	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		boardManager = GetComponent<BoardManager>();
		boardManager.loadScene();
		spawnEnemy(boardManager.enemyStartPosition);
	}

	void Update() {

	}

	public void spawnEnemy(Vector3 position) {
		Debug.Log("Spawning enemy at " + position);
		GameObject testEnemy = Instantiate(enemy, position, Quaternion.identity)
		 												as GameObject;
		enemies.Add(testEnemy.transform);
	}

	public void spawnBullet(Vector3 position) {
		GameObject testBullet = Instantiate(bullet, position, Quaternion.identity)
															as GameObject;
		bullets.Add(testBullet.transform);
	}

	public void spawnTower(Vector3 position) {
		GameObject testTower = Instantiate(tower, position, Quaternion.identity)
														as GameObject;
		towers.Add(testTower.transform);
	}

	public Vector2 getEnemyTargetTile(int currentTargetIndex) {
		return boardManager.getEnemyTargetTile(currentTargetIndex);
	}
}
