using System.Collections;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public abstract class Agent : MonoBehaviour
{

    protected bool canMove = true;
    private bool canTakeDamage = true;
    private bool canDie = true;
    private bool isKnockedBack;
    private bool canChase;
    private float resetDelay = 4f;

    protected bool coroutineStarted;

    [Range(0, 100)] protected int hp;
    protected float hitStun;
    //add event listened that updates this value based on what weapon is equipped
    protected int attackDamage = 1;
    protected int knockBackStrength;

    protected Vector2 currentPos;
    protected Vector2 knockBackDirection;
    protected Rigidbody2D rb;
    protected CapsuleCollider2D myCollider;
    [SerializeField] protected GameObject xpGem;



    void Start() { Initialize(); }
    public virtual void Initialize() { }

    private void FixedUpdate()
    {
        if (isKnockedBack) ApplyKnockBack();
        isKnockedBack = false;
    }
    
    private void UpdatePosition()
    {
        currentPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    private void KnockBack(Vector2 collisionLocation)
    {
        if (isKnockedBack) return;
        UpdatePosition();
        canMove = false;
        knockBackDirection = (currentPos - collisionLocation).normalized;
        isKnockedBack = true;
    }

    private void ApplyKnockBack()
    {
        rb.AddForce(knockBackDirection * knockBackStrength, ForceMode2D.Impulse);
        //DebugLogKnockBackVars();
    }

    protected IEnumerator StartTakeDamage(Vector2 collisionLocation, AIPath aiPath)
    {
        //if (coroutineStarted) yield break;
        //coroutineStarted = true;
        TakeDamage(attackDamage);
        KnockBack(collisionLocation);
        myCollider.enabled = false;
        if(aiPath != null) aiPath.canMove = false;
        canTakeDamage = false;
        yield return new WaitForSeconds(hitStun);
        if (aiPath != null) aiPath.canMove = true;
        myCollider.enabled = true;
        canMove = true;
        canTakeDamage = true;
        coroutineStarted = false;
        //StopCoroutine(StartTakeDamage(collisionLocation, aiPath));
    }

    public void TakeDamage(int attackDamage)
    {
        if (canTakeDamage)
        {
            hp = hp - attackDamage;
            Debug.Log("HP = " + hp);
            CheckHp();
        }
    }

    private void CheckHp()
    {
        if (hp < 1 && canDie)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " is Dead");
        UpdatePosition();
        Drops();
        if(tag == "Player")
        {
            GameEvents.current.Die();
        }
        Destroy(gameObject);
    }

    protected void DebugLogKnockBackVars()
    {
        Debug.Log("knockback strength = " + knockBackStrength + " knockback direction = " + knockBackDirection + " current position = " + currentPos);
    }

    public virtual void OnTriggerStay2D(Collider2D collision) { }
    public virtual void OnCollisionStay2D(Collision2D other) { }
    protected virtual void Drops() { }

}
