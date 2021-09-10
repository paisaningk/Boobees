using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private LayerMask dashLayerMask;
        private Rigidbody2D Rd;
        private Vector3 Walk;
        private Vector3 MoveDie;
        private Animator animator;
        private bool IsAttacking;
        private bool isDashButtonDown;

        //ปรับได้
        private const float MoveSpeed = 5f;
        float dashAmount = 3f;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            Rd = GetComponent<Rigidbody2D>();
        }
    

        // Update is called once per frame
        private void Update()
        {
            Walk = Vector3.zero;
            Walk.x = Input.GetAxisRaw("Horizontal");
            Walk.y = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButton(0))
            {
                Attack();
            }
        
            MoveDie = Walk.normalized;

            if (Walk != Vector3.zero)
            {
                animator.SetFloat("MoveX",Walk.x);
                animator.SetFloat("MoveY",Walk.y);
                animator.SetBool("Walking",true); ;
            }
            else
            {
                animator.SetBool("Walking",false); ;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isDashButtonDown = true;
            }
        
        }

        private void FixedUpdate()
        {
            Rd.velocity = MoveDie * MoveSpeed;

            if (isDashButtonDown == true)
            {
                Vector3 dashPoint = transform.position + MoveDie * dashAmount;

                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, MoveDie, dashAmount,dashLayerMask);
                if (raycastHit2D.collider != null)
                {
                    dashPoint = raycastHit2D.point;
                }
                Rd.MovePosition(dashPoint);
                isDashButtonDown = false;
            }
        }

        private void Attack()
        {
            StartCoroutine(TimerRoutine());
            animator.SetBool("Attacking",true);
            //print($"Attack");
        }
        
        private IEnumerator TimerRoutine()
        {
            //code can be executed anywhere here before this next statement 
            yield return new WaitForSeconds(0.2f); //code pauses for 5 seconds
            animator.SetBool("Attacking",false);
            //print($"Attack false");
            //code resumes after the 5 seconds and exits if there is nothing else to run
 
        }
    }
}
