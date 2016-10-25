using UnityEngine;
using System.Collections;

public class BFT : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Needle" || coll.gameObject.tag == "Bat")
        {
            StartCoroutine("ColorFlash");
        }
    }

    IEnumerator ColorFlash()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Renderer>().material.color = Color.white;
        yield return null;
    }
}
