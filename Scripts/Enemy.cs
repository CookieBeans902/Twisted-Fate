using System.Collections;
using Pathfinding;
using Unity.Collections;
using UnityEditor.Callbacks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask GroundLayer;
    public LayerMask playerLayer;
    Collider2D circle;
    int CurrentHealth;
    private Animator anim;
    int attackDamage=10;
    int MaximumHealth=100;
    int coinadd=0;
    float attackrate = 2f;
    float nextAttacktime = 0f;
    bool enemydeath = false;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
    }
    void Start()
    {
        if(gameObject.CompareTag("Dino")) {
            CurrentHealth = 5*MaximumHealth;
            coinadd = 500;
            attackDamage = 4*attackDamage;
        }
        else if(gameObject.CompareTag("Skull")) {
            CurrentHealth = 2*MaximumHealth;
            attackDamage = 2*attackDamage;
            coinadd = 250;
        }
        else if(gameObject.CompareTag("Bat")) {
            CurrentHealth = 1*MaximumHealth;
            coinadd = 100;
        }
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
    }
    public void TakeDamage(int a) {
        CurrentHealth -= a;
       if(CurrentHealth>0) {
            anim.SetTrigger("Hurt");
        }
        else {
            anim.SetTrigger("Death");
        }
    }
    void OnDeath() {
        GameManager.Instance.AddCoins(coinadd);
        gameObject.SetActive(false);
    }
    void OnDeathBody() {
        GameManager.Instance.AddCoins(coinadd);
        gameObject.GetComponentInParent<AIPath>().enabled = false;
        rb.gravityScale = 1.5f;
        enemydeath=true;
        gameObject.layer = 8;
        Physics2D.IgnoreLayerCollision(gameObject.layer, 7, true);
    }
    void Update()
    {
        if(!enemydeath) {
        //Detecting the player in range. 
        Collider2D detectplayer = Physics2D.OverlapCircle(transform.position,10f,playerLayer);
        if(detectplayer!=null&&!GameManager.Instance.dead) {
            gameObject.GetComponentInParent<AIDestinationSetter>().enabled = true;
            anim.SetBool("PlayerInRange",true);
        }
        else {
            anim.SetBool("PlayerInRange",false);
            gameObject.GetComponentInParent<AIDestinationSetter>().enabled = false;
        }
        Collider2D attackplayer = Physics2D.OverlapCircle(transform.position,2.5f,playerLayer);
        if(attackplayer!=null) {
            Attack();
        }
    }
    }
    public void Attack() {
        if(Time.time>=nextAttacktime) {
            anim.SetTrigger("Attack");
            nextAttacktime = Time.time +2/attackrate;
        Collider2D hitplayer = Physics2D.OverlapCircle(transform.position,2f,playerLayer);
        if(hitplayer!=null&&GameManager.Instance.character==3) {
            hitplayer.GetComponent<HeroCombat>().TakeDamage(attackDamage);
        }
        else if (hitplayer!=null) {
            hitplayer.GetComponent<BanditCombat>().TakeDamage(attackDamage);
            Debug.Log("Damage taken");
        }
        }
    } 
}
