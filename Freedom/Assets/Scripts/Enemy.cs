using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour
{


    [SerializeField]
    GameManager GM;
    public StageControler stage;
    Rigidbody2D rb2D;
    [SerializeField]
    float hitForce, moveSpeed;
    public GameObject target;
    private float attackTime;
    public bool beenHit, knockBack, beenKilled;
    float elapsedTime;

    void Start()
    {
        beenHit = false;
        knockBack = false;
        rb2D = GetComponent<Rigidbody2D>();
        //target = GetComponent<>();

    }

    void Update()
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
            if (beenKilled)
            {
                Debug.Log("killed loop");
                if (elapsedTime >= 3f)
                {
                    Debug.Log("Change Layer");
                    knockBack = false;
                    beenKilled = false;
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                    Debug.Log("Setting to false");
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (elapsedTime >= 5f)
                {
                    Debug.Log("wrong if");
                    knockBack = false;
                    gameObject.layer = LayerMask.NameToLayer("Enemy");
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Needle")
        {
            GM.AmountInBar.fillAmount += 0.2f;
            stage.NeedleKill();
            Debug.Log("needle kill");
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        if (coll.gameObject.tag == "Bat")
        {
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
                Debug.Log("bat kill");
                //Destroy(gameObject);
                stage.BatKill();
                beenKilled = true;
                gameObject.layer = LayerMask.NameToLayer("EnemyKill");
            }
        }
        if (coll.gameObject.tag == "Player")
        {
            //Debug.Log("Attack Player");
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
                GM.DownHigh();
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