using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject bullet;
	public GameObject enemy;

	[HideInInspector] public List<Transform> bullets;
	[HideInInspector] public List<Transform> enemies;

	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		GameObject testEnemy = Instantiate(enemy, new Vector3(10f, 10f, 0f), Quaternion.identity) as GameObject;
		enemies.Add(testEnemy.transform);
		GameObject testEnemy2 = Instantiate(enemy, new Vector3(5f, 10f, 0f), Quaternion.identity) as GameObject;
		enemies.Add(testEnemy2.transform);
		GameObject testBullet = Instantiate(bullet, new Vector3(-5f, 0f, 0f), Quaternion.identity) as GameObject;
		bullets.Add(testBullet.transform);
	}

	// Update is called once per frame
	void Update () {

	}
}
