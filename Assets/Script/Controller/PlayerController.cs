using System.Collections;
using UnityEngine;

namespace Assets.Script.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask dashLayerMask;
        private PlayerInputAction playerInput;
        private Rigidbody2D Rd;
        private Vector3 MoveDie;
        private Animator animator;
        private bool IsAttacking = false;
        private bool Attack01 = false;
        private bool Attack02 = false;
        private bool Attack03 = false;

        //ปรับได้
        private const float MoveSpeed = 5f;
        float dashAmount = 3f;
        private float dashcooldowntime;
        private float dashcooldown = 1f;
        //private float Attackcooldowntime;
        //private float Attackcooldown = 1f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Rd = GetComponent<Rigidbody2D>();
            playerInput = new PlayerInputAction();
            playerInput.PlayerAction.Attack.performed += context => Attack();
            playerInput.PlayerAction.Dash.performed += context => Dash();
            OnEnable();
        }

        private void Update()
        {
            var walk = playerInput.PlayerAction.Move.ReadValue<Vector2>();
            
            MoveDie = walk.normalized;
            
            if (walk != Vector2.zero)
            {
                animator.SetFloat("MoveX",walk.x);
                animator.SetFloat("MoveY",walk.y);
                animator.SetBool("Walking",true);
            }
            else
            {
                animator.SetBool("Walking",false); 
            }
            Rd.velocity = MoveDie * MoveSpeed;
        }

        private void Attack()
        {
            if (IsAttacking == false)
            {
                if (Attack01 == false)
                {
                    IsAttacking = true;
                    Attack01 = true;
                    animator.SetBool("Attacking",true); 
                    animator.SetBool("Attack01",true);
                }
                else if (Attack02 == false)
                {
                    IsAttacking = true;
                    Attack02 = true;
                    animator.SetBool("Attacking",true);
                    animator.SetBool("Attack02",true);
                }
                else if (Attack03 == false)
                {
                    IsAttacking = true;
                    Attack03 = true;
                    animator.SetBool("Attacking",true); 
                    animator.SetBool("Attack03",true);
                }

            }
        }

        private void Dash()
        {
            if (dashcooldowntime <= Time.time)
            {
                dashcooldowntime = Time.time + dashcooldown;
                Vector3 dashPoint = transform.position + MoveDie * dashAmount;

                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, MoveDie, dashAmount,dashLayerMask);
                if (raycastHit2D.collider != null)
                {
                    dashPoint = raycastHit2D.point;
                }
                Rd.MovePosition(dashPoint);
            }
        }

        private void AttackFinish()
        {
            animator.SetBool("Attacking",false);
            IsAttacking = false;
        }
        
        private void AttackFinish03()
        {
            animator.SetBool("Attacking",false);
            animator.SetBool("Attack01",false);
            animator.SetBool("Attack02",false);
            animator.SetBool("Attack03",false);
            IsAttacking = false;
            Attack01 = false;
            Attack02 = false;
            Attack03 = false;
        }
        
        private void OnEnable()
        {
            playerInput.Enable();
        }

        private void OnDisable()
        {
            playerInput.Disable();
        }
    }
}
