using System.Collections;
using UnityEngine;
using Pathfinding;

public class Walker : Agent
{
    protected AIPath aiPath;
    protected AIDestinationSetter aiDestination;
    protected GameObject player;
    private int walkerHP = 3;
    private int walkerKnockBackStrength = 10;
    private float walkerHitStun = .2f;
    protected Rigidbody2D collidingRBCached;
    protected Vector2 collisionLocationCached;

    private GameObject homeDestination;

    public override void Initialize()
    {
        base.Initialize();
        Debug.Log("Initialize called on Walker");
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        aiPath = GetComponent<AIPath>();
        aiDestination = GetComponent<AIDestinationSetter>();
        player = GameObject.FindWithTag("Player");
        homeDestination = GameObject.FindWithTag("Home");
        aiDestination.target = player.transform;
        knockBackStrength = walkerKnockBackStrength;
        hitStun = walkerHitStun;
        hp = walkerHP;
        SubscribeGameEvents();
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.gameObject.tag == "Weapon")
        {
            if (coroutineStarted) return;
            coroutineStarted = true;
            collidingRBCached = collision.attachedRigidbody;
            collisionLocationCached = new Vector2(collidingRBCached.position.x, collidingRBCached.position.y);
            //DebugLogWalkerVars();
            StartCoroutine(StartTakeDamage(collisionLocationCached, aiPath));
        }
    }
    protected override void Drops()
    {
        base.Drops();
        Instantiate(xpGem, currentPos, Quaternion.identity);
    }    

    private void LoseTarget()
    {
        aiDestination.target = homeDestination.transform;
    }

    private void Chugging()
    {
        walkerKnockBackStrength *= 2;
    }    
    private void Booking()
    {
        walkerKnockBackStrength *= 4;
    }

    private void SubscribeGameEvents()
    {
        GameEvents.current.onDie += LoseTarget;
        GameEvents.current.onChugging += Chugging;
        GameEvents.current.onBooking += Booking;
    }

    private void OnDestroy()
    {
        GameEvents.current.onDie -= LoseTarget;
        GameEvents.current.onChugging -= Chugging;
        GameEvents.current.onBooking -= Booking;
    }

    private void DebugLogWalkerVars()
    {
        Debug.Log(" collision location = " + collisionLocationCached);
    }
}
