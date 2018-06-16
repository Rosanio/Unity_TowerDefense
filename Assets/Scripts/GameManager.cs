using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject bullet;
	public GameObject enemy;
	private bool isBulletOnScreen = false;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		GameObject testBullet = Instantiate(bullet, new Vector3(-5f, 0f, 0f), Quaternion.identity) as GameObject;
		GameObject testEnemy = Instantiate(enemy, new Vector3(10f, 10f, 0f), Quaternion.identity) as GameObject;
	}

	// Update is called once per frame
	void Update () {

	}
}
