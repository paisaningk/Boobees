using System.Collections;
using Script.Sound;
using Script.Spawn;
using UnityEngine;

namespace Script.Controller
{
    public class ExecutionerController : MonoBehaviour
    {
        [SerializeField] private float starMove = 1.5f;
        [SerializeField] private float starMoveslowe = 2f;
        private Rigidbody2D rb;
        private Transform player;
        private Animator animator;
        private float movespeed = 8f;
        private float stoppingDistance = 2f;
        private Vector3 directionnormalized;
        private bool attacking = false;
        private bool nextMove = false;
        

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            player = GameObject.FindWithTag("Player").transform;
            if (SpawnPlayer.instance?.PlayerType == PlayerType.Gun)
            {
                stoppingDistance = 1.5f;
            }

        }

        private void FixedUpdate()
        {
            if (nextMove == false)
            {
                Selectnextmove();
                var direction = (player.position - transform.position).normalized;
                transform.localScale = direction.x < 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            }
            
        }
    
        private void MoveCharacter(Vector3 direction)
        {
            Vector2 directionNormalized = direction.normalized;
            var move = (Vector2) transform.position + (directionNormalized * movespeed * Time.deltaTime);
            
            animator.SetFloat("MoveX",directionnormalized.x);
            animator.SetBool("Walking",true);
            
            rb.MovePosition(move);
        }

        private void Playsound()
        {
            SoundManager.Instance.Play(SoundManager.Sound.ExecutionerAttack);
        }
        
        IEnumerator Wait()
        {
            var a = Random.Range(starMove,starMoveslowe);
            yield return new WaitForSeconds(a);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            attacking = false;
            nextMove = false;
        }

        private void AttackFinish()
        {
            animator.SetBool("Attack",false);
            StartCoroutine(Wait());
        }

        private void Selectnextmove()
        {
            var distance = Vector2.Distance(transform.position, player.position);
            var direction = player.position - transform.position;
            directionnormalized = direction.normalized;
            
            if ( distance >= stoppingDistance)
            {
                MoveCharacter(direction);
            }
            else if ( distance <= stoppingDistance)
            {
                if (attacking == false)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
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

