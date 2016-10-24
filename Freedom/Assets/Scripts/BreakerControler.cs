using UnityEngine;
using System.Collections;

public class BreakerControler : MonoBehaviour {

	public StageControler controler;
	public GameObject cameraObject;

	void OnTriggerExit2D(Collider2D con)
	{
		Debug.Log ("triffer leave");
		if (con.gameObject.tag == "Player") {

			transform.GetComponent<BoxCollider2D> ().isTrigger = false;

			cameraObject.GetComponent<CameraControler> ().locked = true;
			Debug.Log ("locked");
		}
	}


}

