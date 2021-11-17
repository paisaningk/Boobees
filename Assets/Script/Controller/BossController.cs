using System.Collections;
using UnityEngine;

namespace Script.Controller
{
    public class BossController : MonoBehaviour
    {
        private Rigidbody2D Rb;
        private Transform player;
        private Animator animator;
        private Vector3 directionnormalized;
        [SerializeField] private float stoppingDistance = 3f;
        [SerializeField] private float movespeed = 20f;
        [SerializeField] private float Waitfornextmove = 3f;
        private EnemyController enemyController;
        private bool attacking = true;
        private bool nextMove = true;

        private void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            enemyController = GetComponent<EnemyController>();
            player = GameObject.FindWithTag("Player").transform;
        }
        
        private void Update()
        {
            if (nextMove)
            {
                Selectnextmove();
            }
            
        }
        
        private void Selectnextmove()
        {
            var distance = Vector2.Distance(transform.position, player.position);
            var direction = player.position - transform.position;
            
            directionnormalized = direction.normalized;
            
            if ( distance >= stoppingDistance)
            {
                Debug.Log("walk");
                MoveCharacter(direction);
            }
            else if ( distance <= stoppingDistance)
            {
                if (attacking)
                {
                    animator.SetBool("Walking",false);
                    animator.SetBool("Attack",true);
                    animator.SetFloat("MoveX",directionnormalized.x);
                    attacking = false;
                    nextMove = false;
                }
                Debug.Log("attacking");
            }
        }
        
        private void AttackFinish()
        {
            animator.SetBool("Walking",false);
            animator.SetBool("Attack",false);
            StartCoroutine(Wait3Sec());
        }
        
        IEnumerator Wait3Sec()
        {
            Selectnextmove();
            yield return new WaitForSeconds(Waitfornextmove);
            attacking = true;
            nextMove = true;
        }
        
        private void MoveCharacter(Vector3 direction)
        {
            Vector2 directionNormalized = direction.normalized;
            var move = (Vector2) transform.position + (directionNormalized * movespeed * Time.deltaTime);
            animator.SetFloat("MoveX",directionnormalized.x);
            animator.SetFloat("MoveY",directionnormalized.y);
            animator.SetBool("Walking",true);
            
            Rb.MovePosition(move);
        }
    }
}
