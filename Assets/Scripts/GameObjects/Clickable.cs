using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour {

	protected virtual void Start () {

	}

	public abstract void handleClick();
}
