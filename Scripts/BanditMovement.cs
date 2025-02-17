using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

    public class BanditMovement : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public CharacterController2D controller;
        public Animator animator;
        public Rigidbody2D player;
        float horizontalmove;
        public float speed = 40f;
        bool jump;
        public bool CanMove=true;
    void Start()
    {
        animator = GetComponent<Animator>();
        // GameManager.Instance.LightHeavyBandit = gameObject;
        // GameManager.Instance.BanditPosition = gameObject.transform;
        StartCoroutine(MovementFix());
    }
    // Update is called once per frame
    void Update()
        {
            if(GameManager.Instance.InPanels) {
                return;
            }
            horizontalmove = Input.GetAxisRaw("Horizontal")*speed;
            animator.SetFloat("speed",Mathf.Abs(horizontalmove)); 
            if(Input.GetButtonDown("Jump")) {
                if(animator.GetBool("isattacking")) {
                    return;
                }
                if(controller.m_Grounded==true) {
                    player.linearVelocityY =0.02f;
                }
                jump = true;
                animator.SetBool("isjumping",true);
            }
        }
    IEnumerator MovementFix() {
        while(true) {
         if(!animator.GetBool("isjumping") && !animator.GetBool("isattacking")) {
            CanMove = true;
        }
        yield return new WaitForSeconds(0.5f);
        }
    }
    public void Onlanding() {
            animator.SetBool("isjumping",false);
        }
        void FixedUpdate() {
            if(GameManager.Instance.InPanels) {
                return;
            }
            if(CanMove&&!GameManager.Instance.dead) {
                controller.Move(horizontalmove*Time.fixedDeltaTime,false,jump);
            }
            jump = false;
        }
        void ToggleMove() {
            CanMove = !CanMove;
            if(controller.m_Grounded==true) {
                player.linearVelocityX = 0f;
                player.linearVelocityY = 0f;
            }
        }
}
