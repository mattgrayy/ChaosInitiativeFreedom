using UnityEngine;
using System.Collections;

public class BreakerControler : MonoBehaviour {

	public StageControler controler;
	public GameObject cameraObject;
	public GameObject collider;

	private bool disabled = false;


	void OnTriggerExit2D(Collider2D con)
	{
		if (!disabled) {
			if (con.gameObject.tag == "Player") {

				collider.transform.GetComponent<BoxCollider2D> ().isTrigger = false;
				cameraObject.GetComponent<CameraControler> ().StartStage ();
				cameraObject.GetComponent<CameraControler> ().locked = true;
				disabled = true;
			}
		}

	}
}

