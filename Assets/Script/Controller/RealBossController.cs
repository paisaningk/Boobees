using System.Collections;
using Script.Sound;
using Script.Spawn;
using UnityEngine;

namespace Script.Controller
{
    public enum AttackState
    {
        Attack,
        Ring,
    }
    public class RealBossController : MonoBehaviour
    {
        [SerializeField] private float starMove = 3f;
        [SerializeField] private float starMoveslowe = 4f;
        private Rigidbody2D rb;
        private Transform player;
        public Animator bodyAnimator;
        public Animator armAnimator;
        public Animator ringAnimator;
        private float movespeed = 5f;
        private float stoppingDistance = 3f;
        private bool nextMove = false;
        private bool selectNextAttack;
        private AttackState attackState;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player").transform;
            if (SpawnPlayer.instance.PlayerType == PlayerType.Gun)
            {
                movespeed = 7.5f;
            }
        }

        private void FixedUpdate()
        {
            if (nextMove == false)
            {
                SelectNextMove();
            }
            var direction = (player.position - transform.position).normalized;
            transform.localScale = direction.x < 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }

        private void MoveCharacter(Vector3 direction)
        {
            Vector2 directionNormalized = direction.normalized;
            var move = (Vector2) transform.position + (directionNormalized * movespeed * Time.deltaTime);
            bodyAnimator.SetBool("Walk",true);
            armAnimator.SetBool("Walk",true);
            rb.MovePosition(move);
        }
        
        IEnumerator Wait()
        {
            var a = Random.Range(starMove,starMoveslowe);
            yield return new WaitForSeconds(a);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            nextMove = false;
            selectNextAttack = true;
        }

        public void AttackFinish()
        {
            bodyAnimator.SetBool("Walk",false);
            armAnimator.SetBool("Walk",false);
            bodyAnimator.SetBool("Attack",false);
            armAnimator.SetBool("Attack",false);
            StartCoroutine(Wait());
        }
        
        public void RingFinish()
        {
            bodyAnimator.SetBool("Walk",false);
            armAnimator.SetBool("Walk",false);
            bodyAnimator.SetBool("Skill",false);
            armAnimator.SetBool("Skill",false);
            ringAnimator.SetBool("Skill",false);
            StartCoroutine(Wait());
        }

        public void StartRing()
        {
            ringAnimator.SetBool("Skill",true);
        }
        
        public void Sound01()
        {
            SoundManager.Instance.Play(SoundManager.Sound.BossAttack01);
        }
        
        public void Sound02()
        {
            SoundManager.Instance.Play(SoundManager.Sound.BossAttack02);
        }
        

        private void SelectNextMove()
        {
            var distance = Vector2.Distance(transform.position, player.position);
            var direction = player.position - transform.position;
            if (selectNextAttack)
            {
                var random = Random.Range(1,3);
                attackState = random == 1 ? AttackState.Attack : AttackState.Ring;
                selectNextAttack = false;
            }

            if (distance >= stoppingDistance)
            {
                MoveCharacter(direction);
            }
            else if (distance <= stoppingDistance)
            {
                if (attackState == AttackState.Attack)
                {
                    bodyAnimator.SetBool("Attack",true);
                    armAnimator.SetBool("Attack",true);
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    nextMove = true;
                }
                else
                {
                    bodyAnimator.SetBool("Skill",true);
                    armAnimator.SetBool("Skill",true);
                    ringAnimator.SetBool("Skill",true);
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    nextMove = true;
                }
                
            }
        }

    }
}
