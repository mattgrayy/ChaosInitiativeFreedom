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
                    enemy.GetComponent<Enemy>().moveSpeed = 10;
                    break;

				case 1: if (i == 1) {
						enemy.transform.position = Stage1Spawn1;
				} else if (i == 2) {
					enemy.transform.position = Stage1Spawn2;
				}
					enemy.SetActive (true);
                        enemy.GetComponent<Enemy>().moveSpeed = 10;
                        break;

				case 2: if (i == 1) {
						enemy.transform.position = Stage2Spawn1;
				} else if (i == 2){
					enemy.transform.position = Stage2Spawn2;
				}
					enemy.SetActive (true);
                        enemy.GetComponent<Enemy>().moveSpeed = 10;
                        break;

				}
				break;
			}
		}
	}

	public void NeedleKill()
	{
		Left2Kill -= 1;
		//up the freedom bar
		GetComponent<GameManager>().UpHigh();
	}



	public void BatKill()
	{
		Left2Kill -= 1;
	}

    public void changeHighness(int newHighness)
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<Enemy>().changeHighness(newHighness);
        }
    }


	// Use this for initialization
	void Start () {
		Left2Kill = Stage0EmemyCount;
		spawnTimer = 80f;
		spawnRate = 80f;
		Stage0Spawn1 = new Vector3 (-20f, -8.37f, 0f);
		Stage0Spawn2 = new Vector3 (15f, -8.37f, 0f);
		Stage1Spawn1 = new Vector3 (14f, -8.37f, 0f);
		Stage1Spawn2 = new Vector3 (53f, -8.37f, 0f);
		Stage2Spawn1 = new Vector3 (52f, -8.37f, 0f);
		Stage2Spawn2 = new Vector3 (89f, -8.37f, 0f);
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
                    spawnRate += 10;
					spawnTimer = 100f;
					breakerOtherSide1.GetComponent<BoxCollider2D> ().isTrigger = true;
					break;

				case 1:
					Left2Kill = Stage2EmemyCount;
					stage = 2;
                        spawnRate += 10;
                        spawnTimer = 100f;
					breakerOtherSide2.GetComponent<BoxCollider2D> ().isTrigger = true;
					break;
                case 2:
                        Destroy(Stage0Break);
                        Destroy(Stage1Break);
                        Destroy(Stage2Break);
                        Destroy(breakerOtherSide1);
                        Destroy(breakerOtherSide2);
                        Destroy(breakerOtherSide3);
                     break;
				}

			}
		}
	}
}
