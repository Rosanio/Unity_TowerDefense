using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour {

	protected virtual void Start () {

	}

	protected virtual void Update () {

	}

	public abstract void handleClick();
}
