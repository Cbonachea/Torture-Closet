using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Agent
{
    private Camera mainCam;

    internal Vector3 playerPosition;
    private Vector3 mousePosition;
    private Vector3 moveForceDirection;
    protected Vector2 collisionLocationCached;

    private Rigidbody2D playerRB;
    protected Rigidbody2D collidingRBCached;

    private int playerHP = 5;
    private int playerKnockBackStrength = 30;

    private float moveForce;
    private float playerHitStun = .1f;
  //  private float chugging = 3f;
  //  private float booking = 5f;

    private bool isAccelerating;

  //  private SpriteRenderer playerSprite;


    public override void Initialize()
    {
        base.Initialize();
        hp = playerHP;
        hitStun = playerHitStun;
        knockBackStrength = playerKnockBackStrength;
        SubscribeControllerEvents();
        playerRB = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
   //     playerSprite = GetComponentInChildren<SpriteRenderer>();    
    }
    private void Update()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
    }
    private void FixedUpdate()
    {
        if (isAccelerating && canMove) Move();
    }
    public override void OnCollisionStay2D(Collision2D other)
    {
        base.OnCollisionStay2D(other);
        if (other.gameObject.tag == "enemy")
        if (coroutineStarted) return;
        //coroutineStarted = true;
        collidingRBCached = other.rigidbody;
        collisionLocationCached = new Vector2(collidingRBCached.position.x, collidingRBCached.position.y);
        StartCoroutine(StartTakeDamage(collisionLocationCached, null));
        DebugLogKnockBackVars();
    }
    private void OnMove()
    {
        isAccelerating = true;
        moveForceDirection = (mousePosition - transform.position).normalized;
        moveForce = moveForceDirection.magnitude;
    }
    private void OnMove_Idle()
    {
        isAccelerating = false;
    }

    private void Move()
    {
        playerRB.AddForce(moveForceDirection * moveForce, ForceMode2D.Impulse);
/*        if (playerRB.velocity.magnitude >= booking) GameEvents.current.Booking();
        if (playerRB.velocity.magnitude <= booking) GameEvents.current.Booking_Idle();
        if (playerRB.velocity.magnitude >= chugging) GameEvents.current.Chugging();   
        if (playerRB.velocity.magnitude >= chugging) GameEvents.current.Chugging_Idle();   */
    }

 /*   private void Chugging()
    {
        playerSprite.color = Color.blue;
    }    
    private void NotChugging()
    {
        playerSprite.color = Color.white;
    }
    private void Booking()
    {
        playerSprite.color = Color.green;
    }    
    private void NotBooking()
    {
        playerSprite.color = Color.white;
    }
 */
    private void SubscribeControllerEvents()
    {
        GameEvents.current.onLeftClick_Input += OnMove;
        GameEvents.current.onLeftClick_Input_Idle += OnMove_Idle;
   /*     GameEvents.current.onChugging += Chugging;
        GameEvents.current.onChugging_Idle += NotChugging;
        GameEvents.current.onBooking += Booking;
        GameEvents.current.onBooking_Idle += NotBooking; */
        //Debug.Log("Controller events subscribed");
    }    
    private void OnDestroy()
    {
        GameEvents.current.onLeftClick_Input -= OnMove;
        GameEvents.current.onLeftClick_Input_Idle -= OnMove_Idle;
   /*     GameEvents.current.onChugging -= Chugging;
        GameEvents.current.onChugging_Idle -= NotChugging;
        GameEvents.current.onBooking -= Booking;
        GameEvents.current.onBooking_Idle -= NotBooking; */
    }
}
