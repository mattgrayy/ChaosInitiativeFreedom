using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    Rigidbody2D rb2D;
    public Vector2 moveSpeed;
    public GameObject target, needle;
    private float attackTime;

	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //target = GetComponent<>();
    }
	
	void Update ()
    {
        if (target.transform.position.x < transform.position.x)
        {
            rb2D.MovePosition(rb2D.position - moveSpeed * Time.fixedDeltaTime); 
        }
        else if (target.transform.position.x > transform.position.x)
        { rb2D.MovePosition(rb2D.position + moveSpeed * Time.fixedDeltaTime); }

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "needle")
        {
            //change to saved sprite
        }
        if (coll.gameObject.tag == "player")
        {
            attackTime = Time.time;
        }
    }

    void OncollisionDuration2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "player")
        {
            if (Time.time - attackTime > 0.5)
            {
                Debug.Log("Attack Player");
            }

        }
    }
}
