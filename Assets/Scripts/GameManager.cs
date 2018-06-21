using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject bullet;
	public GameObject enemy;
	public GameObject tower;

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
		spawnTower(new Vector3(-2.0f, 0.0f, 0.0f));
		spawnEnemy(new Vector3(0.0f, -15.0f, 0.0f));
	}

	void Update() {

	}

	public void spawnEnemy(Vector3 position) {
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
}
