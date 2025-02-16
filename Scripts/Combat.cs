using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BanditCombat : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    public LayerMask layer;
    [SerializeField] float attackradius = 1.1f;
    public Animator bandit;
    float attackrate = 2f;
    float nextAttacktime = 0f;
    CharacterController2D player;
    int damage=100;
    void Awake()
    {
        player = GetComponent<CharacterController2D>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Time.time>=nextAttacktime) {
            if(Input.GetKeyDown(KeyCode.Mouse0)&&!EventSystem.current.IsPointerOverGameObject()) {
                bandit.SetBool("isattacking",true);
                //Triggering animation and dealing damage through animation events
                bandit.SetTrigger("attack");
                nextAttacktime = Time.time +1/attackrate;
            }
        }
    }
    public void TakeDamage(int a) {
        GameManager.Instance.PlayerHealth -= a;
        
        if(GameManager.Instance.PlayerHealth<=0) {
            GameManager.Instance.GameOver();
            bandit.SetTrigger("Death");
        }
        else {
            bandit.SetTrigger("Hurt");
        }
    }
    void Attack() {
        //Detecting enemies in range
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position,attackradius,layer);
        //Damaging the enemies through script
        foreach(Collider2D enemy in hitenemies) {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("It's working!");
        }
    }
    void AttackFinish() {
        bandit.SetBool("isattacking",false);
    }
}
