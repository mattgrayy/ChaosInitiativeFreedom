using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [SerializeField] float liftForce;
    Rigidbody2D rb2D;

    [SerializeField] Transform needle;
    [SerializeField] Transform bat;

    bool hasNeedle = true, onGround = false;

    Animator myAnimator;
    SpriteRenderer myRenderer;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update ()
    {
        float horizAxis = Input.GetAxis("Horizontal");

        if (horizAxis != 0)
        {
            if (horizAxis > 0)
            {
                myRenderer.flipX = false;
            }
            else
            {
                myRenderer.flipX = true;
            }

            // switch for different types (normal, medium, high)
            if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz norm walk test") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz_norm_hitting"))
            {
                myAnimator.Play("Gaz norm walk test");
            }

            rb2D.velocity = new Vector2(horizAxis * 8, rb2D.velocity.y);
        }
        else
        {
            // switch for different types (normal, medium, high)
            if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz_norm_bob") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz_norm_hitting"))
            {
                myAnimator.Play("Gaz_norm_bob");
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
            //if (hit.collider != null) // needs to include enemy and some other objects?
            //{
            //    if (hit.point.y - transform.position.y > -0.8f)
            //    {
            if (onGround)
            {
                rb2D.AddForce(Vector3.up * liftForce);
            }
            //    }
            //}
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouseScreenPosition.x > transform.position.x)
            {
                myRenderer.flipX = false;
                Instantiate(bat, transform.position, bat.rotation);
            }
            else
            {
                myRenderer.flipX = true;
                Instantiate(bat, transform.position, Quaternion.Euler(new Vector3(0,0,180)));
            }
            myAnimator.Play("Gaz_norm_hitting");
        }

        if (Input.GetMouseButtonDown(1) && hasNeedle)
        {
            myAnimator.Play("Gaz_norm_hitting");
            // throw drugs
            Transform clone = Instantiate(needle, transform.position, transform.rotation) as Transform;
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseScreenPosition - (Vector2)clone.transform.position);

            // set vector of transform directly
            clone.transform.right = direction;
            clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * 500);
            hasNeedle = false;
        }
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Pickup")
        {
            hasNeedle = true;
            Destroy(coll.gameObject);
        }
        if (coll.transform.tag == "Ground")
        {
            onGround = true;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            onGround = false;
        }
    }
}
