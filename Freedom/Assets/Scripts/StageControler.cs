using UnityEngine;
using System.Collections.Generic;

public class StageControler : MonoBehaviour {

	public GameObject Stage0Break, Stage1Break, Stage2Break, breakerOtherSide1, breakerOtherSide2, breakerOtherSide3;
	public GameObject cameraObject;

	public List<GameObject> enemyList;

	public float spawnTimer;
	public float spawnRate;
	public int Stage0EmemyCount, Stage1EmemyCount, Stage2EmemyCount;
	public Vector3 Stage0Spawn1, Stage0Spawn2, Stage1Spawn1, Stage1Spawn2, Stage2Spawn1, Stage2Spawn2;
	public int Left2Kill;

	public int stage;

	void spawnEnemy()
	{
		Debug.Log ("spawn");
		foreach (GameObject enemy in enemyList) {

			int i = Random.Range (1, 3);

			if (enemy.activeSelf == false) {

				//spawn it
				switch (stage) {

				case 0:
					if (i == 1) {
						enemy.transform.position = Stage0Spawn1;
					} else if (i == 2) {
						enemy.transform.position = Stage0Spawn2;
					}
					enemy.SetActive (true);
					break;

				case 1: if (i == 1) {
						enemy.transform.position = Stage1Spawn1;
				} else if (i == 2) {
					enemy.transform.position = Stage1Spawn2;
				}
					enemy.SetActive (true);
					break;

				case 2: if (i == 1) {
						enemy.transform.position = Stage2Spawn1;
				} else if (i == 2){
					enemy.transform.position = Stage2Spawn2;
				}
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
		Debug.Log(Left2Kill);
		//up the freedom bar
		GetComponent<GameManager>().UpHigh();
		print ("kilL");
	}



	public void BatKill()
	{
		Left2Kill -= 1;
		Debug.Log(Left2Kill);
		print ("kilL");
	}



	// Use this for initialization
	void Start () {
		Left2Kill = Stage0EmemyCount;
		spawnTimer = 100f;
		spawnRate = 50f;
		Stage0Spawn1 = new Vector3 (-14.1f, -2f, 0f);
		Stage0Spawn2 = new Vector3 (16.2f, -2f, 0f);
		Stage1Spawn1 = new Vector3 (11f, -2f, 0f);
		Stage1Spawn2 = new Vector3 (41.6f, -2f, 0f);
		Stage2Spawn1 = new Vector3 (37.9f, -2f, 0f);
		Stage2Spawn2 = new Vector3 (67.7f, -2f, 0f);
		stage = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (cameraObject.GetComponent<CameraControler> ().stageStart == true) {
			

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
			default:
				break;

			}

			//if all are killed
			if (Left2Kill <= 0) {
				//change the stage
				cameraObject.GetComponent<CameraControler> ().locked = false;
				cameraObject.GetComponent<CameraControler> ().stageStart = false;
				switch (stage) {

				case 0:
					Left2Kill = Stage1EmemyCount;
					stage = 1;
					spawnTimer = 100f;
					breakerOtherSide1.GetComponent<BoxCollider2D> ().isTrigger = true;
					break;

				case 1:
					Left2Kill = Stage2EmemyCount;
					stage = 2;
					spawnTimer = 100f;
					breakerOtherSide2.GetComponent<BoxCollider2D> ().isTrigger = true;
					break;
			
				}

			}
		}
	}
}
