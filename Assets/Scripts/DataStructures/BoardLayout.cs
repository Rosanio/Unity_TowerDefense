using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardLayout {
  public List<Tile> layout;
  public List<int> path;
  public List<float> enemyStartPosition;
  public List<float> enemyEndPosition;
}
