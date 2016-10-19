using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	// Update is called once per frame
	void Update (){
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * 5, GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,250));
        }
	}
}
