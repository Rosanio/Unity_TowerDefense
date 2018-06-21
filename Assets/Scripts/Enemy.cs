using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Transform transform;
	private Rigidbody2D rigidBody;

	void Start () {
		transform = GetComponent<Transform>();
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		move();
	}

	private void move() {
		rigidBody.velocity = new Vector2(0, 1);
	}
}
