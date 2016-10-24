using UnityEngine;
using System.Collections.Generic;

public class StageControler : MonoBehaviour {

	public GameObject Stage0Break, Stage1Break, Stage2Break;
	public GameObject enemy, cameraObject;

	public List<GameObject> enemyList;




	public float spawnTimer;
	public float spawnRate;
	public int Stage0EmemyCount, Stage1EmemyCount, Stage2EmemyCount;
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
					enemy.transform.position = new Vector3 (17, -2, 0);
					enemy.SetActive (true);

					break;

				case 1: 
					break;

				case 2: 
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
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime * spawnRate;

		if ((spawnTimer <= 0) && (Stage0EmemyCount!=0)) {

			//GameObject spawn = Instantiate (enemy, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			//spawn.GetComponent<Enemy> ().setStageControler (this);
			//enemyList.Add (spawn);

			//activate first deactive enemy in list
			spawnEnemy();

			spawnTimer = 100;
			Stage0EmemyCount--;

		}

		//if all are killed
		if (Left2Kill <= 0) {
			//chenge the stage
			stage = 1;
			cameraObject.GetComponent<CameraControler> ().locked = false;
			Left2Kill++;
		}

	}
}
