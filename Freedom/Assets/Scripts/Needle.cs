using UnityEngine;
using System.Collections;

public class Needle : MonoBehaviour {

    void Update()
    {
        if (transform.eulerAngles.z > 300)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 1);
        }

        if (transform.eulerAngles.z < 200 && transform.eulerAngles.z > 90)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 1);

        }
        else if (transform.eulerAngles.z < 220)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 1);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
