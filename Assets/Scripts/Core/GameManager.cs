using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject bullet;
	public GameObject enemy;
	public GameObject tower;
	public GameObject bombTower;
	public GameObject grassTile;
	public GameObject pathTile;

	private GameObject towerIcon;
	private bool menuOpen = false;
	private GrassTile selectedTile;
	private int gold;
	private Text goldText;
	private int health;
	private Text healthText;
	private GameObject gameOverImage;
	private GameObject startImage;

	[HideInInspector] public BoardManager boardManager;
	[HideInInspector] public Player player;
	[HideInInspector] public WaveManager waveManager;
	[HideInInspector] public List<GameObject> bullets;
	[HideInInspector] public List<GameObject> enemies;
	[HideInInspector] public List<GameObject> towers;
	[HideInInspector] public bool gameActive = false;

	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		initPlayerControls();
	}

	private void initPlayerControls() {
		player = GetComponent<Player>();
		player.init();
		PlayerMouse.initialize();
	}

	private void loadScene() {
		boardManager = GetComponent<BoardManager>();
		boardManager.loadScene();
	}

	private void initializePlayerResources() {
		gold = 100;
		health = 10;
	}

	private void setupUI() {
		setupStartImage();
		setupGameOverImage();
		updateGoldText();
		updateHealthText();
		setupTowerIcon();
	}

	private void setupTowerIcon() {
		if(towerIcon == null)
			towerIcon = GameObject.Find("TowerIcon");
		towerIcon.SetActive(false);
	}

	private void setupGameOverImage() {
		if(gameOverImage == null)
			gameOverImage = GameObject.Find("GameOverImage");
		gameOverImage.SetActive(false);
	}

	private void setupStartImage() {
		if(startImage == null)
			startImage = GameObject.Find("StartImage");
	}

	private void updateGoldText() {
		goldText = GameObject.Find("GoldText").GetComponent<Text>();
		goldText.text = "Gold: " + gold;
	}

	private void updateHealthText() {
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		healthText.text = "Health: " + health;
	}

	public void startLevel() {
		loadScene();
		initializePlayerResources();
		setupUI();
		gameActive = true;
		spawnFirstWave();
		hideStartScreen();
	}

	private void spawnFirstWave() {
		waveManager = GetComponent<WaveManager>();
		waveManager.init();
	}

	void Update() {
		PlayerMouse.checkForClick();
	}

	public void spawnEnemy(Vector3 position) {
		GameObject testEnemy = Instantiate(enemy, position, Quaternion.identity)
		 												as GameObject;
		enemies.Add(testEnemy);
	}

	public void spawnBullet(Vector3 position, Transform target) {
		GameObject newBullet = Instantiate(bullet, position, Quaternion.identity)
															as GameObject;
		newBullet.GetComponent<Projectile>().target = target;
		bullets.Add(newBullet);
	}

	public void spawnTower(Vector3 position, string type) {
		switch(type) {
			case "bowlingball":
				spawnBowlingBallTower(position);
				break;
			case "bomb":
				spawnBombTower(position);
				break;
		}
	}

	private void spawnBowlingBallTower(Vector3 position) {
		spawnTower(tower, 40, position);
	}

	private void spawnBombTower(Vector3 position) {
		spawnTower(bombTower, 60, position);
	}

	private void spawnTower(GameObject towerPrefab, int goldCost, Vector3 position) {
		if(gold >= goldCost) {
			spendGold(goldCost);
			GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity)
															as GameObject;
			towers.Add(newTower);
		}
	}

	public Vector2 getEnemyTargetTile(int currentTargetIndex) {
		return boardManager.getEnemyTargetTile(currentTargetIndex);
	}

	public void closeMenu() {
		towerIcon.SetActive(false);
		menuOpen = false;
	}

	public void openMenu(Vector2 centerPosition) {
		Vector2 iconPosition = centerPosition - new Vector2(1.5f, 0);
		towerIcon.transform.position = iconPosition;
		towerIcon.SetActive(true);
		menuOpen = true;
	}

	public bool isMenuOpen() {
		return menuOpen;
	}

	public void setSelectedTile(GrassTile tile) {
		selectedTile = tile;
	}

	public GrassTile getSelectedTile() {
		return selectedTile;
	}

	public void incrementGold(int amount) {
		gold += amount;
		updateGoldText();
	}

	public void spendGold(int amount) {
		gold -= amount;
		updateGoldText();
	}

	public void takeDamage(int damage) {
		health -= damage;
		if(health <= 0) {
			gameOver();
		} else {
			updateHealthText();
		}
	}

	private void gameOver() {
		gameActive = false;
		despawnAllGameObjects();
		hideActiveUIElements();
		showGameOverScreen();
	}

	private void despawnAllGameObjects() {
		uninstantiateGameObjects(enemies);
		uninstantiateGameObjects(bullets);
		uninstantiateGameObjects(towers);
		uninstantiateGameObjects(boardManager.tiles);
	}

	private void uninstantiateGameObjects(List<GameObject> gameObjects) {
		for(int i = 0; i < gameObjects.Count; i++) {
			Destroy(gameObjects[i]);
		}
		gameObjects.Clear();
	}

	private void hideActiveUIElements() {
		goldText.enabled = false;
		healthText.enabled = false;
	}

	private void showGameOverScreen() {
		gameOverImage.SetActive(true);
	}

	private void hideStartScreen() {
		Debug.Log("Hiding start screen");
		startImage.SetActive(false);
	}
}
