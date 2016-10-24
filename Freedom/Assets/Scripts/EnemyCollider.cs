using UnityEngine;
using System.Collections;

public class EnemyCollider : MonoBehaviour {
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.parent.GetComponent<Enemy>().knockBack == true)
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyHit");
        }
        else if (this.transform.parent.GetComponent<Enemy>().knockBack == false)
        {
            gameObject.layer = LayerMask.NameToLayer("EnemyCollider");
            
        }
    }   
}
