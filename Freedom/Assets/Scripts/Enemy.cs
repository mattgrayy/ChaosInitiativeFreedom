using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{


    [SerializeField]
    GameManager GM;
    public StageControler stage;
    Rigidbody2D rb2D;
    [SerializeField]
    public float hitForce, moveSpeed;
    public GameObject target;
    private float attackTime;
    public bool beenHit, knockBack, beenKilled;
    float elapsedTime;

    int highness = 0;
    public Animator myAnimator;
    public SpriteRenderer myRenderer;

	public AudioSource playerHit;
	public AudioSource EnemyHit;

    [SerializeField]
    RuntimeAnimatorController highController;
    [SerializeField]
    RuntimeAnimatorController midController;
    [SerializeField]
    RuntimeAnimatorController normController;





    void Start()
    {
        beenHit = false;
        knockBack = false;
        rb2D = GetComponent<Rigidbody2D>();
        //target = GetComponent<>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!knockBack)
        {
            if (target.transform.position.x < transform.position.x)
            {
                rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
                myRenderer.flipX = true;
            }
            else if (target.transform.position.x > transform.position.x)
            {
                rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
                myRenderer.flipX = false;
            }
        }
        else if (knockBack)
        {
            elapsedTime = elapsedTime + Time.deltaTime;
            if (beenKilled)
            {
                if (elapsedTime >= 3f)
                {
                    knockBack = false;
                    beenKilled = false;
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (elapsedTime >= 5f)
                {
                    knockBack = false;
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                }
            }
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
        if (coll.gameObject.tag == "Needle")
        {
            GM.AmountInBar.fillAmount += 0.1f;
            stage.NeedleKill();
            //Destroy(gameObject);
            moveSpeed = 10;
            gameObject.SetActive(false);
			EnemyHit.Play ();
        }
        if (coll.gameObject.tag == "Bat")
        {
			EnemyHit.Play ();
            StartCoroutine("ColorFlash");
            rb2D.AddForce(Vector2.up * hitForce, ForceMode2D.Impulse);
            knockBack = true;
            elapsedTime = 0;
            if (target.transform.position.x < transform.position.x)
            { rb2D.AddForce(Vector2.right * hitForce, ForceMode2D.Impulse); }
            else if (target.transform.position.x > transform.position.x)
            { rb2D.AddForce(Vector2.left * hitForce, ForceMode2D.Impulse); }

            if (!beenHit)
            {
                beenHit = true;
                gameObject.layer = LayerMask.NameToLayer("EnemyHit");
            }
            else if (beenHit)
            {
                stage.BatKill();
                beenKilled = true;
                moveSpeed = 10;
                gameObject.layer = LayerMask.NameToLayer("EnemyKill");
            }
        }
        if (coll.gameObject.tag == "Player")
        {
            moveSpeed = 0;
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
                if (highness == 2)
                {
                    myAnimator.Play("enemy_normal_hitting");
                }
                GM.DownHigh();
                attackTime = 0f;
				coll.gameObject.GetComponent<Player> ().gethit ();
				playerHit.Play ();
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            moveSpeed = 10;
        }
    }


    public void setStageControler(StageControler cont)
    {

        stage = cont;



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