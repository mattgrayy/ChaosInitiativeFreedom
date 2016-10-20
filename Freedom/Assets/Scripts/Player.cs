using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField] float liftForce;
    Rigidbody2D rb2D;

    [SerializeField] Transform needle;
    [SerializeField] Transform bat;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        rb2D.velocity = new Vector2(Input.GetAxis("Horizontal") * 5, rb2D.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
            if (hit.collider != null) // needs to include enemy and some other objects?
            {
                if (hit.point.y - transform.position.y > -0.8f)
                {
                    rb2D.AddForce(Vector3.up * liftForce);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mouseScreenPosition.x > transform.position.x)
            {
                Instantiate(bat, transform.position, bat.rotation);
            }
            else
            {
                Instantiate(bat, transform.position, Quaternion.Euler(new Vector3(0,0,180)));
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // throw drugs
            Transform clone = Instantiate(needle, transform.position, transform.rotation) as Transform;
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseScreenPosition - (Vector2)clone.transform.position);

            // set vector of transform directly
            clone.transform.right = direction;
            clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * 500);
        }
	}
}
