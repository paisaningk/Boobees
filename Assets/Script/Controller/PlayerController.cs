using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask dashLayerMask;
        private PlayerInputAction playerInput;
        private Rigidbody2D Rd;
        private Vector3 MoveDie;
        private Animator animator;
        private bool IsAttacking;

        //ปรับได้
        private const float MoveSpeed = 5f;
        float dashAmount = 3f;

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
                animator.SetBool("Walking",true); ;
            }
            else
            {
                animator.SetBool("Walking",false); ;
            }
            Rd.velocity = MoveDie * MoveSpeed;
        }

        private void Attack()
        {
            StartCoroutine(TimerRoutine());
            animator.SetBool("Attacking",true);
            //print($"Attack");
        }

        private void Dash()
        {
            Vector3 dashPoint = transform.position + MoveDie * dashAmount;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, MoveDie, dashAmount,dashLayerMask);
            if (raycastHit2D.collider != null)
            {
                dashPoint = raycastHit2D.point;
            }
            Rd.MovePosition(dashPoint);
        }
        
        private IEnumerator TimerRoutine()
        {
            //code can be executed anywhere here before this next statement 
            yield return new WaitForSeconds(0.2f); //code pauses for 5 seconds
            animator.SetBool("Attacking",false);
            //print($"Attack false");
            //code resumes after the 5 seconds and exits if there is nothing else to run
 
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
