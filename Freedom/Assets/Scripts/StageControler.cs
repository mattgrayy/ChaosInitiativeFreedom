using UnityEngine;
using System.Collections.Generic;

public class StageControler : MonoBehaviour {

	public GameObject Stage0Break, Stage1Break, Stage2Break;
	public GameObject enemy, cameraObject;

	public List<GameObject> enemyList;

	public float spawnTimer;
	public float spawnRate;
	public int Stage0EmemyCount, Stage1EmemyCount, Stage2EmemyCount;
	public Vector3 StageSpawn0, StageSpawn1, StageSpawn2;
	public int Left2Kill;

	public int stage;

	void spawnEnemy()
	{
		Debug.Log ("spawn");
		foreach (GameObject enemy in enemyList) {

			if (enemy.activeSelf == false) {

				//spawn it
				switch (stage) {

				case 0:
					enemy.transform.position = StageSpawn0;
					enemy.SetActive (true);
					break;

				case 1: enemy.transform.position = StageSpawn1;
					enemy.SetActive (true);
					break;

				case 2: enemy.transform.position = StageSpawn2;
					enemy.SetActive (true);
					break;

				}
				break;
			}
		}
	}



	public void NeedleKill()
	{
		Left2Kill -= 1;
		Debug.Log("into stagecontrler");
		//up the freedom bar
		transform.GetComponent<GameManager>().UpHigh();
		print ("kilL");
	}



	public void BatKill()
	{
		Left2Kill -= 1;
		print ("kilL");
	}



	// Use this for initialization
	void Start () {
		Stage0EmemyCount = 10;
		Left2Kill = 10;
		spawnTimer = 100f;
		spawnRate = 50f;
		StageSpawn0 = new Vector3 (17, -2, 0);
		StageSpawn1 = new Vector3 (17, -2, 0);
		StageSpawn2 = new Vector3 (17, -2, 0);
		Stage0EmemyCount = 10;
		Stage1EmemyCount = 15;
		Stage2EmemyCount = 20;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime * spawnRate;

			switch (stage) {

			case 0:
				if ((spawnTimer <= 0) && (Stage0EmemyCount != 0)) {
					//activate first deactive enemy in list
					spawnEnemy ();
					spawnTimer = 100;
					Stage0EmemyCount--;
				}
				break;

			case 1: 
				if ((spawnTimer <= 0) && (Stage1EmemyCount != 0)) {
					//activate first deactive enemy in list
					spawnEnemy ();
					spawnTimer = 100;
					Stage1EmemyCount--;
				}
				break;
			case 2:
				if ((spawnTimer <= 0) && (Stage2EmemyCount != 0)) {
					//activate first deactive enemy in list
					spawnEnemy ();
					spawnTimer = 100;
					Stage2EmemyCount--;
				}
				break;

		}

		//if all are killed
		if (Left2Kill <= 0) {
			//change the stage
			cameraObject.GetComponent<CameraControler> ().locked = false;

			switch (stage)
			{

			case 0:
				Left2Kill = Stage1EmemyCount;
				stage = 1;
				spawnTimer = 100f;
				break;

			case 1:
				Left2Kill = Stage2EmemyCount;
				stage = 2;
				spawnTimer = 100f;
				break;
			
			}

		}

	}
}
