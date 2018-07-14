using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WaveManager : MonoBehaviour {
  
  private Waves levelWaves;
  private Wave currentWave;
  private int currentWaveIndex;
  private int nextEnemyIndex;
  
  private float waveStartTime;
  private float currentTime;
  
  private bool isWaveActive;
  
  public void init() {
    string path = Application.dataPath + "/LevelData/level1Waves.json";
    if(File.Exists(path)) {
      string waveString = File.ReadAllText(path);
      levelWaves = JsonUtility.FromJson<Waves>(waveString);
      currentWaveIndex = 0;
    } else {
      throw new FileNotFoundException("Wave file not found");
    }
  }
  
  public void spawnNextWave() {
    isWaveActive = true;
    currentWave = levelWaves.waves[currentWaveIndex];
    currentWaveIndex++;
    waveStartTime = getTime();
    nextEnemyIndex = 0;
  }
  
  void Update() {
    if(isWaveActive) {
      currentTime = getTime();
      if((currentTime - waveStartTime) > currentWave.wave[nextEnemyIndex].startTime) {
        GameManager.instance.spawnEnemy(GameManager.instance.boardManager.enemyStartPosition);
        nextEnemyIndex++;
        if(nextEnemyIndex >= currentWave.wave.Count) {
          isWaveActive = false;
        }
      }
    }
  }
  
  private float getTime() {
		return Time.time * 1000;
	}
}