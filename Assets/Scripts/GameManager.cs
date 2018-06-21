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

	void Start () {
		GameObject testTower = Instantiate(tower, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
		towers.Add(testTower.transform);
		GameObject testEnemy = Instantiate(enemy, new Vector3(0f, 100f, 0f), Quaternion.identity) as GameObject;
		enemies.Add(testEnemy.transform);
	}
}
