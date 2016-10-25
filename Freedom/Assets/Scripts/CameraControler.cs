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
		Stage0Lock = new Vector3 (-3.1f, 0.1f, -10);
		Stage1Lock = new Vector3 (34.26f, 0.1f, -10);
		Stage2Lock = new Vector3 (70.52f, 0.1f, -10);
		stage = 0;
		locked = true;
		stageStart = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		stage = Managers.GetComponent<StageControler> ().stage;



		if (locked == false) {
			
			//transform.position = player.transform.position + offset;
			transform.position = Vector3.Lerp (transform.position, new Vector3(player.transform.position.x, 0.1f, -10f) /* player.transform.position + offset*/, Time.deltaTime);

            if(transform.position.x < -3.1f)
            {
                transform.position = new Vector3(-3.1f, transform.position.y, transform.position.z);
            }
            if (transform.position.x > 70.52f)
            {
                transform.position = new Vector3(70.52f, transform.position.y, transform.position.z);
            }

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
