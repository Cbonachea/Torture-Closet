using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private Rigidbody2D gemRB;
    //private int xpTimer = 100;
    //private int xpLifeTime;
    //private CapsuleCollider2D xpCollider;

    private void Start()
    {
        //xpCollider = GetComponent<CapsuleCollider2D>();
        gemRB = GetComponent<Rigidbody2D>();
        gemRB.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)),ForceMode2D.Impulse);
        gemRB.AddTorque(Random.Range(-1000f,1000f));
    }

   /* void FixedUpdate()
    {
        if (xpLifeTime < xpTimer)
        {
            xpLifeTime += 1;
        }
        else
        { 
            xpCollider.enabled = true;
        }
    }
   */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
        GameEvents.current.GainXP();
        Destroy(gameObject);
        }
    }

}
