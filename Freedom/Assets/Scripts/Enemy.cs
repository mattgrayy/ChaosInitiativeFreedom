using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {



	public StageControler stage;
    Rigidbody2D rb2D;
    [SerializeField] float hitForce, moveSpeed;
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
            { rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y); }
            else if (target.transform.position.x > transform.position.x)
            { rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y); }
        }
        else if (knockBack)
        {
            elapsedTime = elapsedTime + Time.deltaTime;
            if (elapsedTime >= 5f)
            {
                knockBack = false;
                gameObject.layer = LayerMask.NameToLayer("Enemy");
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
                rb2D.AddForce(Vector2.up * hitForce, ForceMode2D.Impulse);
                knockBack = true;
                if (target.transform.position.x < transform.position.x)
                { rb2D.AddForce(Vector2.right * hitForce, ForceMode2D.Impulse); }
                else if (target.transform.position.x > transform.position.x)
                { rb2D.AddForce(Vector2.left * hitForce, ForceMode2D.Impulse); }
                gameObject.layer = LayerMask.NameToLayer("EnemyHit");

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
            //Debug.Log("Attack Player");
            moveSpeed = 0;
            //transform.GetComponent<GameManager>().DownHigh();
            attackTime = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            moveSpeed = 0;
            attackTime = attackTime + Time.deltaTime;
            if (attackTime >= 0.5f)
            {
                //transform.GetComponent<GameManager>().DownHigh();
                attackTime = 0f;
                Debug.Log("Attack after 1 second");
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            moveSpeed = 1;
        }
    }


	public void setStageControler(StageControler cont)
	{

		stage = cont;



	}


}
