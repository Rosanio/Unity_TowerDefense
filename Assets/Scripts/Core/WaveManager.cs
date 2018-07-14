using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WaveManager : MonoBehaviour {
  
  private Waves levelWaves;
  private Wave currentWave;
  private int nextWaveIndex;
  private int nextEnemyIndex;
  
  private float gameStartTime;
  private float waveStartTime;
  private float currentTime;
  
  private bool isWaveActive;
  private bool allWavesSpawned;
  
  public void init() {
    allWavesSpawned = false;
    string path = Application.dataPath + "/LevelData/level1Waves.json";
    if(File.Exists(path)) {
      string waveString = File.ReadAllText(path);
      levelWaves = JsonUtility.FromJson<Waves>(waveString);
      nextWaveIndex = 0;
      gameStartTime = getTime();
      spawnNextWave();
    } else {
      throw new FileNotFoundException("Wave file not found");
    }
  }
  
  private void spawnNextWave() {
    isWaveActive = true;
    currentWave = levelWaves.waves[nextWaveIndex];
    nextWaveIndex++;
    if(nextWaveIndex >= levelWaves.waves.Count) {
      allWavesSpawned = true;
    }
    waveStartTime = getTime();
    nextEnemyIndex = 0;
  }
  
  void Update() {
    currentTime = getTime();
    checkForEnemySpawn();
    checkForWaveSpawn();
  }
  
  private void checkForEnemySpawn() {
    if(isWaveActive) {
      if((currentTime - waveStartTime) > currentWave.wave[nextEnemyIndex].startTime) {
        GameManager.instance.spawnEnemy(GameManager.instance.boardManager.enemyStartPosition);
        nextEnemyIndex++;
        if(nextEnemyIndex >= currentWave.wave.Count) {
          isWaveActive = false;
        }
      }
    }
  }
  
  private void checkForWaveSpawn() {
    if(!allWavesSpawned) {
      if((currentTime - gameStartTime) > levelWaves.waves[nextWaveIndex].startTime) {
        spawnNextWave();
      }
    }
  }
  
  private float getTime() {
		return Time.time * 1000;
	}
}