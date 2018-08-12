using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : Clickable {
  [HideInInspector] public float x;
	[HideInInspector] public float y;
	[HideInInspector] public string type;

  public override void handleClick() {
    if(GameManager.instance.isMenuOpen()) {
      GameManager.instance.closeMenu();
    }
  }


}
