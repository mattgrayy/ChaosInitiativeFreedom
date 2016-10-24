using UnityEngine;
using System.Collections;

public class CameraControler : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	public GameObject Managers;

	public bool locked;
	public Vector3 Stage0Lock, Stage1Lock, Stage2Lock;
	public int stage;

	// Use this for initialization
	void Start () {
	
		offset = transform.position - player.transform.position + new Vector3 (0, 3f, 0);;
		Stage0Lock = new Vector3 (0.62f, -3f, -10);
		Stage1Lock = new Vector3 (24.47f, -3f, -10);
		Stage2Lock = new Vector3 (49.37f, -3f, -10);
		stage = 0;
		locked = true;
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
				transform.position = Vector3.Lerp (transform.position, Stage0Lock + offset, Time.deltaTime);
				break;

			case 1:
				transform.position = Vector3.Lerp (transform.position, Stage1Lock + offset, Time.deltaTime);
				break;

			case 2:
				transform.position = Vector3.Lerp (transform.position, Stage2Lock + offset, Time.deltaTime);
				break;
			}
		}


	}
}
