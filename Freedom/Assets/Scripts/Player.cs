using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField] float liftForce;
    Rigidbody2D rb2D;
    [SerializeField] Enemy enemy;
    [SerializeField] Transform needle;
    [SerializeField] Transform bat;

    bool hasNeedle = true, onGround = false;


    public int highness = 0;
    [SerializeField]
    RuntimeAnimatorController highController;
    [SerializeField]
    RuntimeAnimatorController midController;
    [SerializeField]
    RuntimeAnimatorController normController;

    Animator myAnimator;
    SpriteRenderer myRenderer;

    float needleCooldown = 0.0f;

	public void gethit()
		{

		StartCoroutine ("ColorFlash");


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

            switch (highness)
            {
                case 0:
                    // switch for different types (normal, medium, high)
                    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("gaz_high_walking") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("bat swing_high"))
                    {
                        myAnimator.Play("gaz_high_walking");
                    }
                    break;
                case 1:
                    // switch for different types (normal, medium, high)
                    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("gaz_mid_walking") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("gaz_mid_hit"))
                    {
                        myAnimator.Play("gaz_mid_walking");
                    }
                    break;
                case 2:
                     // switch for different types (normal, medium, high)
                    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz norm walk test 0") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz_Norm_Hitting"))
                    {
                        myAnimator.Play("Gaz norm walk test 0");
                    }
                    break;
                default:
                    break;
            }

            rb2D.velocity = new Vector2(horizAxis * 8, rb2D.velocity.y);
        }
        else
        {
            switch (highness)
            {
                case 0:
                    // switch for different types (normal, medium, high)
                    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("gaz_high_bob") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("bat swing_high"))
                    {
                        myAnimator.Play("gaz_high_bob");
                    }
                    break;
                case 1:
                    // switch for different types (normal, medium, high)
                    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("gaz_mid_bob") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("gaz_mid_hit"))
                    {
                        myAnimator.Play("gaz_mid_bob");
                    }
                    break;
                case 2:
                    // switch for different types (normal, medium, high)
                    if (!myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz_norm_bob") && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gaz_Norm_Hitting"))
                    {
                        myAnimator.Play("Gaz_norm_bob");
                    }
                    break;
                default:
                    break;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (onGround)
            {
                rb2D.AddForce(Vector3.up * liftForce);
            }
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
            switch (highness)
            {
                case 0:
                        myAnimator.Play("bat swing_high");
                    break;
                case 1:
                        myAnimator.Play("gaz_mid_hit");
                    break;
                case 2:
                        myAnimator.Play("Gaz_Norm_Hitting");
                    break;
                default:
                    break;
            }
        }

        if (Input.GetMouseButtonDown(1) && hasNeedle && needleCooldown > 0.5f)
        {
            // throw drugs
            Transform clone = Instantiate(needle, transform.position, transform.rotation) as Transform;
            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseScreenPosition - (Vector2)clone.transform.position);

            // set vector of transform directly
            clone.transform.right = direction;
            clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * 800);
            hasNeedle = false;
            /*
            switch (highness)
            {
                case 0:
                    myAnimator.Play("bat swing_high");
                    break;
                case 1:
                    myAnimator.Play("gaz_mid_hit");
                    break;
                case 2:
                    myAnimator.Play("gaz_norm_hit");
                    break;
                default:
                    break;
            }
            */
        }

        if (needleCooldown < 0.5f)
        {
            needleCooldown += Time.deltaTime;
        }

        if (enemy.playerHit == true)
        {
            StartCoroutine("ColorFlash");
            enemy.playerHit = false;
        }
	}

    public void changeHighness(int newHighness)
    {
        highness = newHighness;
        switch (highness)
        {
            case 0:
                myAnimator.runtimeAnimatorController = highController;
                break;
            case 1:
                myAnimator.runtimeAnimatorController = midController;
                break;
            case 2:
                myAnimator.runtimeAnimatorController = normController;
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Pickup")
        {
            hasNeedle = true;
            needleCooldown = 0.0f;
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
