using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityEngine.EventSystems;

public class HeroCombat : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    public LayerMask layer;
    [SerializeField] float attackradius = 1.1f;
    public Animator knight;
    int combostep = 0;
    float comboGap = 0.5f;
    float lastAttackTime = 0.3f;
    CharacterController2D player;
    int damage=75;

    void ComboReset() {
        combostep = 0;
    }
    void Awake()
    {
        player = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.InPanels) {
            return;
        }
            if(Input.GetKeyDown(KeyCode.Mouse0)&&!EventSystem.current.IsPointerOverGameObject()) {
                if(Time.time - lastAttackTime > comboGap) {
                    combostep = 0;
                    knight.ResetTrigger("Attack1");
                    knight.ResetTrigger("Attack2");
                    knight.ResetTrigger("Attack3");
                }
            lastAttackTime=Time.time;
            combostep++;
            combostep=Mathf.Clamp(combostep,1,3);
            knight.SetTrigger("Attack" + combostep);
        }
    }
    public void TakeDamage(int a) {
        GameManager.Instance.PlayerHealth -= a;
        if(GameManager.Instance.PlayerHealth<=0) {
            GameManager.Instance.GameOver();
            knight.SetTrigger("Death");
        }
        else {
            knight.SetTrigger("Hurt");
        }
    }
    void DealDamage() {
        switch(combostep) {
                    case 1:
                    Attack(damage);
                    break;
                    case 2:
                    Attack((int)(damage*0.5));
                    break;
                    case 3:
                    Attack((int)(damage*1.5));
                    break;
                }
    }
    void Attack(int a) {
        //Detecting enemies in range
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(attackPoint.position,attackradius,layer);
        //Damaging the enemies through script
        if(hitenemies==null) {return;}
        foreach(Collider2D enemy in hitenemies) {
            enemy.GetComponent<Enemy>().TakeDamage(a);
            Debug.Log("It's working!");
        }
    }
    void AttackBegin() {
        knight.SetBool("isattacking",true);
    }
    void AttackFinish() {
        knight.SetBool("isattacking",false);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position,attackradius);
    }

}
