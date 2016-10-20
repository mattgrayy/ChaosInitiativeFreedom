using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {

	void Start ()
    {
        StartCoroutine("killSelf");
	}

    IEnumerator killSelf()
    {
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Hit!");
    }
}
