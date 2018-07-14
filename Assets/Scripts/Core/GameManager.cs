using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public GameObject bullet;
	public GameObject enemy;
	public GameObject tower;
	public GameObject grassTile;
	public GameObject pathTile;

	private GameObject towerIcon;
	private bool menuOpen = false;
	private GrassTile selectedTile;
	private int gold;
	private Text goldText;

	[HideInInspector] public BoardManager boardManager;
	[HideInInspector] public WaveManager waveManager;
	[HideInInspector] public List<Transform> bullets;
	[HideInInspector] public List<Transform> enemies;
	[HideInInspector] public List<Transform> towers;

	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		PlayerMouse.initialize();
		loadScene();
		gold = 100;
		setupUI();
		spawnFirstWave();
	}

	private void loadScene() {
		boardManager = GetComponent<BoardManager>();
		boardManager.loadScene();
	}

	private void setupUI() {
		setupTowerIcon();
		updateGoldText();
	}

	private void setupTowerIcon() {
		towerIcon = GameObject.Find("TowerIcon");
		towerIcon.SetActive(false);
	}

	private void updateGoldText() {
		goldText = GameObject.Find("GoldText").GetComponent<Text>();
		goldText.text = "Gold: " + gold;
	}
	
	private void spawnFirstWave() {
		waveManager = GetComponent<WaveManager>();
		waveManager.init();
		waveManager.spawnNextWave();
	}

	void Update() {
		PlayerMouse.checkForClick();
	}

	public void spawnEnemy(Vector3 position) {
		GameObject testEnemy = Instantiate(enemy, position, Quaternion.identity)
		 												as GameObject;
		enemies.Add(testEnemy.transform);
	}

	public void spawnBullet(Vector3 position) {
		GameObject testBullet = Instantiate(bullet, position, Quaternion.identity)
															as GameObject;
		bullets.Add(testBullet.transform);
	}

	public void spawnTower(Vector3 position) {
		if(gold >= 40) {
			spendGold(40);
			GameObject testTower = Instantiate(tower, position, Quaternion.identity)
			as GameObject;
			towers.Add(testTower.transform);
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
}
