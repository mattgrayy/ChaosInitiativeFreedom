using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	public GameObject Managers;

	public bool locked;
	public Vector3 Stage0Lock, Stage1Lock, Stage2Lock;
	public int stage;

	public bool stageStart;


	public void StartStage()
	{


		stageStart = true;


	}

	// Use this for initialization
	void Start () {
	
		offset = transform.position - player.transform.position + new Vector3 (0, 1f, 0);;
		Stage0Lock = new Vector3 (1f, 0.23f, -10);
		Stage1Lock = new Vector3 (26.7f, 0.23f, -10);
		Stage2Lock = new Vector3 (52.8f, 0.23f, -10);
		stage = 0;
		locked = false;
		stageStart = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		stage = Managers.GetComponent<StageControler> ().stage;



		if (locked == false) {
			
			//transform.position = player.transform.position + offset;
			transform.position = Vector3.Lerp (transform.position, player.transform.position + offset, Time.deltaTime);

		} else {

			switch (stage)
			{
			case 0:
				transform.position = Vector3.Lerp (transform.position, Stage0Lock, Time.deltaTime);
				break;

			case 1:
				transform.position = Vector3.Lerp (transform.position, Stage1Lock, Time.deltaTime);
				break;

			case 2:
				transform.position = Vector3.Lerp (transform.position, Stage2Lock, Time.deltaTime);
				break;
			}
		}


	}
}
