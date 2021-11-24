using System.Collections;
using UnityEngine;

namespace Script.Controller
{
    public class ExecutionerController : MonoBehaviour
    {
        private Rigidbody2D Rb;
        private Transform player;
        private Animator animator;
        private float movespeed = 8f;
        private float stoppingDistance = 2f;
        private Vector3 directionnormalized;
        private bool attacking = false;
        private bool nextMove = false;
        

        private void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            if (nextMove == false)
            {
                Selectnextmove();
            }
            
        }
    
        private void moveCharacter(Vector3 direction)
        {
            Vector2 directionNormalized = direction.normalized;
            var move = (Vector2) transform.position + (directionNormalized * movespeed * Time.deltaTime);
            
            animator.SetFloat("MoveX",directionnormalized.x);
            animator.SetBool("Walking",true);
            
            Rb.MovePosition(move);
        }
        
        IEnumerator Wait3sec()
        {
            yield return new WaitForSeconds(1.5f);
            Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            attacking = false;
            nextMove = false;
        }

        private void AttackFinish()
        {
            animator.SetBool("Attack",false);
            StartCoroutine(Wait3sec());
        }

        private void Selectnextmove()
        {
            var distance = Vector2.Distance(transform.position, player.position);
            var direction = player.position - transform.position;
            directionnormalized = direction.normalized;
            
            if ( distance >= stoppingDistance)
            {
                moveCharacter(direction);
            }
            else if ( distance <= stoppingDistance)
            {
                if (attacking == false)
                {
                    Rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    animator.SetBool("Walking",false);
                    animator.SetBool("Attack",true);
                    animator.SetFloat("MoveX",directionnormalized.x);
                    nextMove = true;
                    attacking = true;
                }
            }
        }
    }
}

