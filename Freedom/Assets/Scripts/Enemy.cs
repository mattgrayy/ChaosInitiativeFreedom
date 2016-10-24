using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {



	public StageControler stage;

    Rigidbody2D rb2D;
    public Vector2 moveSpeed, hitForce;
    public GameObject target;
    private float attackTime;
    public bool beenHit, knockBack;
    float elapsedTime;

	void Start ()
    {
        beenHit = false;
        knockBack = false;
        rb2D = GetComponent<Rigidbody2D>();
        //target = GetComponent<>();

    }
	
	void Update ()
    {
        if (!knockBack)
        {
            if (target.transform.position.x < transform.position.x)
            { rb2D.MovePosition(rb2D.position - moveSpeed * Time.fixedDeltaTime); }
            else if (target.transform.position.x > transform.position.x)
            { rb2D.MovePosition(rb2D.position + moveSpeed * Time.fixedDeltaTime); }
        }
        else if (knockBack)
        {
            elapsedTime = elapsedTime + Time.deltaTime;
            if (elapsedTime >= 5f)
            {
                knockBack = false;
            }
        }

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Needle")
        {
			stage.NeedleKill ();
			Debug.Log("needle kill");
            //Destroy(gameObject);
			gameObject.SetActive(false);
        }
        if (coll.gameObject.tag == "Bat")
        {
            if(!beenHit)
            {
                beenHit = true;
                rb2D.AddForce(hitForce, ForceMode2D.Impulse);
                knockBack = true;
            }
            else if (beenHit)
            {
				Debug.Log("bat kill");
                //Destroy(gameObject);
				stage.BatKill ();
				gameObject.SetActive(false);
            }
            

        }
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Attack Player");
            //attackTime = Time.time;
        }
    }

    void OncollisionDuration2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //if (Time.time - attackTime < 1)
            //{
            //    Debug.Log("Attack Player");
            //}

        }
    }


	public void setStageControler(StageControler cont)
	{

		stage = cont;



	}


}
